using VisitorService.Domain.ValueObject;

namespace VisitorService.Domain.Services
{
    public interface IPasswordService
{
    bool Verify(Password storedPassword, string passwordToCheck);

    Password Hash(string plainPassword);
}
}