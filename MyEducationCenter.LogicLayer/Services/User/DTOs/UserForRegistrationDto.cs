using MyEducationCenter.DataLayer;
using System.ComponentModel.DataAnnotations;

namespace MyEducationCenter.LogicLayer;

public class UserForRegistrationDto
{
    [Required]
    [StringLength(50)]
    public string UserName { get; set; }

    [Required]
    [StringLength(50)]
    public string ShortName { get; set; }

    [StringLength(50)]
    public string FullName { get; set; }

    [Required]
    [StringLength(100)]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(250)]
    public virtual string Password { get; set; }
    public int OrganizationId { get; set; }
    public bool isNewEntity { get; set; } = false;
    public int LanguageId { get; set; }
    public int UserTypeId { get; set; } 
    public List<int> Roles { get; set; } = new List<int>();

    public static explicit operator UserForRegistrationDto(User user)
    {
        return new UserForRegistrationDto
        {
            UserName = user.UserName,
            ShortName = user.Shortname,
            FullName = user.Fullname,
            Email = user.Email,
            OrganizationId = user.OrganizationId,
            LanguageId = user.LanguageId.Value,
            UserTypeId = user.UserTypeId
            
        };
    }

    public static explicit operator User(UserForRegistrationDto userDto)
    {
        var user = new User
        {
            UserName = userDto.UserName,
            Fullname = userDto.FullName,
            Shortname = userDto.ShortName,
            Email = userDto.Email,
            OrganizationId = userDto.OrganizationId,
            LanguageId = userDto.LanguageId,
            UserTypeId = userDto.UserTypeId
        };
        user.Password = userDto.Password;
        //user.SetPassword(userDto.Password);

        return user;
    }        
}
