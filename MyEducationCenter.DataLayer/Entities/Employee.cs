using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyEducationCenter.DataLayer;
[Table("employee")]
public partial class Employee
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("first_name")]
    [StringLength(100)]
    public string FirstName { get; set; } = null!;

    [Column("last_name")]
    [StringLength(100)]
    public string LastName { get; set; } = null!;

    [Column("middle_name")]
    [StringLength(100)]
    public string MiddleName { get; set; } = null!;

    [Column("details")]
    [StringLength(500)]
    public string? Details { get; set; }

    [Column("organization_id")]
    public int OrganizationId { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("status_id")]
    public int StatusId { get; set; }

    [Column("position_id")]
    public int PositionId { get; set; }

    [Column("phone_number")]
    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    [Column("created_date", TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [Column("created_user_id")]
    public int? CreatedUserId { get; set; }

    [Column("modified_date", TypeName = "timestamp without time zone")]
    public DateTime? ModifiedDate { get; set; }

    [Column("modified_user_id")]
    public int? ModifiedUserId { get; set; }

    [Column("region_id")]
    public int? RegionId { get; set; }

    [Column("district_id")]
    public int? DistrictId { get; set; }

    [Column("store_id")]
    public int? StoreId { get; set; }

    [Column("work_out_time")]
    public TimeOnly WorkOutTime { get; set; }

    [Column("work_in_time")]
    public TimeOnly WorkInTime { get; set; }

    [ForeignKey("DistrictId")]
    [InverseProperty("Employees")]
    public virtual District? District { get; set; }

    [ForeignKey("OrganizationId")]
    [InverseProperty("Employees")]
    public virtual Organization Organization { get; set; } = null!;

    [ForeignKey("PositionId")]
    [InverseProperty("Employees")]
    public virtual Position Position { get; set; } = null!;

    [ForeignKey("RegionId")]
    [InverseProperty("Employees")]
    public virtual Region? Region { get; set; }

    [ForeignKey("StatusId")]
    [InverseProperty("Employees")]
    public virtual Status Status { get; set; } = null!;

    [InverseProperty("Employee")]
    public virtual ICollection<Timesheet> Timesheets { get; set; } = new List<Timesheet>();

    [ForeignKey("UserId")]
    [InverseProperty("Employees")]
    public virtual User User { get; set; } = null!;
}
