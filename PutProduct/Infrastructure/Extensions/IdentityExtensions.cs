using System.Security.Claims;
using PutProduct.abstracts.Services;

namespace PutProduct.Infrastructure.Extensions
{
    public    class IdentityExtensions : IIdentityService
    {
        private readonly ClaimsPrincipal? _user;

        public IdentityExtensions(IHttpContextAccessor accessor)
        {
            _user = accessor.HttpContext?.User;
        }
       

        public string? GetUserId() =>
            _user?.Claims.
                FirstOrDefault(c => c.Type == "UserId")?
                .Value;

        public string? GetUserName() =>
            _user?.Claims.
                FirstOrDefault(c => c.Type == "Name")?
                .Value;
    }
}
