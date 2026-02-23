using VisitorService.Domain.Shared;
using VisitorService.Domain.ValueObject;

namespace VisitorService.Domain.Entities
{
    public sealed class User : BaseEntity
    {
        public Name Name { get; private set; } = default!;
        public Email Email { get; private set; } = default!;
        public Password Password { get; private set; } = default!;
        public Phone? Phone { get; private set; }
        public string? Company { get; private set; }
        public Cnpj? Cnpj { get; private set; }
        public Guid? CreatedByUserId { get; private set; }
        public string? CreatedByUserName { get; private set; }

        private readonly List<Role> _Roles = new();
        private readonly List<ValidationToken> _validationTokens = new();
        private readonly List<Visit> _visits = new();
        private readonly List<RefreshToken> _refreshTokens = new();

        public IReadOnlyCollection<Role> Roles => _Roles.AsReadOnly();
        public IReadOnlyCollection<ValidationToken> ValidationTokens => _validationTokens.AsReadOnly();
        public IReadOnlyCollection<Visit> Visits => _visits.AsReadOnly();
        public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();


        private User()  : base() { }

        private User(Name name, Email email, Password password, Phone? phone, string? company, Cnpj? cnpj) : base()
        {
            Name = name;
            Email = email;
            Password = password;
            Phone = phone;
            Company = company;
            Cnpj = cnpj;

            name.AddRangeNotification(name.Errors);
            email.AddRangeNotification(email.Errors);
            password.AddRangeNotification(password.Errors);

            if (phone != null)
                AddRangeNotification(phone.Errors);

            if (cnpj != null)
                AddRangeNotification(cnpj.Errors);
        }

        public static User Create(Name name, Email email, Password password, Phone? phone = null, string? company = null, Cnpj? cnpj = null)
        {
            var user = new User(name, email, password, phone, company, cnpj);

            var usuario = user.Name.Value;

            return user;

        }

        public void UpdateData(Name name, Phone? phone = null, string? company = null, Cnpj? cnpj = null)
        {
            NotificationClear();

            AddRangeNotification(name.Errors);

            if (phone != null)
                AddRangeNotification(phone.Errors);

            if (cnpj != null)
                AddRangeNotification(cnpj.Errors);

            if (HasErrors)
                return;

            Name = name;
            Phone = phone;
            Company = company;
            Cnpj = cnpj;
        }
        public void AddRole(Role role)
        {
            foreach (var userRole in Roles)
            {
                if (userRole.Id == role!.Id)
                    AddNotification("User.AddRole", "O usuário ja possui essa role.");
            };

            _Roles.Add(role);
        }

        public void RemoveRole(Role role)
        {
            if (_Roles.Contains(role))
                _Roles.Remove(role);
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
                AddNotification("Visit.Date", "A visita não pode ser no passado.");
                return;
            }

            var visit = new Visit(this, date, time, category, reason, status);

            if (visit.HasErrors)
            {
                foreach (var item in visit.Errors)
                    AddNotification(item.Key, item.Message);

                return;
            }

            _visits.Add(visit);
        }
    }
}

