using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEducationCenter.DataLayer;

[Table("course_type")]
public partial class CourseType
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("short_name")]
    [StringLength(250)]
    public string? ShortName { get; set; }

    [Column("full_name")]
    [StringLength(250)]
    public string? FullName { get; set; }

    [Column("created_date", TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [Column("direction_id")]
    public int DirectionId { get; set; }

    [InverseProperty("CourseType")]
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    [ForeignKey("DirectionId")]
    [InverseProperty("CourseTypes")]
    public virtual Direction Direction { get; set; } = null!;
}
