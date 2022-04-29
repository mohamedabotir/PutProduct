using PutProduct.abstracts.Models;

namespace PutProduct.Data
{
    public class Notification :IDeleteEntity
    {
        public int Id { get; set; }
        public string SenderId { get; set; }

        public User Sender  { get; set; }
        public string ReceiverId { get; set; }
        public User Receiver { get; set; }
        public string Message { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
