using System.ComponentModel.DataAnnotations;
using PutProduct.abstracts.Models;

namespace PutProduct.Data
{
    public class Product :IDeleteEntity
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
        public ICollection<Comment> Comments { get; set; }
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
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
    public enum ProductType {
        Electronic,Book,Computer,GraphicCard,Other
    };
}
