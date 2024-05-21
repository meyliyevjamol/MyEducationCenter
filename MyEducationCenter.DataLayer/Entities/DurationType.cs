using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEducationCenter.DataLayer;

[Table("duration_type")]
public partial class DurationType
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("short_name")]
    [StringLength(20)]
    public string? ShortName { get; set; }

    [Column("full_name")]
    [StringLength(20)]
    public string? FullName { get; set; }

    [InverseProperty("DurationType")]
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
