using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Primitives;
using PutProduct.abstracts.Services;

namespace PutProduct.Hubs
{
    public class NotificationHub:Hub<IHub>
    {
        private readonly IIdentityService _identityService;
        private readonly string userId;
        public NotificationHub(IIdentityService identityService)
        {
            _identityService = identityService;
            userId = _identityService.GetUserId();
        }
        public string GetUserId() => userId;
    }
}
