namespace MyEducationCenter.Core;

public class ErrorConst
{
    public const string ProblemCreating = $"There is problem with creating.";
    public static string NotFound<T>(int id) => $"{nameof(T)} with ID {id} not found.";
    public static string NotFound<T>(long id) => $"{nameof(T)} with ID {id} not found.";
    public static string NotFound<T>(T id) => $"Value with ID {id} not found.";
    public static string LeadNotFound(int id) => $"Lead with ID {id} not found.";
    public static string TemplateNotFound(int id) => $"Template with ID {id} not found.";
    public static string AttendanceNotFound(int id) => $"Attendance with ID {id} not found.";
    public const string ProductNotEnough = "Product not enough";
    public static string ProductValidation(string modelname, string modelColor, string details) => $"This Product modelname: {modelname} modelColorName: {modelColor} modelDetails: {details} already exsisting";
}
