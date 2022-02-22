using PutProduct.Data;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PutProduct.Model
{
    public class ResponseUser
    {
        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        
         
        public string Token { get; set; }
        public ResponseUser(User user ,string token)
        {
            Id = user.Id;
            UserName = user.UserName;
            this.Token = token;
        
        }
    }
}
