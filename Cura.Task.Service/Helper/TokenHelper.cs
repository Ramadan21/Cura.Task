using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cura.Task.Service.Helper
{
    public class TokenHelper : ITokenHelper
    {
        private readonly IConfiguration _configuration;
        public TokenHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GenerateToken(List<Claim> claims)
        {
            string key = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var expTime =double.Parse( _configuration["Jwt:ExpTime"]);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);  
            var token = new JwtSecurityToken(issuer,  
                            issuer,    
                            claims,
                            expires:DateTime.Now.AddDays(expTime) );
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt_token;
        }
    }
}