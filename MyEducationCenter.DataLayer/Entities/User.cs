using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyEducationCenter.DataLayer;

[Table("user")]
public partial class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("user_name")]
    [StringLength(250)]
    public string? UserName { get; set; }

    [Column("password_hash")]
    [StringLength(250)]
    public string? PasswordHash { get; set; }

    [Column("password_salt")]
    [StringLength(250)]
    public string? PasswordSalt { get; set; }

    [Column("email")]
    [StringLength(250)]
    public string? Email { get; set; }

    [Column("phone_number")]
    [StringLength(50)]
    public string? PhoneNumber { get; set; }

    [Column("organization_id")]
    public int OrganizationId { get; set; }

    [Column("shortname")]
    [StringLength(260)]
    public string Shortname { get; set; } = null!;

    [Column("fullname")]
    [StringLength(500)]
    public string Fullname { get; set; } = null!;

    [Column("created_date", TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [Column("created_user_id")]
    public int? CreatedUserId { get; set; }

    [Column("modified_date", TypeName = "timestamp without time zone")]
    public DateTime? ModifiedDate { get; set; }

    [Column("modified_user_id")]
    public int? ModifiedUserId { get; set; }

    [Column("language_id")]
    public int? LanguageId { get; set; }

    [Column("password")]
    [StringLength(250)]
    public string? Password { get; set; }

    [Column("state_id")]
    public int StateId { get; set; }

    [Column("user_type_id")]
    public int UserTypeId { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    [ForeignKey("LanguageId")]
    [InverseProperty("Users")]
    public virtual Language? Language { get; set; }

    [ForeignKey("OrganizationId")]
    [InverseProperty("Users")]
    public virtual Organization Organization { get; set; } = null!;

    [InverseProperty("CreatedUser")]
    public virtual ICollection<RoleModule> RoleModules { get; set; } = new List<RoleModule>();

    [InverseProperty("User")]
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    [ForeignKey("UserTypeId")]
    [InverseProperty("Users")]
    public virtual UserType? UserType { get; set; }

    public bool IsValidPassword(string password)
    {
        return password == Password;
    }
}
