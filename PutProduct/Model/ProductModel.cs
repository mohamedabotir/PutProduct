using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PutProduct.Model
{
    public class ProductModel
    {

        public int Id  { get; set; }
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
<<<<<<< HEAD
        [JsonIgnore]
        public string UserId { get; set; }

        public ProductModel(string description, int quantity, string name, decimal price, int categoryId, string imageUrl)
=======
        
        public string? UserId { get; set; }
        public string? UserName { get; set; }      
        public int qty { get; set; }

<<<<<<< HEAD
        public ProductModel(string description, int quantity, string name, decimal price, int categoryId, string imageUrl,string? userId,string? userName,int qty)
=======
        public ProductModel(string description, int quantity, string name, decimal price, int categoryId, string imageUrl,string? userId,string? userName)
>>>>>>> 379e267fae2a188d12179926643ecb260907ea7c
>>>>>>> d1b3b9f2a824929312ae40626be27db3f1e95974
        {
            Description = description;
            Quantity = quantity;
            Name = name;
            Price = price;
            this.CategoryId = categoryId;
            this.ImageUrl = imageUrl;
            UserId = userId;
            UserName = userName;
            this.qty = qty;
        }

        public ProductModel()
        {
                
        }

        public ProductModel()
        {
                
        }


    }
}
