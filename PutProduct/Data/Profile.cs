using System.ComponentModel.DataAnnotations;

namespace PutProduct.Data
{
    public class Profile
    {
        public string Name { get; set; }
        [MaxLength(150)]
        public string Bio { get; set; }
        public string Website { get; set; }
        public string EmailAddress { get; set; }
        public string Location { get; set; }
    }
}
