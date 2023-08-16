using Microsoft.IdentityModel.Tokens;
using Solution.Core.Features.UsersCQ.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Employee_CleanPattern.Helper
{
    public class CreateJwtToken
    {
        private readonly IConfiguration _iconfiguration;

        public CreateJwtToken(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }

        public string CreateToken(int id, string name)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_iconfiguration["JWT:Key"]);
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, name),
                new Claim("Id",id.ToString())
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = credentials
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            return jwtTokenHandler.WriteToken(token);
        }
    }
}
