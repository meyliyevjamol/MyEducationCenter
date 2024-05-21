using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEducationCenter.DataLayer;

[Table("direction")]
public partial class Direction
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("short_name")]
    [StringLength(100)]
    public string? ShortName { get; set; }

    [Column("full_name")]
    [StringLength(100)]
    public string? FullName { get; set; }

    [InverseProperty("Direction")]
    public virtual ICollection<CourseType> CourseTypes { get; set; } = new List<CourseType>();
}
