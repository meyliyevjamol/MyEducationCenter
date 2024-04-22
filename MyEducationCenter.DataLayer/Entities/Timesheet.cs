using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyEducationCenter.DataLayer;

[Table("timesheet")]
public partial class Timesheet
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("doc_date")]
    public DateOnly DocDate { get; set; }

    [Column("in_time", TypeName = "timestamp without time zone")]
    public DateTime? InTime { get; set; }

    [Column("out_time", TypeName = "timestamp without time zone")]
    public DateTime? OutTime { get; set; }

    [Column("employee_id")]
    public int EmployeeId { get; set; }

    [Column("organization_id")]
    public int OrganizationId { get; set; }

    [Column("status_id")]
    public int StatusId { get; set; }

    [Column("delayed")]
    public bool? Delayed { get; set; }

    [Column("delay_reason")]
    [StringLength(200)]
    public string? DelayReason { get; set; }

    [Column("dont_work")]
    public bool? DontWork { get; set; }

    [Column("dont_work_reason")]
    [StringLength(200)]
    public string? DontWorkReason { get; set; }

    [Column("created_date", TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [Column("created_user_id")]
    public int? CreatedUserId { get; set; }

    [Column("modified_date", TypeName = "timestamp without time zone")]
    public DateTime? ModifiedDate { get; set; }

    [Column("modified_user_id")]
    public int? ModifiedUserId { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("Timesheets")]
    public virtual Employee Employee { get; set; } = null!;

    [ForeignKey("OrganizationId")]
    [InverseProperty("Timesheets")]
    public virtual Organization Organization { get; set; } = null!;

    [ForeignKey("StatusId")]
    [InverseProperty("Timesheets")]
    public virtual Status Status { get; set; } = null!;
}
