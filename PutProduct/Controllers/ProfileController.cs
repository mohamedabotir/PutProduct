using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PutProduct.abstracts.Services;
using PutProduct.Data;
using PutProduct.Model;
using PutProduct.Services;
using Profile = PutProduct.Data.Profile;

namespace PutProduct.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ApplicationDbContext _ctx;
        private readonly IMapper _map;
        private readonly IIdentityService _user;
        public ProfileController(ApplicationDbContext context,IMapper map,IIdentityService user)
        {
            _ctx = context;
            _map = map;
            _user = user;
            
        }
        [Route(nameof(Index)+"/{id}")]
        [HttpGet()]
        public async Task<IActionResult> Index(string id)
        {
            var user = await _ctx.User!.FirstOrDefaultAsync(e => e.Id == id);
            var profile = user?.profile;
            if (user == null)
                return NotFound();
            var result =_map.Map<Profile,ProfileModel>(profile!);
            return Ok(result);
        }
        [Microsoft.AspNetCore.Authorization.Authorize]
        [Route(nameof(UpdateProfile))]
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(Profile profile)
        {
            var userId = _user.GetUserId();
            var user = _ctx.User!.FirstOrDefault(e => e.Id == userId);
            if (userId == null)
            {
                return NotFound();
            }

            user!.profile = profile;
            _ctx.Update(user);
             _ctx!.SaveChanges();
            return Ok(new {message="Successfully Updated"});
        }
    }
}
