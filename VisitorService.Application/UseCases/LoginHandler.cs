using System.Linq;
using System.Threading.Tasks;
using VisitorService.Application.DTOS;
using VisitorService.Application.Interfaces;
using VisitorService.Application.Shared.results;
using VisitorService.Domain.ValueObject;
using VisitorService.Domain.Services;


namespace VisitorService.Application.UseCases
{
    public class LoginHandler : IloginHandler
{
    private readonly IUserRepository _userRepo;
    private readonly IPasswordService _passwordService;
    private readonly IAuthService _authService;

    public LoginHandler(IUserRepository userRepo, IPasswordService passwordService, IAuthService authService)
    {
        _userRepo = userRepo;
        _passwordService = passwordService;
        _authService = authService;
    }

    public async Task<Result<AuthResultDto>> Handle(LoginCommand command)
    {
        var emailResult = Email.Create(command.Email);
        if (emailResult.HasErrors)
            return Result<AuthResultDto>.Fail("Email inválido");

        var user = await _userRepo.GetByEmailAsync(emailResult.Value!);
        if (user is null)
            return Result<AuthResultDto>.Fail("Credenciais inválidas.");

        var ok = _passwordService.Verify(user.Password, command.Password);
        if (!ok)
            return Result<AuthResultDto>.Fail("Credenciais inválidas.");

        var roles = user.Roles?.Select(ur => ur.Name.ToString()) ?? Enumerable.Empty<string>();

        var token = await _authService.GenerateTokenAsync(user.Id, user.Email.Value, roles!);

        var dto = new AuthResultDto
        {
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddHours(8)
        };

        return Result<AuthResultDto>.Success(dto);
    }
}
}