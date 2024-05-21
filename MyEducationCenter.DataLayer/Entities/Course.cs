﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEducationCenter.DataLayer;

[Table("course")]
public partial class Course
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("short_name")]
    [StringLength(100)]
    public string? ShortName { get; set; }

    [Column("full_name")]
    [StringLength(100)]
    public string? FullName { get; set; }

    [Column("state_id")]
    public int StateId { get; set; }

    [Column("duration_type_id")]
    public int DurationTypeId { get; set; }

    [Column("course_type_id")]
    public int CourseTypeId { get; set; }

    [Column("region_id")]
    public int RegionId { get; set; }

    [Column("organization_id")]
    public int OrganizationId { get; set; }

    [Column("teacher_full_name")]
    [StringLength(100)]
    public string TeacherFullName { get; set; } = null!;

    [Column("price")]
    public decimal? Price { get; set; }

    [ForeignKey("CourseTypeId")]
    [InverseProperty("Courses")]
    public virtual CourseType CourseType { get; set; } = null!;

    [ForeignKey("DurationTypeId")]
    [InverseProperty("Courses")]
    public virtual DurationType DurationType { get; set; } = null!;

    [ForeignKey("OrganizationId")]
    [InverseProperty("Courses")]
    public virtual Organization Organization { get; set; } = null!;

    [ForeignKey("RegionId")]
    [InverseProperty("Courses")]
    public virtual Region Region { get; set; } = null!;

    [ForeignKey("StateId")]
    [InverseProperty("Courses")]
    public virtual Status State { get; set; } = null!;
}
