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
            .IsNotNullOrWhiteSpace(description, "Role")
            .MinLength(description, 5, "Role")
            .MaxLength(description, 100, "Role");

            var role = new Role(name, description);

            if(contract.HasErrors)
                role.AddRangeNotification(contract.Errors);

            if (name.HasErrors)
                role.AddRangeNotification(name.Errors);

            return role;
        }
    }
}
