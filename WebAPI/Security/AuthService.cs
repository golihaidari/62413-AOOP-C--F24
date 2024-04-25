using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace WebAPI.Security
{
    public class AuthService : IAuthService
    {
        private const int saltByteSize = 128 / 8;
        private const int keyByteSize = 256 / 8;
        private int iterationCount = 10000;
        private static readonly HashAlgorithmName _algorithName = HashAlgorithmName.SHA256;

        public bool VerifyPassword(string userPassword, string EnteredPassword)
        {
            var splitPassword = userPassword.Split(';');
            var passwordSalt = Convert.FromBase64String(splitPassword[0]);
            var userPasswordHash = Convert.FromBase64String(splitPassword[1]);

            var passwordHashToVerify = Rfc2898DeriveBytes.Pbkdf2(EnteredPassword, passwordSalt, iterationCount, _algorithName, keyByteSize);

            return CryptographicOperations.FixedTimeEquals(userPasswordHash, passwordHashToVerify);
        }

        public string HashPassword(string Password)
        {
            var passwordSalt = RandomNumberGenerator.GetBytes(saltByteSize);
            var passwordHash = Rfc2898DeriveBytes.Pbkdf2(Password, passwordSalt, iterationCount, _algorithName, keyByteSize);

            return string.Join(';', Convert.ToBase64String(passwordSalt), Convert.ToBase64String(passwordHash));
        }

        public bool IsEmailValid(string Email)
        {
            Regex validEmail = new("^(.+)@(.+)$");
            return validEmail.IsMatch(Email);
        }

        public bool IsPasswordValid(string Password)
        {
            Regex validPassword = new("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-/*_]).{12,}$");
            return validPassword.IsMatch(Password);
        }
    }
}
