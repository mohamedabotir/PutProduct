using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PutProduct.Infrastructure.Extensions;

namespace PutProduct.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public IActionResult Get() {
            var userId = User.GetUserId();

            return Ok("Work:  "+userId);

        }
    }
}
