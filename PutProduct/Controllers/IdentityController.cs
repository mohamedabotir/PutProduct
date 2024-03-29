﻿
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PutProduct.abstracts.Repository;
using PutProduct.abstracts.Services;
using PutProduct.Data;
using PutProduct.Data.Migrations;
using PutProduct.Infrastructure.Extensions;
using PutProduct.Model;
using PutProduct.Services.jwt;

namespace PutProduct.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<User> _manager;
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwt;
        private readonly IIdentityService _user;

         
        public IdentityController(IUserRepository userRepository, UserManager<User> manager,IJwtService jwt,IIdentityService user)
        {
            this._manager = manager; 
            this._jwt= jwt;
            _user= user;
            _userRepository = userRepository;

        }
        [Route(nameof(Register))]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody]Register user) {
            User member = new User {
                Email = user.Email,
            UserName = user.UserName,
            PhoneNumber = user.Phone,
            profile = new Profile()
            {
                EmailAddress = user.Email,
                Name = user.UserName
            }
           };
            var result = await _manager.CreateAsync(member, user.Password);
            if (result.Succeeded)
                return Ok();
            return BadRequest(result);
        
        }
        [Route(nameof(Login))]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody]RequestUser request) {
            var result = _manager.Users.FirstOrDefault(x => x.Email == request.Email);
            if (result == null) {
                return NotFound();
            }
           
            var check = await _manager.CheckPasswordAsync(result, request.Password);
            if(check==false)
                return NotFound();
            var token = _jwt.JwtGenerate(result.Id, result.UserName, result.Email);


            return Ok(token);
           
            
        }

         [HttpGet]
         [Authorize]
        public async Task<IActionResult> Get() {
            
            return Content("hello");
        }
        [HttpGet]
        [Authorize]
        [Route(nameof(GetUserId))]
        public IActionResult GetUserId()
        {
            var user= _user.GetUserId();

            return Ok(user);
        }

        [Route(nameof(checkUserName))]
        [HttpGet]
        public async Task<IActionResult> checkUserName(string name)
        {
            var user = await _userRepository.checkUsername(name);
            return Ok(user);
        }

        [Route(nameof(checkEmail))]
        [HttpGet]
        public async Task<IActionResult> checkEmail(string email)
        {
            var user = await _userRepository.checkEmailAddress(email);
            return Ok(user);
        }
    }
}
