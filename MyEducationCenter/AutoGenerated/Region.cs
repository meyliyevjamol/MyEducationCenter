using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyEducationCenter;

[Table("region")]
public partial class Region
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

    [Column("created_date", TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [Column("country_id")]
    public int CountryId { get; set; }

    [ForeignKey("CountryId")]
    [InverseProperty("Regions")]
    public virtual Country Country { get; set; } = null!;

    [InverseProperty("Region")]
    public virtual ICollection<District> Districts { get; set; } = new List<District>();

    [InverseProperty("Region")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    [InverseProperty("Region")]
    public virtual ICollection<Organization> Organizations { get; set; } = new List<Organization>();

    [ForeignKey("StateId")]
    [InverseProperty("Regions")]
    public virtual Status State { get; set; } = null!;
}
