using BCrypt.Net;
using VisitorService.Domain.Services;
using VisitorService.Domain.ValueObject;

namespace VisitorService.Infrastructure.Services
{
    public class PasswordService : IPasswordService
    {
        public Password Hash(string plainPassword)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(plainPassword);
            return Password.Create(hash);
        }

        public bool Verify(Password storedPassword, string passwordToCheck)
        {
            return BCrypt.Net.BCrypt.Verify(passwordToCheck, storedPassword.Value);
        }
    }
}
