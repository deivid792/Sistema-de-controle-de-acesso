using VisitorService.Domain.Shared.results;
using VisitorService.Domain.ValueObject;
using VisitorService.Domain.ValueObjects;

namespace VisitorService.Domain.Entities
{
    public sealed class User
    {
        public Guid Id { get; private set; }
        public Name Name { get; private set; } = default!;
        public Email Email { get; private set; } = default!;
        public Password Password { get; private set; } = default!;
        public Phone? Phone { get; private set; }
        public string? Company { get; private set; }
        public Cnpj? Cnpj { get; private set; }
        public Guid? CreatedByUserId { get; private set; }
        public string? CreatedByUserName { get; private set; }

        public ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();
        public ICollection<ValidationToken> ValidationTokens { get; private set; } = new List<ValidationToken>();
        public ICollection<Visit> Visits { get; private set; } = new List<Visit>();
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();


        private User() { }

        private User(Name name, Email email, Password password, Phone? phone, string? company, Cnpj? cnpj)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Password = password;
            Phone = phone;
            Company = company;
            Cnpj = cnpj;

        }

        public static Result<User> Create(Name name, Email email, Password password, Phone? phone = null, string? company = null, Cnpj? cnpj = null)
        {
            var user = new User(name, email, password, phone, company, cnpj);
            return Result<User>.Success(user);
        }

        public void UpdateData(Name name, Phone? phone = null, string? company = null, Cnpj? cnpj = null)
        {
            Name = name;
            Phone = phone;
            Company = company;
            Cnpj = cnpj;
        }
        public Result AddRole(Role role)
        {
            if (role == null)
                return Result.Fail("Role cannot be null.");

            foreach (var userRole in UserRoles)
            {
                if (userRole.RoleId == role.Id)
                    return Result.Fail("User already has this role.");
            }

            var newUserRole = new UserRole(this, role);
            UserRoles.Add(newUserRole);

            return Result.Success();
        }

        public void RemoveRole(UserRole role)
        {
            if (UserRoles.Contains(role))
                UserRoles.Remove(role);
        }

        public void AddRefreshToken(RefreshToken token)
        {
            RefreshTokens.Add(token);
        }

        public void SetCreatedBy(Guid managerId, string? managerName = null)
        {
            CreatedByUserId = managerId;
            CreatedByUserName = managerName;
        }
    }
}
