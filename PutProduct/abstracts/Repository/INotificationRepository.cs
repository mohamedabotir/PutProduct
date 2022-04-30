using PutProduct.Hubs;
using PutProduct.Model;

namespace PutProduct.abstracts.Repository
{
    public interface INotificationRepository
    {
        Task<IEnumerable<NotificationModel>> GetNotifications();
    }
}
