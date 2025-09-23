using System;
using System.Collections.Generic;

namespace VisitorService.Domain.Entities
{
    public sealed class Role
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = default!;
        public ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();

        private Role() { }

        public Role(string name)
        {
            Name = name;
        }
        
    }
}


