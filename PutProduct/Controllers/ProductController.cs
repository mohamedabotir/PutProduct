using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using PutProduct.Cores.Repositories;
using PutProduct.Data;
using PutProduct.Infrastructure.Extensions;
=======
using PutProduct.abstracts.Repository;
using PutProduct.abstracts.Services;
>>>>>>> 379e267fae2a188d12179926643ecb260907ea7c
using PutProduct.Model;

namespace PutProduct.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
<<<<<<< HEAD
       
        private readonly  ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
           
            _context = context; 
           
=======
        private readonly IProductRepository _productRepository;
        private readonly IIdentityService _user;

        public ProductController(IProductRepository productRepository,IIdentityService user)
        {
            _productRepository = productRepository;
            _user = user;
>>>>>>> 379e267fae2a188d12179926643ecb260907ea7c
        }


        [Authorize]
        [Route(nameof(Create))]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
<<<<<<< HEAD
        public async Task<IActionResult> Create([FromBody]ProductModel product)
        {
            var userId = User.GetUserId();
            var prod = new Product(
                description: product.Description,
                quantity: product.Quantity,
                name: product.Name,
                price: product.Price,
                categoryId: product.CategoryId,
                userId: userId,
                imageUrl: product.ImageUrl
            );
            _context.Products?.Add(prod);
            await _context.SaveChangesAsync();
            return Ok(prod.Id);
=======
        public async Task<IActionResult> Create([FromBody]ProductModel product) {

            var userId = _user.GetUserId();
            product.UserId = userId;
            product.UserName = _user.GetUserName();
            var result = await _productRepository.CreateProduct(product, userId);
            return Ok(result);
        } 


        [Authorize]
        [Route(nameof(MyProducts))]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MyProducts() {

            var userId = _user.GetUserId();
            var result =await _productRepository.RetrieveMyProducts(userId);
            return Ok(result);
        }


        [Authorize]
        [Route(nameof(Update))]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody]ProductModel product) {

            var userId = _user.GetUserId();
            
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
        [Authorize]
        public async Task<IActionResult> RemoveProduct(int productId) {
            var userId = _user.GetUserId();
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
            var userId = _user.GetUserId();
            var result = await _productRepository.RetrieveSpecificProduct(id);
            
            if (result.Id == 0)
                return NotFound("The Product is not Available");
            return Ok(result);
>>>>>>> 379e267fae2a188d12179926643ecb260907ea7c
        }
    }
}
