

namespace MyEducationCenter.Core;

public class ErrorResponse
{
    public int status { get; set; }
    public string message { get; set; }
    public ErrorResponse(Exception ex, int statusCode)
    {
        message = ex.Message;
        status = statusCode;
    }
}