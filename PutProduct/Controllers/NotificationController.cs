using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PutProduct.abstracts.Repository;
using PutProduct.Model;

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

        [HttpPost]
        [Route(nameof(MarkNotificationAsReaded))]
        public async Task<IActionResult> MarkNotificationAsReaded([FromBody] IEnumerable<NotificationModel> model )
        {
            var result = await _notificationRepository.MarkItAsRead(model);

            return Ok(result);
        }

        [HttpGet]
        [Route(nameof(GetNotificationsCount))]
        public async Task<IActionResult> GetNotificationsCount()
        {
            var count = await _notificationRepository.GetUnReadedNotificationCount();

            return Ok(count);
        }
    }
}
