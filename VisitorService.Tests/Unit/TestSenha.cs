using BCrypt.Net;
using Xunit;

public class PasswordTests
{
    [Fact]
    public void Verify_AdminPassword_HashMatches()
    {
        string plainPassword = "Admin123";
        string hash = "$2a$11$eWfJ7cek2y71rZPH7gtVueb5MhgPCXn4P0x7FwFjhvlShSYLJ7D9u";

        bool isMatch = BCrypt.Net.BCrypt.Verify(plainPassword, hash);

        Assert.True(isMatch, "A senha não corresponde ao hash armazenado");
    }
}
