using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyEducationCenter.DataLayer;

[Table("plastic_card")]
public partial class PlasticCard
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("cardholder_name")]
    [StringLength(200)]
    public string CardholderName { get; set; } = null!;

    [Column("card_number")]
    [StringLength(100)]
    public string CardNumber { get; set; } = null!;

    [Column("card_name")]
    [StringLength(50)]
    public string? CardName { get; set; }

    [Column("state_id")]
    public int StateId { get; set; }

    [Column("created_date", TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [Column("created_user_id")]
    public int? CreatedUserId { get; set; }

    [Column("modified_date", TypeName = "timestamp without time zone")]
    public DateTime? ModifiedDate { get; set; }

    [Column("modified_user_id")]
    public int? ModifiedUserId { get; set; }

    [Column("organization_id")]
    public int OrganizationId { get; set; }

    [ForeignKey("OrganizationId")]
    [InverseProperty("PlasticCards")]
    public virtual Organization Organization { get; set; } = null!;

    [ForeignKey("StateId")]
    [InverseProperty("PlasticCards")]
    public virtual Status State { get; set; } = null!;
}
