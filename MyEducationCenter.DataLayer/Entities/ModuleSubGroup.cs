﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyEducationCenter.DataLayer;

[Table("module_sub_group")]
public partial class ModuleSubGroup
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("group_id")]
    public int? GroupId { get; set; }

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

    [ForeignKey("GroupId")]
    [InverseProperty("ModuleSubGroups")]
    public virtual ModuleGroup? Group { get; set; }

    [InverseProperty("SubGroup")]
    public virtual ICollection<Module> Modules { get; set; } = new List<Module>();
}
