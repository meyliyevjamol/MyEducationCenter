﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEducationCenter.DataLayer;

[Table("deleted")]
public partial class Deleted
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("table_id")]
    public int TableId { get; set; }

    [Column("column_id")]
    public int ColumnId { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }
    [Column("created_date", TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [Column("column_name")]
    [StringLength(200)]
    public string? ColumnName { get; set; }
}
