using VisitorService.Domain.Enums;
using VisitorService.Domain.Shared.results;

namespace VisitorService.Domain.ValueObjects
{
    public sealed class RoleName
    {
        public RoleType Value { get; }

        private RoleName(RoleType value)
        {
            Value = value;
        }

        public static Result<RoleName> Create(RoleType value)
        {
            if (!Enum.IsDefined(typeof(RoleType), value))
                return Result<RoleName>.Fail("Invalid role type.");

            return Result<RoleName>.Success(new RoleName(value));
        }

    }
}
