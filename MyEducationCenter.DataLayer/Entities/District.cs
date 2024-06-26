﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEducationCenter.DataLayer;

[Table("district")]
public partial class District
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("short_name")]
    [StringLength(100)]
    public string ShortName { get; set; } = null!;

    [Column("full_name")]
    [StringLength(100)]
    public string FullName { get; set; } = null!;

    [Column("state_id")]
    public int StateId { get; set; }

    [Column("region_id")]
    public int RegionId { get; set; }

    [Column("created_date", TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [InverseProperty("District")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    [InverseProperty("District")]
    public virtual ICollection<Organization> Organizations { get; set; } = new List<Organization>();

    [ForeignKey("RegionId")]
    [InverseProperty("Districts")]
    public virtual Region Region { get; set; } = null!;

    [ForeignKey("StateId")]
    [InverseProperty("Districts")]
    public virtual Status State { get; set; } = null!;
}
