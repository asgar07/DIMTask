using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApp.Models;
using WebApp.Service.Interfaces;

namespace WebApp.Service
{
    public class TokenService:ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateJwtToken(AppUser user, List<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),

         



                    //new Claim(ClaimTypes.UserData,user.Lang),
                    //new Claim(ClaimTypes.UserData,user?.CardNo),
                    //  new Claim(ClaimTypes.NameIdentifier,Id)
            };

            
                claims.Add(new Claim(JwtRegisteredClaimNames.Name, user.UserName));
            

            roles.ForEach(role =>
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            });

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddYears(1);
            // var expires = DateTime.UtcNow.AddSeconds(2);




            var token = new JwtSecurityToken(
                _configuration["Jwt:Audience"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: expires,
               signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
