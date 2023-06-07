using API.Contracts.Services;
using API.Models;
using API.Models.DB;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using System.Text;

namespace BeautyCRM.Utilities
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string secret;

        public JwtMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            this.secret = config["TokenKey"];
        }

        public async Task Invoke(HttpContext context, IUsersService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null && token != "null")
                attachUserToContext(context, userService, token);

            await _next(context);
        }

        private void attachUserToContext(HttpContext context, IUsersService userService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(this.secret);
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
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "nameid").Value);

                // attach user to context on successful jwt validation
                User user = userService.GetUserById(userId);
                context.Items["User"] = user;

                Thread.CurrentPrincipal = new GenericPrincipal(new DatingAppIdentity(user), new string[] { });
            }
            catch (Exception e)
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}