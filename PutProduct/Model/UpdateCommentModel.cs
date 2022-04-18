using System.ComponentModel.DataAnnotations;

namespace PutProduct.Model
{
    public class UpdateCommentModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
