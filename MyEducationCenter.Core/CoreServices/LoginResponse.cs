
namespace MyEducationCenter.Core;

public class LoginResponse
{
    public string Message { get; set; }
    public TokenData Data { get; set; }
    public string TokenType { get; set; }

    public class TokenData
    {
        public string Token { get; set; }
    }
}
