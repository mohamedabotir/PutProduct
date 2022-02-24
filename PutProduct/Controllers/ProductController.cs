
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PutProduct.abstracts.Repository;
using PutProduct.Data;
using PutProduct.Infrastructure.Extensions;
using PutProduct.Model;

namespace PutProduct.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;


        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [Authorize]
        [Route(nameof(Create))]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody]ProductModel product) {

            var userId = User.GetUserId();
            product.UserId = userId;
            var result = await _productRepository.CreateProduct(product, userId);
            return Ok(result);
        } 
        [Authorize]
        [Route(nameof(MyProducts))]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> MyProducts() {

            var userId = User.GetUserId();
            var result =await _productRepository.RetrieveMyProducts(userId);
            return Ok(result);
        }
    }
}
