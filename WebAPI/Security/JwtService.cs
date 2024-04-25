using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Models;

namespace WebAPI.Security
{
    public class JwtService(IConfiguration configuration) : IJwtService
    {
        private readonly IConfiguration _configuration = configuration;

        public string GenerateToken(User user)
        {
            var tokenClaims = GetTokenClaims(user);
            var tokenSigningCredentials = GetTokenSigningCredentials();           
            var jwtToken = GenerateJwtToken(tokenSigningCredentials, tokenClaims);
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        private SigningCredentials GetTokenSigningCredentials()
        {
            var key = _configuration.GetSection("Jwt").GetSection("Key");
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key.Value!));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private List<Claim> GetTokenClaims(User user)
        {
            var tokenClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user!.Email),
                new Claim(ClaimTypes.Role, user!.Role.ToString())
            };

            return tokenClaims;
        }

        private JwtSecurityToken GenerateJwtToken(SigningCredentials tokenSigningCredentials, List<Claim> tokenClaims)
        {
            var settings = _configuration.GetSection("Jwt");

            var expireDateTime = DateTime.UtcNow.AddMinutes(Convert.ToDouble(settings.GetSection("Expires").Value));

            var jwtToken = new JwtSecurityToken(
                issuer: settings.GetSection("Issuer").Value,
                claims: tokenClaims,
                signingCredentials: tokenSigningCredentials,
                expires: expireDateTime
                );

            return jwtToken;
        }

        public string GetEmailFromToken(ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
        }

        public string GetRoleFromToken(ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.First(claim => claim.Type == ClaimTypes.Role).Value;
        }
    }
}
