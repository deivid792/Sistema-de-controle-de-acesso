using System.Collections.ObjectModel;
using VisitorService.Domain.Enums;
using VisitorService.Domain.Shared;
using VisitorService.Domain.ValueObjects;

namespace VisitorService.Domain.Entities
{
    public sealed class Role : BaseEntity
    {
        public RoleName Name { get; private set; } = default!;
        public string Description { get; private set; } = default!;

        private readonly List<User> _Users = new();

        public IReadOnlyCollection<User> Users => _Users.AsReadOnly();

        private Role() : base() { }

        private Role(RoleName name, string description) : base()
        {
            Name = name;
            Description = description;

        }
        public static Role Create(RoleName name, string description)
        {
            var contract = new Contract()
            .Requires()
            .IsNotNullOrWhiteSpace(description, "Role.Create")
            .MinLength(description, 5, "Role.Create")
            .MaxLength(description, 100, "Role.Create");

            var role = new Role(name, description);

            if(contract.HasErrors)
                role.AddRangeNotification(contract.Errors);

            if (name.HasErrors)
                role.AddRangeNotification(name.Errors);

            return role;
        }

        public static Role Visitor()
        {
            return Role.Create(
                RoleName.Create(RoleType.Visitor),
                 "Perfil com permissões limitadas, focado na visualização de dados e gerenciamento do próprio cadastro.");
        }

        public static Role Manager()
        {
            return Role.Create(
                RoleName.Create(RoleType.Manager),
                 "Responsável pelo controle de fluxo, execução de check-in, check-out e monitoramento de acessos em tempo real.");
        }

        public static Role Security()
        {
            return Role.Create(
                RoleName.Create(RoleType.Security),
                 "Acesso total ao sistema, com poderes para gerenciar usuários, configurar roles, extrair relatórios e auditar logs");
        }
    }
}
