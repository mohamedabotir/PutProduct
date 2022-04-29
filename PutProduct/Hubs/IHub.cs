namespace PutProduct.Hubs
{
    public interface IHub
    {
        Task BroadcastMessage();
    }
}
