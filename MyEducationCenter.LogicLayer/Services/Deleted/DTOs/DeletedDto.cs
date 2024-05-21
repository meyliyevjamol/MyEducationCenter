

namespace MyEducationCenter.LogicLayer.Services;

public class DeletedDto:DeletedCreateDto
{
    public string TableName {  get; set; }
    public string ColumnName {  get; set; }
}
