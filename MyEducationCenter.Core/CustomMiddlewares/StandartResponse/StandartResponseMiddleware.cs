using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;


namespace MyEducationCenter.Core;

public class StandartResponseMiddleware
{
    private readonly RequestDelegate _next;

    public StandartResponseMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var originalBodyStream = context.Response.Body;
        string responseBodyText = "";
        bool isSuccess = true;

        try
        {
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await _next(context);

                context.Response.Body.Seek(0, SeekOrigin.Begin);
                responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);
            }
        }
        catch (Exception ex)
        {
            isSuccess = false;
            responseBodyText = ex.Message;
        }
        finally
        {
            var standardResponse = new StandartResponse
            {
                Status = context.Response.StatusCode.ToString(),
                Message = responseBodyText
            };

            context.Response.Body = originalBodyStream;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(standardResponse));
        }
    }
}
