using System.ComponentModel.DataAnnotations;

namespace PutProduct.Data
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        
        [Required]
        public int categoryId { get; set; }
        [Required]
        public string userId { get; set; }
        public User user { get; set; }
        public string imageUrl { get; set; }
    }
    public enum ProductType {
        Electronic,Book,Computer,GraphicCard,Other
    };
}
