using System.Security.Claims;
using WebAPI.Models;

namespace WebAPI.Security
{
    public interface IJwtService
    {
        string GenerateToken(User user);
        string GetEmailFromToken(ClaimsPrincipal claimsPrincipal);
        string GetRoleFromToken(ClaimsPrincipal claimsPrincipal);
    }
}
