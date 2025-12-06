using VisitorService.Domain.Shared;
using VisitorService.Domain.ValueObject;

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

        private readonly Notification _notification = new();
        public IReadOnlyCollection<NotificationItem> Notification => _notification.Errors;
        public bool HasErrors => _notification.HasErrors;

        private readonly List<UserRole> _userRoles = new();
        private readonly List<ValidationToken> _validationTokens = new();
        private readonly List<Visit> _visits = new();
        private readonly List<RefreshToken> _refreshTokens = new();

        public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();
        public IReadOnlyCollection<ValidationToken> ValidationTokens => _validationTokens.AsReadOnly();
        public IReadOnlyCollection<Visit> Visits => _visits.AsReadOnly();
        public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();


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

            _notification.addRange(name.Notification);
            _notification.addRange(email.Notification);
            _notification.addRange(password.Notification);

            if (phone != null)
                _notification.addRange(phone.Notification);

            if (cnpj != null)
                _notification.addRange(cnpj.Notification);
        }

        public static User Create(Name name, Email email, Password password, Phone? phone = null, string? company = null, Cnpj? cnpj = null)
        {
            var user = new User(name, email, password, phone, company, cnpj);

            return user;
        }

        public void UpdateData(Name name, Phone? phone = null, string? company = null, Cnpj? cnpj = null)
        {
            _notification.Clear();

            _notification.addRange(name.Notification);

            if (phone != null)
                _notification.addRange(phone.Notification);

            if (cnpj != null)
                _notification.addRange(cnpj.Notification);

            if (_notification.HasErrors)
                return;

            Name = name;
            Phone = phone;
            Company = company;
            Cnpj = cnpj;
        }
        public void AddRole(Role role)
        {
            if (role == null)
            {
                _notification.add("User.AddRole", "Role não pode ser nula.");
                return;
            }

            foreach (var userRole in UserRoles)
            {
                if (userRole.RoleId == role!.Id)
                    _notification.add("User.AddRole", "O usuário ja possui essa role.");
            }

            var newUserRole = new UserRole(this, role!);
            _userRoles.Add(newUserRole);
        }

        public void RemoveRole(UserRole role)
        {
            if (_userRoles.Contains(role))
                _userRoles.Remove(role);
        }

        public void AddRefreshToken(RefreshToken token)
        {
            _refreshTokens.Add(token);
        }

        public void SetCreatedBy(Guid managerId, string? managerName = null)
        {
            CreatedByUserId = managerId;
            CreatedByUserName = managerName;
        }

        public void AddVisit(DateOnly date, TimeOnly time, string reason, string category, string status)
        {
            if (date < DateOnly.FromDateTime(DateTime.Today))
            {
                _notification.add("Visit.Date", "A visita não pode ser no passado.");
                return;
            }

            var visit = new Visit(this, date, time, category, reason, status);

            if (visit.HasErrors)
            {
                foreach (var item in visit.Notification)
                    _notification.add(item.Key, item.Message);

                return;
            }

            _visits.Add(visit);
        }
    }
}

