using Microsoft.AspNetCore.Mvc;
using MyEducationCenter.Logiclayer;
namespace MyEducationCenter.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class AuthorizeAttribute : TypeFilterAttribute
{
    public AuthorizeAttribute()
        : base(typeof(AuthorizeFilter))
    {
        Arguments = new[] { new string[] { } };
    }

    public AuthorizeAttribute(params ModuleCode[] moduleCodes)
        : base(typeof(AuthorizeFilter))
    {
        Arguments = new[] { moduleCodes.Select(c => c.ToString()).ToArray() };
    }
}
