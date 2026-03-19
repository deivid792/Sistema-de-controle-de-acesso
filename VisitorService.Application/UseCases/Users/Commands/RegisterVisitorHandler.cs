using VisitorService.Domain.ValueObject;
using VisitorService.Domain.Enums;
using VisitorService.Domain.Entities;
using VisitorService.Application.DTOS;
using VisitorService.Application.Interfaces;
using VisitorService.Domain.Interfaces;
using VisitorService.Application.Shared.results;
using VisitorService.Domain.Services;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace VisitorService.Application.UseCases.Users.Commands
{
    public class RegisterVisitorHandler : IRegisterVisitorHandler
    {
        private readonly IUserRepository _userRepo;
        private readonly IRoleRepository _roleRepo;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public RegisterVisitorHandler(IUserRepository userRepo, IRoleRepository roleRepo, IPasswordService passwordService, ITokenService tokenService,
        IConfiguration configuration)
        {
            _userRepo = userRepo;
            _roleRepo = roleRepo;
            _passwordService = passwordService;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        public async Task<Result<AuthResultDto>> Handle(RegisterVisitorCommand command)
        {
            var nameResult = Name.Create(command.Name);
            if (nameResult.HasErrors)
                return Result<AuthResultDto>.Fail(nameResult.Errors);

            var emailResult = Email.Create(command.Email);
            if (emailResult.HasErrors)
                return Result<AuthResultDto>.Fail(emailResult.Errors);

            var existing = await _userRepo.GetByEmailAsync(emailResult.Value!);
            if (existing is not null)
                return Result<AuthResultDto>.Fail("E-mail já cadastrado.");

            var password = Password.Create(command.Password);
            if (password.HasErrors)
                return Result<AuthResultDto>.Fail(password.Errors!);
            var hashResult = _passwordService.Hash(password.Value!);

            var passwordVo = hashResult;

            Phone? phoneVo = null;
            if (!string.IsNullOrWhiteSpace(command.Phone))
            {
                var phoneRes = Phone.Create(command.Phone);
                if (phoneRes.HasErrors)
                    return Result<AuthResultDto>.Fail(phoneRes.Errors);
                phoneVo = phoneRes;
            }

            Cnpj? cnpjVo = null;
            if (!string.IsNullOrWhiteSpace(command.Cnpj))
            {
                var cnpjRes = Cnpj.Create(command.Cnpj);
                if (cnpjRes.HasErrors)
                    return Result<AuthResultDto>.Fail(cnpjRes.Errors);
                cnpjVo = cnpjRes;
            }

            var user = User.Create(
                nameResult,
                emailResult,
                passwordVo,
                command.Phone is null ? null : phoneVo,
                command.Company,
                cnpjVo
            );

            if (user.HasErrors)
                return Result<AuthResultDto>.Fail(user.Errors!);

            var visitorRole = await _roleRepo.GetByRoleTypeAsync(RoleType.Visitor);
            if (visitorRole is null)
                return Result<AuthResultDto>.Fail("Role padrão 'Visitor' não está criada no sistema.");

            user.AddRole(visitorRole);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email.Value!),
                new Claim(ClaimTypes.Role, RoleType.Visitor.ToString())
            };

            var acessToken = _tokenService.GenerateAccessToken(claims);

            var refreshToken = _tokenService.GenerateRefreshToken();

            var duration = _configuration.GetValue<int>("JWT:RefreshTokenValidityInDays");

            user.AddRefreshToken(refreshToken, duration);

            await _userRepo.AddAsync(user);

            var response = new AuthResultDto
            {
                AccessToken = acessToken,
                RefeshToken = refreshToken,
                ExpiresAccessTokenIn = DateTime.UtcNow.AddMinutes(15),
                User = new UserResponseDto
                {
                    Name = user.Name.Value!,
                    Email = user.Email.Value!,
                    Role = RoleType.Visitor.ToString(),
                }
            };

            return Result<AuthResultDto>.Success(response);
        }
    }
}