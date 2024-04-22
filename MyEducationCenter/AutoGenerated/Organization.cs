﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyEducationCenter;

[Table("organization")]
public partial class Organization
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string? Name { get; set; }

    [Column("address")]
    [StringLength(200)]
    public string? Address { get; set; }

    [Column("phone_number")]
    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    [Column("vat_code")]
    [StringLength(20)]
    public string? VatCode { get; set; }

    [Column("created_date", TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [Column("created_user_id")]
    public int CreatedUserId { get; set; }

    [Column("modified_date", TypeName = "timestamp without time zone")]
    public DateTime? ModifiedDate { get; set; }

    [Column("modified_user_id")]
    public int? ModifiedUserId { get; set; }

    [Column("district_id")]
    public int? DistrictId { get; set; }

    [Column("region_id")]
    public int? RegionId { get; set; }

    [Column("state_id")]
    public int? StateId { get; set; }

    [ForeignKey("DistrictId")]
    [InverseProperty("Organizations")]
    public virtual District? District { get; set; }

    [InverseProperty("Organization")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    [InverseProperty("Organization")]
    public virtual ICollection<PlasticCard> PlasticCards { get; set; } = new List<PlasticCard>();

    [InverseProperty("Organization")]
    public virtual ICollection<Position> Positions { get; set; } = new List<Position>();

    [ForeignKey("RegionId")]
    [InverseProperty("Organizations")]
    public virtual Region? Region { get; set; }

    [ForeignKey("StateId")]
    [InverseProperty("Organizations")]
    public virtual Status? State { get; set; }

    [InverseProperty("Organization")]
    public virtual ICollection<Timesheet> Timesheets { get; set; } = new List<Timesheet>();

    [InverseProperty("Organization")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
