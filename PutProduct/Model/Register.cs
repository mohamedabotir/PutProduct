using System.ComponentModel.DataAnnotations;

namespace PutProduct.Model
{
    public class Register
    {
       
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
        public Register(string userName, string password, string email, string phone)
        {
            UserName = userName;
            Password = password;
            Email = email;
            Phone = phone;
        }
    }
}
