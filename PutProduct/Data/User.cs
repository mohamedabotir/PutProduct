﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PutProduct.Data
{
    public class User : IdentityUser
    {
        
 
        
        public ICollection<Product> Products { get; set; }
        public User()
        {
           
            Products = new List<Product>();
        }

        
    }
}
