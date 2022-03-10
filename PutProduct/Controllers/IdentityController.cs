
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PutProduct.abstracts.Services;
using PutProduct.Data;
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
     
        private readonly IJwtService _jwt;
        private readonly IIdentityService _user;

         
        public IdentityController(UserManager<User> manager,IJwtService jwt,IIdentityService user)
        {
            this._manager = manager; 
            this._jwt= jwt;
            _user= user;
           
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
    }
}
