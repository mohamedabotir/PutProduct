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
        public int CategoryId { get; set; }
         
        public string ImageUrl { get; set; }
        public ProductModel(string description, int quantity, string name, decimal price, int categoryId, string imageUrl)
        {
            Description = description;
            Quantity = quantity;
            Name = name;
            Price = price;
            this.CategoryId = categoryId;
            this.ImageUrl = imageUrl;
        }


    }
}
