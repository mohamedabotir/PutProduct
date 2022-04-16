namespace PutProduct.Data
{
    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }

        public Product Product { get; set; }

        public int ProductId { get; set; }
        public DateTime CommentDateTime { get; set; }
    }
}
