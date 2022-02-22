
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PutProduct.Data;
using PutProduct.Model;
using PutProduct.Services;
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
         
        public IdentityController(UserManager<User> manager,IJwtService jwt)
        {
            this._manager = manager; 
            this._jwt= jwt;
           
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
        public IActionResult Get() {
            
            return Content("hello");
        }
    }
}
