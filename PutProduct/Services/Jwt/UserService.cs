using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PutProduct.Data;
using PutProduct.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PutProduct.Services.Jwt
{
    public class UserService : IUserService
    {
        private readonly  UserManager<User> user;
        private readonly AppSetting appSetting;
        public UserService(UserManager<User> user, IOptions<AppSetting> app)
        {
            this.user = user;
            appSetting = app.Value;
        }
        
        public ResponseUser Authenticate(RequestUser model)
        {
            var check = user.FindByNameAsync(model.UserName);
            if (check == null) {
                return null;
            }
            var member = new User
            {
                UserName = model.UserName,
                Password = model.Password,
            };
            var result = user.CheckPasswordAsync(member, model.Password);
            if (result == null) {
                return null;
            }
            var token = generateJwtToken(member);

            return new ResponseUser(member, token);
        }

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(appSetting.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetById(string id)
        {
            User U =default;
            var t = Task.Factory.StartNew(() => {  
             U = user.FindByIdAsync(id).Result;
            }); 
            Task.WaitAll(t);
            
            return U;
            
        }
    }
}
