using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using MyEducationCenter.Core;

namespace MyEducationCenter.Attributes;
public class AuthorizeFilter : Attribute, IAuthorizationFilter, IFilterMetadata
{
    protected string[] ModuleCodes { get; }

    public AuthorizeFilter(string[] moduleCodes)
    {
        ModuleCodes = moduleCodes;
    }

    public virtual void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.Filters.Any((IFilterMetadata a) => a is AllowAnonymousAttribute))
        {
            IAuthService authService = (IAuthService)context.HttpContext.RequestServices.GetService(typeof(IAuthService));
            if (!authService.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
            }
            else if ((ModuleCodes?.Any()).GetValueOrDefault() && !ModuleCodes.Any((string moduleCode) => authService.HasPermission(moduleCode)))
            {
                context.Result = new StatusCodeResult(403);
            }
        }
    }
}
