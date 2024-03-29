﻿using System.Net.Mime;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using PutProduct.abstracts.Repository;
using PutProduct.abstracts.Services;
using PutProduct.Hubs;
using PutProduct.Model;
using JsonConverter = Newtonsoft.Json.JsonConverter;

namespace PutProduct.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IIdentityService _user;
        

        public ProductController(IProductRepository productRepository,IIdentityService user)
        {
            _productRepository = productRepository;
            _user = user;
        }


        [Authorize]
        [Route(nameof(Create))]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        }
        [Route(nameof(Cart))]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> Cart([FromBody]OrderModel model) {
            var userId = _user.GetUserId();
       
            var result =  await _productRepository.AddOrder(model);
            
            if (result == false)
                return NotFound("The Product is not Available");
            return Ok(result);
        }

        [Route(nameof(OrderHistory))]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> OrderHistory()
        {
            var products = _productRepository.GetAllOrders();
            return Ok(products);
        }


        [HttpPost]
        [Authorize]
        [Route(nameof(Comment))]
        public async Task<IActionResult> Comment([FromBody] CommentModel comment)
        {
            var result = await _productRepository.Comment(comment);

            if (result == null)
                return BadRequest("Can't Comment");
            return Ok(result);
        }

        [HttpGet]
        [Route(nameof(GetComments))]
        public async Task<IActionResult> GetComments(int id)
        {
            var result = await _productRepository.GetProductComment(id);

            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route(nameof(GetComment))]
        public async Task<IActionResult> GetComment(int id)
        {
            var result = await _productRepository.GetComment(id);
            if (result == null)
                return NotFound("Comment NotFound");
            return Ok(result);
        }
        [Authorize]
        [HttpPut]
        [Route(nameof(UpdateComment))]
       
        public async Task<IActionResult> UpdateComment([FromBody]UpdateCommentModel comment)
        {
            var result = await _productRepository.UpdateComment(comment);
            if (result == false)
                return NotFound("Comment NotFound!");
            
            return Ok( new {message="Comment Updated Successfully"});
        }
        [Authorize]
        [HttpDelete]
        [Route(nameof(DeleteComment))]
       
        public async Task<IActionResult> DeleteComment(int id)
        {
            var result = await _productRepository.DeleteComment(id);
            if (result == false)
                return NotFound("Comment NotFound!");
            
            return Ok(new {message= "Comment Deleted Successfully" });
        }


        [Route(nameof(checkPromoCode))]
        [HttpGet]
        public async Task<IActionResult> checkPromoCode(string code,decimal amount)
        {
            var result = await _productRepository.checkPromoCode(code, amount);
            if (result == 0)
                return Ok("PromoCode Wrong!");

            return Ok(result);
        }


        [Route(nameof(CreatePromoCode))]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePromoCode(PromoCodeModel model)
        {
            var result = await _productRepository.CreatePromoCode(model);
            if (result is false)
                return Ok(result);

            return Ok(result);
        }


        [Route(nameof(MostPopularProduct))]
        [HttpGet]
        [Produces("application/json")]
         
        public async Task<IActionResult> MostPopularProduct()
        {
           
            var result = await _productRepository.GetMostPopularProducts();

             
            return Ok(result.Keys);
        }

         
    }


}
