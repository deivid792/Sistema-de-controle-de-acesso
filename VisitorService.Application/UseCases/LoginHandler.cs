using VisitorService.Application.DTOS;
using VisitorService.Application.Interfaces;
using VisitorService.Application.Shared.results;
using VisitorService.Domain.Services;
using System.Security.Claims;
using VisitorService.Domain.Enums;
using Microsoft.Extensions.Configuration;


namespace VisitorService.Application.UseCases
{
    public class LoginHandler : IloginHandler
{
    private readonly IUserRepository _userRepo;
    private readonly IPasswordService _passwordService;
    private readonly ITokenService _TokenService;
    private readonly IConfiguration _configuration;

    public LoginHandler(IUserRepository userRepo, IPasswordService passwordService, ITokenService tokenService, IConfiguration configuration)
    {
        _userRepo = userRepo;
        _passwordService = passwordService;
        _TokenService = tokenService;
        _configuration = configuration;
    }

    public async Task<Result<AuthResultDto>> Handle(LoginCommand command)
    {
        var user = await _userRepo.GetByEmailAsync(command.Email);
        if (user is null)
            return Result<AuthResultDto>.Fail("Credenciais inválidas.");

        var ok = _passwordService.Verify(user.Password, command.Password);
        if (!ok)
            return Result<AuthResultDto>.Fail("Credenciais inválidas.");

        var roles = user.Roles?.Select(ur => ur.Name.ToString()) ?? Enumerable.Empty<string>();

        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email.Value!),
                new Claim(ClaimTypes.Role, RoleType.Visitor.ToString())
            };

        var accessToken =  _TokenService.GenerateAccessToken(claims);
        var refreshToken = _TokenService.GenerateRefreshToken();

        var duration = _configuration.GetValue<int>("JWT:RefreshTokenValidityInDays");

        user.AddRefreshToken(refreshToken, duration);

        var dto = new AuthResultDto
        {
            AccessToken = accessToken,
            RefeshToken = refreshToken,
            ExpiresAccessTokenIn = DateTime.UtcNow.AddMinutes(15),
        };

        return Result<AuthResultDto>.Success(dto);
    }
}
}