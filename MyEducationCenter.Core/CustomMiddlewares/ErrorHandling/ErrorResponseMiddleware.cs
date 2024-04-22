using System.Net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MyEducationCenter.Core;

public class ErrorResponseMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorResponseMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;

        var errorResponse = new ErrorResponse(exception, (int)code);
        var result = JsonConvert.SerializeObject(errorResponse);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}