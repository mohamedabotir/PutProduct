 
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PutProduct.Data;
using PutProduct.Model;
using PutProduct.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PutProduct.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<User> manager;
        private readonly IConfiguration _conf;
         
        public IdentityController(UserManager<User> manager,IConfiguration _conf)
        {
            this.manager = manager; 
            this._conf = _conf;
           
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
            var result = manager.Users.FirstOrDefault(x => x.Email == request.Email);
            if (result == null) {
                return NotFound();
            }
           
            var check = manager.CheckPasswordAsync(result, request.Password);
            if(check==null)
                return NotFound();
            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", result.Id.ToString()),
                        new Claim("Name", result.UserName),
                        new Claim("Email", result.Email)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_conf["AppSettings:Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_conf["AppSettings:Issuer"],
                _conf["AppSettings:Issuer"], claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials:credentials);
          
            
            return Ok( new JwtSecurityTokenHandler().WriteToken(token));
           
            
        }

         [HttpGet]
         [Authorize]
        public IActionResult get() {
            var resu = HttpContext.Request.Headers.Authorization;
            return Content("hello");
        }
    }
}
