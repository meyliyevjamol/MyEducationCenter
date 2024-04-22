using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyEducationCenter;

[Table("table")]
public partial class Table
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("short_name")]
    [StringLength(250)]
    public string? ShortName { get; set; }

    [Column("full_name")]
    [StringLength(300)]
    public string? FullName { get; set; }

    [Column("db_table_name")]
    [StringLength(150)]
    public string? DbTableName { get; set; }
}
