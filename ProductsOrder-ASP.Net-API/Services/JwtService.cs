using Microsoft.IdentityModel.Tokens;
using ProductsOrder_ASP.Net_API.Models;
using ProductsOrder_ASP.Net_API.Services.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductsOrder_ASP.Net_API.Services
{
    public class JwtService : IJwt
    {
        public readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(Users user)
        {
            //read from appsettings
            var secretKey = _configuration.GetSection("JwtOptions:SecretKey").Value;
            var audience = _configuration.GetSection("JwtOptions:Audience").Value;
            var issuer = _configuration.GetSection("JwtOptions:Issuer").Value;
            //key-secret key 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            //cred security algorithm
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //payload
           

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("Roles", user.Roles));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Name, user.Name));

            var tokendescriptor = new SecurityTokenDescriptor()
            {
                Issuer = issuer,
                Audience = audience,
                Expires = DateTime.UtcNow.AddHours(3),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = cred
            };

            var token = new JwtSecurityTokenHandler().CreateToken(tokendescriptor);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
