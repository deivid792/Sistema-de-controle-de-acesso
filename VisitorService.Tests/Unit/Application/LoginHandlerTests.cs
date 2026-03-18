using Moq;
using VisitorService.Application.UseCases;
using VisitorService.Application.DTOS;
using VisitorService.Application.Interfaces;
using VisitorService.Domain.Entities;
using VisitorService.Domain.ValueObject;
using VisitorService.Domain.Services;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

public class LoginHandlerTests
{
    [Fact]
    public async Task Login_Should_Succeed_When_Credentials_Are_Valid()
    {
        var mockRepo = new Mock<IUserRepository>();
        var mockPassword = new Mock<IPasswordService>();
        var mockAuth = new Mock<ITokenService>();
        var mockConfig = new Mock<IConfiguration>();

        var mockSection = new Mock<IConfigurationSection>();

        mockSection.Setup(s => s.Value).Returns("60");

        mockConfig
            .Setup(c => c.GetSection(It.IsAny<string>()))
            .Returns(mockSection.Object);

        mockConfig
            .Setup(c => c[It.IsAny<string>()])
            .Returns("60");

        var email = Email.Create("test@example.com");

        var storedHash = Password.Create("HASHFAKE123");

        var fakeUser = User.Create(
            Name.Create("Deivid"),
            email,
            storedHash
        );

        mockRepo
            .Setup(r => r.GetByEmailAsync(email.Value!))
            .ReturnsAsync(fakeUser);

        mockPassword
            .Setup(p => p.Verify(storedHash, "Senha123!"))
            .Returns(true);

        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, fakeUser.Id.ToString()),
                new Claim(ClaimTypes.Email, email.Value!),
                new Claim(ClaimTypes.Role, "Admin")
            };

        mockAuth
            .Setup(a => a.GenerateAccessToken(It.IsAny<IEnumerable<Claim>>()))
            .Returns("fake-token-123");

        var handler = new LoginHandler(mockRepo.Object, mockPassword.Object, mockAuth.Object, mockConfig.Object);

        var command = new LoginCommand
        {
            Email = "test@example.com",
            Password = "Senha123!"
        };

        var result = await handler.Handle(command);

        Assert.True(result.IsSuccess);
        Assert.Equal("fake-token-123", result.Value!.AccessToken);
    }

}
