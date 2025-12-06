using VisitorService.Domain.ValueObject;
using VisitorService.Domain.Enums;
using VisitorService.Domain.Entities;
using VisitorService.Application.DTOS;
using VisitorService.Application.Interfaces;
using VisitorService.Domain.Interfaces;
using VisitorService.Application.Shared.results;
using VisitorService.Domain.Services;

namespace VisitorService.Application.UseCases
{
    public class RegisterVisitorHandler : IRegisterVisitorHandler
    {
        private readonly IUserRepository _userRepo;
        private readonly IRoleRepository _roleRepo;
        private readonly IPasswordService _passwordService;

        public RegisterVisitorHandler(IUserRepository userRepo, IRoleRepository roleRepo, IPasswordService passwordService)
        {
            _userRepo = userRepo;
            _roleRepo = roleRepo;
            _passwordService = passwordService;
        }

        public async Task<Result> Handle(RegisterVisitorCommand command)
        {
            var nameResult = Name.Create(command.Name);
            if (nameResult.HasErrors)
                return Result.Fail(nameResult.Notification);

            var emailResult = Email.Create(command.Email);
            if (emailResult.HasErrors)
                return Result.Fail(emailResult.Notification);

            var existing = await _userRepo.GetByEmailAsync(emailResult.Value!);
            if (existing is not null)
                return Result.Fail("E-mail já cadastrado.");

            var password = Password.Create(command.Password);
            if (password.HasErrors)
                return Result.Fail(password.Notification!);
            var hashResult = _passwordService.Hash(password.Value);

            var passwordVo = hashResult;

            Phone? phoneVo = null;
            if (!string.IsNullOrWhiteSpace(command.Phone))
            {
                var phoneRes = Phone.Create(command.Phone);
                if (phoneRes.HasErrors)
                    return Result.Fail(phoneRes.Notification);
                phoneVo = phoneRes;
            }

            Cnpj? cnpjVo = null;
            if (!string.IsNullOrWhiteSpace(command.Cnpj))
            {
                var cnpjRes = Cnpj.Create(command.Cnpj);
                if (cnpjRes.HasErrors)
                    return Result.Fail(cnpjRes.Notification);
                cnpjVo = cnpjRes;
            }

            var userResult = User.Create(
                nameResult,
                emailResult,
                passwordVo,
                command.Phone is null ? null : phoneVo,
                command.Company,
                cnpjVo
            );

            if (userResult.HasErrors)
                return Result.Fail(userResult.Notification!);

            var user = userResult;

            var visitorRole = await _roleRepo.GetByRoleTypeAsync(RoleType.Visitor);
            if (visitorRole is null)
                return Result.Fail("Role padrão 'Visitor' não está criada no sistema.");

            user.AddRole(visitorRole);

            await _userRepo.AddAsync(user);

            return Result.Success();
        }
    }
}