using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PutProduct.Data;
using PutProduct.Infrastructure.Extensions;
using PutProduct.Model;
using System.Security.Claims;

namespace PutProduct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly private ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(ProductModel product) {
            var userId = User.GetUserId();
            var prod = new Product { 
            Description = product.Description,
            Price = product.Price,
            categoryId=product.categoryId,
            imageUrl = product.imageUrl,
            Name = product.Name,
            Quantity = product.Quantity,
            userId = userId,
            };
            _context.Products.Add(prod);
            await _context.SaveChangesAsync();
            return Ok(prod.Id);
        }
    }
}
