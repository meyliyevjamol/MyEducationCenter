using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEducationCenter.DataLayer;

[Table("role_module")]
public partial class RoleModule
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("role_id")]
    public int RoleId { get; set; }

    [Column("module_id")]
    public int ModuleId { get; set; }

    [Column("created_date", TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [Column("created_user_id")]
    public int? CreatedUserId { get; set; }

    [ForeignKey("CreatedUserId")]
    [InverseProperty("RoleModules")]
    public virtual User? CreatedUser { get; set; }

    [ForeignKey("ModuleId")]
    [InverseProperty("RoleModules")]
    public virtual Module Module { get; set; } = null!;

    [ForeignKey("RoleId")]
    [InverseProperty("RoleModules")]
    public virtual Role Role { get; set; } = null!;
}
