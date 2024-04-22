﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyEducationCenter;

[Table("status")]
public partial class Status
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

    [Column("details")]
    [StringLength(500)]
    public string? Details { get; set; }

    [Column("created_date", TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [Column("created_user_id")]
    public int? CreatedUserId { get; set; }

    [Column("modified_date", TypeName = "timestamp without time zone")]
    public DateTime? ModifiedDate { get; set; }

    [Column("modified_user_id")]
    public int? ModifiedUserId { get; set; }

    [InverseProperty("State")]
    public virtual ICollection<District> Districts { get; set; } = new List<District>();

    [InverseProperty("Status")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    [InverseProperty("State")]
    public virtual ICollection<Organization> Organizations { get; set; } = new List<Organization>();

    [InverseProperty("State")]
    public virtual ICollection<PlasticCard> PlasticCards { get; set; } = new List<PlasticCard>();

    [InverseProperty("Status")]
    public virtual ICollection<Position> Positions { get; set; } = new List<Position>();

    [InverseProperty("State")]
    public virtual ICollection<Region> Regions { get; set; } = new List<Region>();

    [InverseProperty("Status")]
    public virtual ICollection<Timesheet> Timesheets { get; set; } = new List<Timesheet>();
}
