using PutProduct.Hubs;
using PutProduct.Model;

namespace PutProduct.abstracts.Repository
{
    public interface INotificationRepository
    {
        Task<IEnumerable<NotificationModel>> GetNotifications();

        Task<bool> MarkItAsRead(IEnumerable<NotificationModel> model);

        Task<int> GetUnReadedNotificationCount();
    }
}
