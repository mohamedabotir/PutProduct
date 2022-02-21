using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PutProduct.Services
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (string)context.HttpContext.Request.Headers.Authorization;
            if (user == null)
                context.Result = new JsonResult(new { message = "UnAuthorize" }) { StatusCode=StatusCodes.Status401Unauthorized};

        }
    }
}
