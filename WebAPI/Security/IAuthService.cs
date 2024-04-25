using WebAPI.Models;

namespace WebAPI.Security
{
    public interface IAuthService
    {
        bool VerifyPassword(string EnteredPassword, string userPassword);

        bool IsEmailValid(string  Email);  
        
        bool IsPasswordValid(string Password);

        string HashPassword(string Password);
    }
}
