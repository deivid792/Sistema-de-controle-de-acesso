using VisitorService.Domain.Enums;
using VisitorService.Domain.Shared;

namespace VisitorService.Domain.ValueObjects
{
    public sealed class RoleName
    {
        public RoleType Value { get; }

        private readonly Notification _notification = new();
        public IReadOnlyCollection<NotificationItem> Notification => _notification.Errors;
        public bool HasErrors => _notification.HasErrors;

        private RoleName(RoleType value)
        {
            Value = value;
        }

        public static RoleName Create(RoleType value)
        {
            RoleType role = value;
            var tempRoleName = new RoleName(role);
            var notification = tempRoleName._notification;

            if (!Enum.IsDefined(typeof(RoleType), value))
                notification.add("Role", "Invalid role type.");

            return new RoleName(tempRoleName.Value);
        }

        public static RoleName FromDatabase(RoleType value)
    => new RoleName(value);


    }
}
