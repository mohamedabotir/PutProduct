using System.ComponentModel.DataAnnotations;

namespace PutProduct.Model
{
    public class CommentModel
    {
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public int ProductId { get; set; }

        public string? Name { get; set; }

        public DateTime CommentDateTime { get; set; }

        

    }
}
