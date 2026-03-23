using VisitorService.Application.Interfaces;
using VisitorService.Application.Shared.results;
using VisitorService.Domain.Entities;
using VisitorService.Domain.Enums;
using VisitorService.Domain.Interfaces;
using VisitorService.Domain.Services;
using VisitorService.Domain.ValueObject;


namespace VisitorService.Application.UseCases.Users.Commands.CreateManager;

public class CreateManagerHandler : ICreateManagerHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly IRoleRepository _roleRepository;

    public CreateManagerHandler(IUserRepository userRepository, IPasswordService passwordService, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _roleRepository = roleRepository;
    }

    public async Task<Result> Handle(CreateManagerCommand user, Guid idManager)
    {
        var IsManager = await _userRepository.IsUserInRoleAsync(idManager, Domain.Enums.RoleType.Manager);

        if (!IsManager)
        {
            return Result.Fail("Apenas Gestores podem criar novos Gestores");
        }

        // Name
        var name = Name.Create(user.name);

        if (name.HasErrors)
            return Result.Fail(name.Errors);

        // Email
        var email = Email.Create(user.Email);

        if (email.HasErrors)
            return Result.Fail(email.Errors);

        var existingUser = await _userRepository.GetByEmailAsync(user.Email);

        if (existingUser != null)
            return Result.Fail("Email já cadastrado!");

        //Password
        var password = Password.Create(user.Password);

        if (password.HasErrors)
            return Result.Fail(password.Errors);

        var HashPassword = _passwordService.Hash(user.Password);

        //Phone
        Phone? phoneVo = null;
        if(! string.IsNullOrWhiteSpace(user.phone))
        {
        var phone = Phone.Create(user.phone);

        if (phone.HasErrors)
            return Result.Fail(phone.Errors);

        phoneVo = phone;
        }

        //CreatedByUserId
        Guid createdByUserId = idManager;

        var createdByUserName = await _userRepository.GetByNameAsync(idManager);

        if (createdByUserName == null)
            return Result.Fail("É necessario o nome do gestor responsável");

        var manager = User.Create(name, email, HashPassword, phoneVo, null, null, createdByUserId, createdByUserName.Value);

        if (manager.HasErrors)
            return Result.Fail(manager.Errors);

        var ManagerRole = await _roleRepository.GetByRoleTypeAsync(RoleType.Manager);
            if (ManagerRole is null)
                return Result.Fail("Role padrão 'Manager' não está criada no sistema.");

        manager.AddRole(ManagerRole);

        await _userRepository.AddAsync(manager);

        return Result.Success();
    }
}