using System.ComponentModel.DataAnnotations;

namespace PutProduct.Model
{
    public class RequestUser
    {
       

        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public RequestUser(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
