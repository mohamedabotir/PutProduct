using PutProduct.Services.Attributes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PutProduct.Data;
using PutProduct.Model;
using PutProduct.Services.Jwt;

namespace PutProduct.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<User> manager;
        private readonly IUserService Auth;
        public IdentityController(UserManager<User> manager,IUserService Auth)
        {
            this.manager = manager; 
            this.Auth = Auth;
        }
        [Route(nameof(Register))]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]Register user) {
            User member = new User { 
            name = user.UserName,
            Phone = user.Phone,
            Email = user.Email,
            UserName = user.UserName,
            PhoneNumber = user.Phone,
            Password = user.Password,
            
            };
            var result = await manager.CreateAsync(member, user.Password);
            if (result.Succeeded)
                return Ok();
            return BadRequest(result);
        
        }
        [Route(nameof(Login))]
       [HttpPost]
        public async Task<IActionResult> Login([FromBody]RequestUser request) {
           
       var result = Auth.Authenticate(request);
            if (result.Token == null) { 
            return Unauthorized();
            }
            return Ok(result);
        }
        [Authorize]
        public IActionResult get() {
            var data = HttpContext.Items["User"];
            return Content("hello"+data);
        }
    }
}
