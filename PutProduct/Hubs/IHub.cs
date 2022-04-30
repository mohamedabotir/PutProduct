using PutProduct.Model;

namespace PutProduct.Hubs
{
    public interface IHub
    {
        Task BroadcastMessage();
        Task BroadcastNotification(NotificationModel notification);
    }
}
 