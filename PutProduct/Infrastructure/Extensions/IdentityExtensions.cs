using System.Security.Claims;

namespace PutProduct.Infrastructure.Extensions
{
    public static  class IdentityExtensions
    {
        public static string? GetUserId(this ClaimsPrincipal user) =>
            user.Claims.
            FirstOrDefault(c => c.Type == "UserId")?
            .Value;

        public static string? GetUserName(this ClaimsPrincipal user) =>
            user.Claims.
            FirstOrDefault(c => c.Type == "Name")?
            .Value;
    }
}
