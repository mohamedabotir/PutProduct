using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PutProduct.Data;
using PutProduct.Infrastructure.Extensions;
using PutProduct.Model;

namespace PutProduct.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize]
        [Route(nameof(Create))]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody]ProductModel product) {
            var userId = User.GetUserId();
            var prod = new Product ( 
            description:product.Description,
            quantity:product.Quantity,
            name: product.Name,
             price:product.Price,
            categoryId:product.CategoryId,
            userId:userId,
            imageUrl: product.ImageUrl
            );
            _context.Products?.Add(prod);
            await _context.SaveChangesAsync();
            return Ok(prod.Id);
        }
    }
}
