

namespace MyEducationCenter.Core;

public interface ISmsService
{
    Task<string> SendSmsAsync(string mobilePhone, string message, string from, string callbackUrl);
}
