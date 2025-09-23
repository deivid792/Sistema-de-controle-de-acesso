using System;
using System.Collections.Generic;

namespace VisitorService.Domain.Entities
{
    public sealed class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = default!;
        public string Email { get; private set; } = default!;
        public string Password { get; private set; } = default!;
        public string? Phone { get; private set; }
        public string? Company { get; private set; }
        public string? Cnpj { get; private set; }

        public ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();
        public ICollection<ValidationToken> ValidationTokens { get; private set; } = new List<ValidationToken>();
        public ICollection<Visit> Visits { get; private set; } = new List<Visit>();

        private User() { }

        public User(string name, string email, string password)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Password = password;
        }

        public void UpdateData(string name, string? phone = null, string? company = null, string? cnpj = null)
        {
            Name = name;
            Phone = phone;
            Company = company;
            Cnpj = cnpj;
        }
    }
}
