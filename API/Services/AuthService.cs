using API.Contracts;
using API.Models;
using API.Models.DB;
using API.Models.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services
{
    public class AuthService : IAuthService
    {
        private readonly SymmetricSecurityKey _key;
        public AuthService(IConfiguration config) {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public LoginResponseDTO Login(LoginUserDTO model)
        {
            var user = new LoginResponseDTO() { Username = "Kalin", Id = 1 };
            user.Token = CreateToken(user);

            return user;
        }

        public bool Register(RegisterUserDTO model)
        {
            throw new NotImplementedException();
        }

        public string CreateToken(LoginResponseDTO user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
