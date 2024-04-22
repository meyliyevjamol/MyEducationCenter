using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyEducationCenter.Core;
using MyEducationCenter.DataLayer;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace MyEducationCenter.Logiclayer;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly JwtSettings _jwtSettings;
    
    private User _user;
    private Employee _employee;
    private Organization _organization;
    private readonly AppDbContext _context;
    private string _userName;
    private bool? _isAuthenticated;

    public AuthService(
        IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor,
        AppDbContext context,
        IUnitOfWork repository,
        JwtSettings jwtSettings)
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _context = context;
        _unitOfWork = repository;
        _jwtSettings = jwtSettings;
    }

    public string Login(UserForAuthenticationDto dto)
    {
        _user = _context.Set<User>()
            .Include(ur => ur.UserRoles)
            .Where(user => user.UserName == dto.UserName)
            .FirstOrDefault();
        if (_user == null)
            throw new ArgumentException("Username is incorrect!");

        var userRoles = _context.UserRoles.Where(a => a.UserId == _user.Id).ToArray();

        var isValidPassword = _user.IsValidPassword(dto.Password);
        if (!isValidPassword)
            throw new ArgumentException("Password is incorrect!");

        var signingCredentials = GetSigningCredentials();
        var claims = GetClaims(_user.UserRoles.FirstOrDefault().RoleId, dto.UserName);
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private SigningCredentials GetSigningCredentials()
    {
        var secretKey = _configuration.GetSection("JwtSettings:secretKey").Value;
        var key = Encoding.UTF8.GetBytes(secretKey);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private List<Claim> GetClaims(int roleId, string userName)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim("sid", userName),
            new Claim("role", roleId.ToString())
        };

        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var tokenOptions = new JwtSecurityToken
        (
            issuer: jwtSettings.GetSection("validIssuer").Value,
            audience: jwtSettings.GetSection("validAudience").Value,
            claims: claims,
            expires: DateTime.Now
                .AddMinutes(Convert
                .ToDouble(jwtSettings
                .GetSection("Expires").Value)),
            signingCredentials: signingCredentials
         );
        return tokenOptions;
    }

    public UserAuthModel User
    {
        get
        {
            if (_user == null)
            {
                _user = _context.Set<User>()
                                .Include(a=> a.Organization)
                                .Include(a=> a.RoleModules)
                                .ThenInclude(a=> a.Module)
                                .Where(user => user.UserName == UserName)
                                .FirstOrDefault();
            }
            var res = _user.ConvertToDto();

            return res;
        }
    }
    
    public EmployeeAuthModel Employee
    {
        get
        {
            if (UserName != null && !UserName.IsNullOrEmpty())
            {
                _employee = _context.Set<Employee>()
                                .Where(e => e.User.UserName == UserName)
                                //.Include(a => a.Store)
                                .FirstOrDefault();
            }
            if(_employee is null)
                return null;

            return _employee.ConvertToDto();
        }
    }

    public OrganizationAuthModel Organization
    {
        get
        {
            if (_organization == null)
            {
                _organization = _context.Set<Organization>()
                                .Where(e => e.Id == User.OrganizationId)
                                .FirstOrDefault();
            }

            return _organization.ConvertToDto();
        }
    }

    public virtual string UserName
    {
        get
        {
            if (_userName == null)
            {
                _userName = new JwtSecurityTokenHandler()
                    .ReadJwtToken(ReadTokenFromRequest()).Claims.FirstOrDefault((Claim a) => a.Type == "sid")?.Value;
            }

            return _userName;
        }
    }

    public virtual bool IsAuthenticated
    {
        get
        {
            if (!_isAuthenticated.HasValue)
            {
                string text = ReadTokenFromRequest();
                if (text.IsNullOrEmpty())
                {
                    _isAuthenticated = false;
                }
                else
                {
                    _isAuthenticated = ValidateToken(text);
                }
            }

            return _isAuthenticated.Value;
        }
    }
    public virtual bool HasPermission(string moduleCode)
    {
        if (!IsAuthenticated)
        {
            return false;
        }

        return Modules.Contains(moduleCode);
    }

    private bool _roleIdIsInitialized = false;
    private int? _roleId;
    public int? RoleId
    {
        get
        {
            if (_roleId != null || _roleIdIsInitialized)
                return _roleId;
            if (IsAuthenticated)
            {
                if (int.TryParse(new JwtSecurityTokenHandler().ReadJwtToken(ReadTokenFromRequest()).Claims.FirstOrDefault((Claim a) => a.Type == "role")?.Value, out int roleId))
                {
                    _roleId = roleId;
                    _modules = _context.Set<RoleModule>().Where(a => a.RoleId == RoleId).Select(c => c.Module.Name).ToHashSet();
                }
            }
            _roleIdIsInitialized = true;
            return _roleId;
        }
    }

    private HashSet<string> _modules;
    public HashSet<string> Modules
    {
        get
        {
            if (IsAuthenticated && _modules == null)
                _modules = User.Modules.ToHashSet();
            return _modules;
        }
    }

    protected virtual string ReadTokenFromRequest()
    {
        string authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
        if (string.IsNullOrEmpty(authorizationHeader))
        {
            return null;
        }

        string[] array = authorizationHeader.Split(' ');
        if (array.Length != 2 || array[0] != "Bearer")
        {
            return null;
        }

        string token = array[1];
        if (string.IsNullOrEmpty(token))
        {
            return null;
        }

        return token;
    }

    protected bool ValidateToken(string token)
    {
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        TokenValidationParameters validationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true,
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidIssuer = _jwtSettings.ValidIssuer,
            ValidAudience = _jwtSettings.ValidAudience,
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey))
        };
        try
        {
            SecurityToken validatedToken;
            return ((IPrincipal)jwtSecurityTokenHandler.ValidateToken(token, validationParameters, out validatedToken)).Identity.IsAuthenticated && validatedToken.ValidTo > DateTime.UtcNow;
        }
        catch (Exception)
        {
            return false;
        }
    }

    protected virtual string ReadTokenFromRequest1()
    {
        string[] array = _httpContextAccessor.HttpContext!.Request.Headers["Authorization"].FirstOrDefault().ToString().Split(' ');
        if (array.Length != 2 || array[0].ToString() != "Bearer")
        {
            return null;
        }

        return array[1].ToString();
    }        
}
