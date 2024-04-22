﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyEducationCenter.DataLayer;

[Table("user_type")]
public partial class UserType
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("short_name")]
    [StringLength(100)]
    public string ShortName { get; set; } = null!;

    [Column("full_name")]
    [StringLength(500)]
    public string FullName { get; set; } = null!;

    [Column("order_number")]
    public int OrderNumber { get; set; }

    [Column("date_of_created", TypeName = "timestamp without time zone")]
    public DateTime? DateOfCreated { get; set; }

    [InverseProperty("UserType")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
