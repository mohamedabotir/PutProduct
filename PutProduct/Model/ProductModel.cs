using System.ComponentModel.DataAnnotations;

namespace PutProduct.Model
{
    public class ProductModel
    {
        [Required]
        [MaxLength(1200)]
        public string Description { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }

        [Required]
        public int categoryId { get; set; }
         
        public string imageUrl { get; set; }

    }
}
