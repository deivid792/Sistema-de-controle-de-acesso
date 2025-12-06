using Moq;
using System.Threading.Tasks;
using VisitorService.Application.UseCases;
using VisitorService.Application.DTOS;
using VisitorService.Application.Interfaces;
using VisitorService.Domain.Entities;
using VisitorService.Domain.ValueObject;
using Xunit;
using VisitorService.Domain.Services;

public class LoginHandlerTests
{
    [Fact]
    public async Task Login_Should_Succeed_When_Credentials_Are_Valid()
    {
        // Arrange
        var mockRepo = new Mock<IUserRepository>();
        var mockPassword = new Mock<IPasswordService>();
        var mockAuth = new Mock<IAuthService>();

        var email = Email.Create("test@example.com");

        // simula o hash salvo no banco
        var storedHash = Password.FromHash("HASHFAKE123");

        var fakeUser = User.Create(
            Name.Create("Deivid"),
            email,
            storedHash
        );

        mockRepo
            .Setup(r => r.GetByEmailAsync(email.Value))
            .ReturnsAsync(fakeUser);

        mockPassword
            .Setup(p => p.Verify(storedHash, "Senha123!"))
            .Returns(true);

        mockAuth
            .Setup(a => a.GenerateTokenAsync(fakeUser.Id, email.Value, It.IsAny<IEnumerable<string>>()))
            .ReturnsAsync("fake-token-123");

        var handler = new LoginHandler(mockRepo.Object, mockPassword.Object, mockAuth.Object);

        var command = new LoginCommand
        {
            Email = "test@example.com",
            Password = "Senha123!"
        };

        var result = await handler.Handle(command);

        Assert.True(result.IsSuccess);
        Assert.Equal("fake-token-123", result.Value.Token);
    }

}
