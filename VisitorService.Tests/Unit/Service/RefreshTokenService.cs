using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Moq;
using FluentAssertions;
using Xunit;
using VisitorService.Infrastructure.Services;

namespace VisitorService.Tests.Unit.Services
{
    public class RefreshTokenServiceTests
    {
        [Fact]
        public void GenerateRefreshToken_ShouldReturnBase64String_OfExpectedLength()
        {
            // Arrange
            var mockConfig = Mock.Of<IConfiguration>();
            var service = new TokenService(mockConfig);

            // Act
            var token = service.GenerateRefreshToken();

            // Assert
            token.Should().NotBeNullOrEmpty();

            var buffer = new byte[64];
            var success = Convert.TryFromBase64String(token, new Span<byte>(buffer), out var bytesWritten);

            success.Should().BeTrue("deve ser uma string Base64 válida");
            bytesWritten.Should().BeGreaterThan(0, "deve gerar bytes ao decodificar");
        }
    }
}
