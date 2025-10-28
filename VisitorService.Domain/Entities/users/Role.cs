using System;
using System.Collections.Generic;
using VisitorService.Domain.Enums;
using VisitorService.Domain.Shared.results;
using VisitorService.Domain.ValueObjects;

namespace VisitorService.Domain.Entities
{
    public sealed class Role
    {
        public int Id { get; private set; }
        public RoleName Name { get; private set; } = default!;
        public string Description { get; private set; } = default!;

        public ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();

        private Role() { }

        private Role(RoleName name)
        {
            Name = name;
        }
        public static Result<Role> Create(RoleName name)
        {
            if (name is null)
                return Result<Role>.Fail("RoleName cannot be null.");

            return Result<Role>.Success(new Role(name));
        }

        public static Result<Role> Visitor()
        {
            var nameResult = RoleName.Create(RoleType.Visitor);
            if (!nameResult.IsSuccess)
                return Result<Role>.Fail(nameResult.Error!);

            return Create(nameResult.Value!);
        }

        public static Result<Role> Manager()
        {
            var nameResult = RoleName.Create(RoleType.Manager);
            if (!nameResult.IsSuccess)
                return Result<Role>.Fail(nameResult.Error!);

            return Create(nameResult.Value!);
        }

        public static Result<Role> Security()
        {
            var nameResult = RoleName.Create(RoleType.Security);
            if (!nameResult.IsSuccess)
                return Result<Role>.Fail(nameResult.Error!);

            return Create(nameResult.Value!);
        }
    }
}
