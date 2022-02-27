using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PutProduct.abstracts.Repository;
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


        [Authorize]
        [Route(nameof(Update))]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody]ProductModel product) {

            var userId = User.GetUserId();
            var result =await _productRepository.ModifyProduct(product,userId);
            if (result == 0)
                return BadRequest();
            return Ok(result);
        }

        [Route(nameof(Products))]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Products() {

            var result = await _productRepository.RetrieveAllProducts();
            return Ok(result);
        }


        [Route(nameof(RemoveProduct)+"/{productId}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveProduct(int productId) {
            var userId = User.GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var result = await _productRepository.DeleteProduct(userId, productId);
            if (result == 0)
                return NotFound("The Product is not Available");
            return Ok(result);
        }
        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Product(int id) {
            var userId = User.GetUserId();
            var result = await _productRepository.RetrieveSpecificProduct(id);
            
            if (result.Id == 0)
                return NotFound("The Product is not Available");
            return Ok(result);
        }
    }
}
