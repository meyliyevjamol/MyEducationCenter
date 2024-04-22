using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyEducationCenter;

[Table("position")]
public partial class Position
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

    [Column("status_id")]
    public int StatusId { get; set; }

    [Column("created_date", TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [Column("created_user_id")]
    public int? CreatedUserId { get; set; }

    [Column("modified_date", TypeName = "timestamp without time zone")]
    public DateTime? ModifiedDate { get; set; }

    [Column("modified_user_id")]
    public int? ModifiedUserId { get; set; }

    [Column("organization_id")]
    public int? OrganizationId { get; set; }

    [Column("is_sender")]
    public bool IsSender { get; set; }

    [Column("is_reciever")]
    public bool IsReciever { get; set; }

    [InverseProperty("Position")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    [ForeignKey("OrganizationId")]
    [InverseProperty("Positions")]
    public virtual Organization? Organization { get; set; }

    [ForeignKey("StatusId")]
    [InverseProperty("Positions")]
    public virtual Status Status { get; set; } = null!;
}
