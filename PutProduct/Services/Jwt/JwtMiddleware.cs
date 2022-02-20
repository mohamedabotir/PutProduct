using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PutProduct.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace PutProduct.Services.Jwt
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSetting _appSettings;
       
        public JwtMiddleware(RequestDelegate next,IOptions<AppSetting> options)
        {
            _next = next;
            _appSettings = options.Value;
            
        }
        public async Task Invoke(HttpContext context,IUserService userService) { 
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
                attachUserToContext(context, userService, token);
            await _next(context);
        }
        private async void attachUserToContext(HttpContext context, IUserService userService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = (jwtToken.Claims.First(x => x.Type == "id").Value);

                // attach user to context on successful jwt validation
                var user = default(object);
                User u=null;
                var task = Task.Factory.StartNew(() => {
                user= userService.GetById(userId);
                });
                Task.WaitAll(task);
                
                 context.Items["User"] = user;
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
           
        }
        
        }
}
