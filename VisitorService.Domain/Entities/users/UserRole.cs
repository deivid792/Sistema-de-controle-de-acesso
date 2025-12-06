using System;
using System.Collections.Generic;

namespace VisitorService.Domain.Entities
{
    public sealed class UserRole
    {
        public Guid UserId { get; private set; }
        public int RoleId { get; private set; }

        public User User { get; private set; } = default!;
        public Role Role { get; private set; } = default!;

        private UserRole() { } 

        public UserRole(User user, Role role)
        {
            User = user;
            UserId = user.Id;
            Role = role;
            RoleId = role.Id;
        }
    }
}
