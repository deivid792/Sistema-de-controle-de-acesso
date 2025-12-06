using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using VisitorService.Application.DTOS;
using VisitorService.Application.Interfaces;
using VisitorService.Interfaces.Controllers;
using VisitorService.Application.Shared.results;
using Xunit;

public class AuthControllerTests
{
    [Fact]
    public async Task LoginRoute_Should_Return_200_When_Handler_Returns_Success()
    {
        // Arrange
        var mockRegister = new Mock<IRegisterVisitorHandler>();
        var mockLogin = new Mock<IloginHandler>();

        mockLogin
            .Setup(l => l.Handle(It.IsAny<LoginCommand>()))
            .ReturnsAsync(Result<AuthResultDto>.Success(
                new AuthResultDto
                {
                    Token = "test-token",
                    ExpiresAt = DateTime.UtcNow.AddHours(8)
                }
            ));

        var controller = new AuthController(mockRegister.Object, mockLogin.Object);

        var command = new LoginCommand
        {
            Email = "test@example.com",
            Password = "Senha123!"
        };

        // Act
        var response = await controller.Login(command);

        // Assert
        var ok = Assert.IsType<OkObjectResult>(response);
        var dto = Assert.IsType<AuthResultDto>(ok.Value);

        Assert.Equal("test-token", dto.Token);
    }
}
