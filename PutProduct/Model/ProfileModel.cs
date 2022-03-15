using System.ComponentModel.DataAnnotations;

namespace PutProduct.Model
{
    public class ProfileModel
    {
        [Required]
        public string Name { get; set; }
        [MaxLength(150)]
        public string? Bio { get; set; }
        public string? Website { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        public string? Location { get; set; }
        public string? ImageUrl { get; set; }
    }
}
