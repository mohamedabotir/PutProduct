using PutProduct.abstracts.Models;

namespace PutProduct.Data
{
    public class Comment :IDeleteEntity
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }

        public Product Product { get; set; }

        public int ProductId { get; set; }
        public DateTime CommentDateTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
