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
        public int CategoryId { get; set; }
        [Required]
        public string? UserId { get; set; }
        public User? User { get; set; }
        public string ImageUrl { get; set; }
        public Product(string description, int quantity, string name, decimal price, int categoryId, string? userId, string imageUrl)
        {
            
            Description = description;
            Quantity = quantity;
            Name = name;
            Price = price;
            CategoryId = categoryId;
            UserId = userId;
            this.ImageUrl = imageUrl;
        }

        public Product()
        {
            
        }
    }
    public enum ProductType {
        Electronic,Book,Computer,GraphicCard,Other
    };
}
