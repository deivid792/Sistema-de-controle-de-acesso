using System.Linq;
using System.Threading.Tasks;
using VisitorService.Application.DTOS;
using VisitorService.Application.Interfaces;
using VisitorService.Domain.Shared.results;
using VisitorService.Domain.ValueObject;

namespace VisitorService.Application.UseCases
{
    public class LoginHandler
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
        var emailResult = Email.create(command.Email);
        if (!emailResult.IsSuccess)
            return Result<AuthResultDto>.Fail(emailResult.Error!);

        var user = await _userRepo.GetByEmailAsync(emailResult.Value!.Value);
        if (user is null)
            return Result<AuthResultDto>.Fail("Credenciais inválidas.");

        var ok = _passwordService.Verify(user.Password, command.Password);
        if (!ok)
            return Result<AuthResultDto>.Fail("Credenciais inválidas.");

        var roles = user.UserRoles?.Select(ur => ur.Role.Name.ToString()) ?? Enumerable.Empty<string>();

        var token = await _authService.GenerateTokenAsync(user.Id, user.Email.Value, roles);

        var dto = new AuthResultDto
        {
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddHours(8)
        };

        return Result<AuthResultDto>.Success(dto);
    }
}
}