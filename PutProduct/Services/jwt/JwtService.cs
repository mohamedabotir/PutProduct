using System.Globalization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PutProduct.Services.jwt
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _conf;
        public JwtService(IConfiguration conf)
        {
            _conf = conf;
        }
        public string JwtGenerate(string userId, string userName,string email)
        {
            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                        new Claim("UserId", userId),
                        new Claim("Name",userName),
                        new Claim("Email", email)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_conf["AppSettings:Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_conf["AppSettings:Issuer"],
                _conf["AppSettings:Issuer"], claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);
            return (new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
