using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyEducationCenter.DataLayer;
using MyEducationCenter.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MyEducationCenter.LogicLayer;

namespace WoodWise.WebApi;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

    public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options => { });

    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<AppDbContext>(opts => opts.UseNpgsql(configuration.GetConnectionString("wood_wise")));
    public static void ConfigureJwtSettigns(this IServiceCollection services, JwtSettings jwtSettings) =>
        services.AddSingleton(jwtSettings);
    public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        var secretKey = configuration.GetSection("JwtSettings:SecretKey").Value;
        services.AddSingleton(_ => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)));
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            var jwtSettings = services.BuildServiceProvider().GetRequiredService<IOptions<JwtSettings>>().Value;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtSettings.ValidIssuer,
                ValidAudience = jwtSettings.ValidAudience,
                IssuerSigningKey = services.BuildServiceProvider().GetRequiredService<SymmetricSecurityKey>()
            };
        });
    }
    public static void AddScopedServiceCollections(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IOrganizationService, OrganizationService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IRoleModuleService, RoleModuleService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();


        services.AddAutoMapper(typeof(OrganizationConfig));
    }

}

