using VisitorService.Domain.Enums;
using VisitorService.Domain.Shared;
using VisitorService.Domain.ValueObjects;

namespace VisitorService.Domain.Entities
{
    public sealed class Role
    {
        public int Id { get; private set; }
        public RoleName Name { get; private set; } = default!;
        public string Description { get; private set; } = default!;

        private readonly Notification _notification = new();
        public IReadOnlyCollection<NotificationItem> Notification => _notification.Errors;
        public bool HasErrors => _notification.HasErrors;

        public ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();

        private Role() { }

        private Role(RoleName name)
        {
            Name = name;

            _notification.addRange(name.Notification);
        }
        public static Role Create(RoleName name)
        {
            var role = new Role(name);
            var notification = role._notification;
            if (name is null)
                notification.add("Role", "RoleName não pode ser nulo.");

            return role;
        }

        public static Role Visitor()
        {
            var name = RoleName.Create(RoleType.Visitor);
            var role = Role.Create(name);

            role._notification.addRange(name.Notification);

            return role;
        }

        public static Role Manager()
        {
            var name = RoleName.Create(RoleType.Visitor);
            var role = Role.Create(name);

            role._notification.addRange(name.Notification);

            return role;
        }

        public static Role Security()
        {
            var name = RoleName.Create(RoleType.Visitor);
            var role = Role.Create(name);

            role._notification.addRange(name.Notification);

            return role;
        }
    }
}
