using Xunit;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Security.Claims;
using VisitorService.Infrastructure.Services;
using Microsoft.Extensions.Configuration.Memory;


namespace VisitorService.Tests.Services
{
    public class TokenServiceTests
    {
        private readonly TokenService _tokenService;

        public TokenServiceTests()
        {
            var inMemorySettings = new Dictionary<string, string?>
            {
                { "JWT:SecretKey", "supersecretkeyforsigningtokens12345" },
                { "JWT:TokenValidityInMinutes", "30" },
                { "JWT:ValidIssuer", "test-issuer" },
                { "JWT:ValidAudience", "test-audience" }
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _tokenService = new TokenService(configuration);
        }

        [Fact]
        public void GenerateAccessToken_ShouldReturn_ValidJwtToken()
        {
            // Arrange
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "123"),
                new Claim(ClaimTypes.Email, "user@example.com")
            };

            // Act
            var token = _tokenService.GenerateAccessToken(claims);

            // Assert
            token.Should().NotBeNullOrEmpty("because a valid token should be returned");
            token.Split('.').Length.Should().Be(3, "because a JWT must have header, payload, and signature");
        }
    }
}
