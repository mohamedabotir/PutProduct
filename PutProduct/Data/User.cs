using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PutProduct.Data
{
    public class User : IdentityUser
    {
        [Required]
        public string name { get; set; }
        [Required]
        [JsonIgnore]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
