using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO.Compression;
using System.Net.Http.Headers;
using System.Text;

namespace MyEducationCenter.Core;

public class RequestTimingMiddleware
{
    private readonly RequestDelegate _next;
    private const int ThresholdMilliseconds = 10000; // 10 seconds

    public RequestTimingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.EnableBuffering();

        var watch = Stopwatch.StartNew();
        try

        {
            await _next(context);
        }
        catch (Exception ex)
        {
            watch.Stop();
            var errorDetails = await FormatErrorDetails(context, ex, watch.ElapsedMilliseconds);

            var curlCommand = await FormatCurlCommand(context);
            //var txtFileData = await CreateCurlCommandTxtFileAsync(curlCommand);
            //var zipFileData = await CreateZipFileAsync(txtFileData);
            await SendTelegramAlertAsync(errorDetails, curlCommand);
            throw;
        }
        finally
        {
            watch.Stop();
            if (watch.ElapsedMilliseconds > ThresholdMilliseconds)
            {
                var message = $"Sekin ishlayapti: {context.Request.Method} {context.Request.Path} \n" +
                              $"Ketgan vaqt {watch.ElapsedMilliseconds} ms \n" +
                              $"Backendchi: @xusan_nematovv \n" +
                              $"#optimizatsiya";

                var curlCommand = await FormatCurlCommand(context);
                var txtFileData = await CreateCurlCommandTxtFileAsync(curlCommand);
                var zipFileData = await CreateZipFileAsync(txtFileData);

                await SendTelegramAlertAsync(message, curlCommand);
            }
        }
    }

    private async Task<string> FormatErrorDetails(HttpContext context, Exception exception, long elapsedMilliseconds)
    {
        var message = new StringBuilder();
        //message.AppendLine($"Exception: {exception.Message}");
        //message.AppendLine($"```cURL: {await FormatCurlCommand(context)}```");
        message.AppendLine($"Endpoint: {context.Request.Method} {context.Request.Path}");
        message.AppendLine($"Javobga ketgan vaqt: {elapsedMilliseconds} ms");
        message.AppendLine($"Backendchi: @xusan_nematovv");
        message.AppendLine($"\n #bugfix");
        return message.ToString();
    }

    private async Task SendTelegramAlertAsync(string message, byte[] zipData)
    {
        try
        {
            var url = $"https://api.telegram.org/botYourBotToken/sendDocument";
            using var httpClient = new HttpClient();
            using var form = new MultipartFormDataContent();

            form.Add(new StringContent("-866243777"), "chat_id");
            form.Add(new StringContent(message), "caption");

            using var fileContent = new ByteArrayContent(zipData);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/zip");
            form.Add(fileContent, "document", "curl_command.zip");



            await httpClient.PostAsync(url, form);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending Telegram message: {ex.Message}");
        }
    }

    private async Task SendTelegramAlertAsync(string message, string curlCommand)
    {
        try
        {
            //var url = $"https://api.telegram.org/bot6964515471:AAF7trxahZrlaOnUVMh4qanJHszLM7ihXL8/sendMessage?chat_id=-866243777&text={Uri.EscapeDataString(message)}";
            //using var httpClient = new HttpClient();
            //await httpClient.GetAsync(url);

            var url = $"https://api.telegram.org/bot6964515471:AAF7trxahZrlaOnUVMh4qanJHszLM7ihXL8/sendMessage";
            using var httpClient = new HttpClient();
            using var form = new MultipartFormDataContent();

            var escapedCurlCommand = EscapeMarkdownV2(curlCommand);

            var payload = new StringContent(JsonConvert.SerializeObject(new
            {
                chat_id = "-866243777",
                text = $"{message}\n```\n{escapedCurlCommand}\n```",
                parse_mode = "MarkdownV2"
            }), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(url, payload);

            if (!response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Failed to send message. Status: {response.StatusCode}. Response: {responseContent}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending Telegram message: {ex.Message}");
        }
    }

    private async Task<string> FormatCurlCommand(HttpContext context)
    {
        var request = context.Request;
        var curlCommand = new StringBuilder($"curl -X {request.Method} \\\n");

        foreach (var header in request.Headers)
        {
            curlCommand.AppendFormat(" -H '{0}: {1}' \\\n", header.Key, header.Value);
        }

        if (request.ContentLength > 0 && (request.Method == "POST" || request.Method == "PUT" || request.Method == "PATCH"))
        {
            request.Body.Position = 0;
            using var reader = new StreamReader(request.Body);
            var requestBody = await reader.ReadToEndAsync();
            request.Body.Position = 0;

            curlCommand.AppendFormat(" -d '{0}' \\\n", requestBody.Replace("'", "\\'"));
        }

        curlCommand.AppendFormat(" '{0}{1}'", request.Scheme, "://", request.Host + request.Path + request.QueryString);

        return curlCommand.ToString().TrimEnd('\\', '\n');
    }

    private async Task<byte[]> CreateCurlCommandTxtFileAsync(string curlCommand)
    {
        using var memoryStream = new MemoryStream();
        using (var writer = new StreamWriter(memoryStream, Encoding.UTF8, leaveOpen: true))
        {
            await writer.WriteAsync(curlCommand);
            await writer.FlushAsync();
            return memoryStream.ToArray();
        }
    }

    private async Task<byte[]> CreateZipFileAsync(byte[] txtFileData)
    {
        using var memoryStream = new MemoryStream();
        using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
        {
            var zipEntry = archive.CreateEntry("curls.txt", CompressionLevel.Optimal);
            using var zipStream = zipEntry.Open();
            await zipStream.WriteAsync(txtFileData, 0, txtFileData.Length);
        }
        return memoryStream.ToArray();
    }

    private string EscapeMarkdownV2(string text)
    {
        var charactersToEscape = new char[] { '_', '*', '[', ']', '(', ')', '~', '`', '>', '#', '+', '-', '=', '|', '{', '}', '.', '!' };

        foreach (var character in charactersToEscape)
        {
            text = text.Replace(character.ToString(), "\\" + character);
        }

        return text;
    }

}
