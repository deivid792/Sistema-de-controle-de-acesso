using VisitorService.Domain.Services;
using VisitorService.Domain.ValueObject;

namespace VisitorService.Infrastructure.Services
{
    public class PasswordService : IPasswordService
    {
        public Password Hash(string plainPassword)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(plainPassword);
            var passwordHash = Password.Create(hash);
            passwordHash.NotificationClear();
            return passwordHash;
        }

        public bool Verify(Password storedPassword, string passwordToCheck)
        {
            return BCrypt.Net.BCrypt.Verify(passwordToCheck, storedPassword.Value);
        }
    }
}
