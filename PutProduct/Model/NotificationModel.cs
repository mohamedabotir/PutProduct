namespace PutProduct.Model
{
    public class NotificationModel
    {
        public int Id { get; set; }
        public string? SenderId { get; set; }
        public string ReceiverId { get; set; }

        public string? SenderName { get; set; }
        public string? ReceiverName { get; set; }
        public string Message { get; set; }
        public bool isRead { get; set; }
        public int ProductId { get; set; }
    }
}
