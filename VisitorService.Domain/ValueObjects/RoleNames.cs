using VisitorService.Domain.Enums;
using VisitorService.Domain.Shared;

namespace VisitorService.Domain.ValueObjects
{
    public sealed class RoleName : Notifiable
    {
        public RoleType Value { get; }

        private RoleName(RoleType value)
        {
            Value = value;
        }

        public static RoleName Create(RoleType value)
        {
            var roleName = new RoleName(value);

            if (!Enum.IsDefined(typeof(RoleType), value))
                roleName.AddNotification("RoleName", "Tipo de Role inválida.");

            return roleName;
        }

    }
}
