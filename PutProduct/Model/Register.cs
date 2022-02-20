using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PutProduct.Model
{
    public class Register
    {
       
        public string UserName { get; set; }
        [Required]
        
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
