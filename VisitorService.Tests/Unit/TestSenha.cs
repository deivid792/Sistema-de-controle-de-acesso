using BCrypt.Net;
using Xunit;

public class PasswordTests
{
    [Fact]
    public void Verify_AdminPassword_HashMatches()
    {
        string plainPassword = "Admin123";
        string hash = BCrypt.Net.BCrypt.HashPassword(plainPassword);

        bool isMatch = BCrypt.Net.BCrypt.Verify(plainPassword, hash);

        Assert.True(isMatch, "A senha não corresponde ao hash armazenado");
    }
}
