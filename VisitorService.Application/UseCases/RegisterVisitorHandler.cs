using System.Threading.Tasks;
using VisitorService.Domain.ValueObject;
using VisitorService.Domain.Shared.results;
using VisitorService.Domain.Enums;
using VisitorService.Domain.Entities;
using VisitorService.Application.DTOS;
using VisitorService.Application.Interfaces;

namespace VisitorService.Application.UseCases
{
    public class RegisterVisitorHandler
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
        if (!nameResult.IsSuccess)
            return Result.Fail(nameResult.Error!);

        var emailResult = Email.create(command.Email);
        if (!emailResult.IsSuccess)
            return Result.Fail(emailResult.Error!);

        var existing = await _userRepo.GetByEmailAsync(emailResult.Value!.Value);
        if (existing is not null)
            return Result.Fail("E-mail já cadastrado.");

        var hashResult = _passwordService.Hash(command.Password);
        if (!hashResult.IsSuccess)
            return Result.Fail(hashResult.Error!);

        var passwordVo = hashResult.Value!;

        Phone? phoneVo = null;
        if (!string.IsNullOrWhiteSpace(command.Phone))
        {
            var phoneRes = Phone.Create(command.Phone);
            if (!phoneRes.IsSuccess)
                return Result.Fail(phoneRes.Error!);
            phoneVo = phoneRes.Value;
        }

        Cnpj? cnpjVo = null;
        if (!string.IsNullOrWhiteSpace(command.Cnpj))
        {
            var cnpjRes = Cnpj.Create(command.Cnpj);
            if (!cnpjRes.IsSuccess)
                return Result.Fail(cnpjRes.Error!);
            cnpjVo = cnpjRes.Value;
        }

        var userResult = User.Create(
            nameResult.Value!,
            emailResult.Value!,
            passwordVo,
            command.Phone is null ? null : phoneVo,
            command.Company,
            cnpjVo
        );

        if (!userResult.IsSuccess)
            return Result.Fail(userResult.Error!);

        var user = userResult.Value!;

        var visitorRole = await _roleRepo.GetByRoleTypeAsync(RoleType.Visitor);
        if (visitorRole is null)
            return Result.Fail("Role padrão 'Visitor' não está criada no sistema.");

        user.AddRole(visitorRole);

        await _userRepo.AddAsync(user);

        return Result.Success();
    }
}
}