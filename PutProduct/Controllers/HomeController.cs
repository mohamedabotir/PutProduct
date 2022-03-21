using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PutProduct.abstracts.Services;


namespace PutProduct.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IIdentityService _user;

        public HomeController(IIdentityService user)
        {
            _user = user;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Get() {
            var userId = _user.GetUserId();

            return Ok("Work:  "+userId);

        }
    }
}
