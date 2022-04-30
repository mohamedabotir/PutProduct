using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PutProduct.abstracts.Repository;

namespace PutProduct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationController(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        [HttpGet]
        [Route(nameof(GetNotifications))]
        public async Task<IActionResult> GetNotifications()
        {

            var notifications = await _notificationRepository.GetNotifications();
            return Ok(notifications);
        }
    }
}
