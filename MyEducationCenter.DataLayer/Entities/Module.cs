using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyEducationCenter.DataLayer;

[Table("module")]
public partial class Module
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(300)]
    public string Name { get; set; } = null!;

    [Column("created_date", TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [Column("created_user_id")]
    public int? CreatedUserId { get; set; }

    [Column("modified_date", TypeName = "timestamp without time zone")]
    public DateTime? ModifiedDate { get; set; }

    [Column("modified_user_id")]
    public int? ModifiedUserId { get; set; }

    [Column("sub_group_id")]
    public int? SubGroupId { get; set; }

    [InverseProperty("Module")]
    public virtual ICollection<RoleModule> RoleModules { get; set; } = new List<RoleModule>();

    [ForeignKey("SubGroupId")]
    [InverseProperty("Modules")]
    public virtual ModuleSubGroup? SubGroup { get; set; }
}
