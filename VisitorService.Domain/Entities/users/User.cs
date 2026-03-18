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
        private readonly List<Visit> _visits = new();
        private readonly List<RefreshToken> _refreshTokens = new();

        public IReadOnlyCollection<Role> Roles => _Roles.AsReadOnly();
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
        }

        public static User Create(Name name, Email email, Password password, Phone? phone = null, string? company = null, Cnpj? cnpj = null)
        {
            var user = new User(name, email, password, phone, company, cnpj);

            user.AddRangeNotification(name.Errors);
            user.AddRangeNotification(email.Errors);
            user.AddRangeNotification(password.Errors);

            if (phone != null)
                user.AddRangeNotification(phone.Errors);

            if (cnpj != null)
                user.AddRangeNotification(cnpj.Errors);

            return user;

        }

        public void UpdateProfile(Name name, Phone? phone = null, string? company = null, Cnpj? cnpj = null)
        {
            ClearNotifications();

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

        public void AddRefreshToken(string token, int daysToExpiry)
        {
            var expirations = DateTime.UtcNow.AddDays(daysToExpiry);
            _refreshTokens.Add(new RefreshToken(token, expirations));
        }

        public void AssignCreator(Guid managerId, string? managerName = null)
        {
            if(CreatedByUserId != Guid.Empty && CreatedByUserId != null)
            {
                AddNotification("User.SetCreatedBy", "O criador desse usuário não pode ser alterado");
            }

            CreatedByUserId = managerId;
            CreatedByUserName = managerName;
        }

        public void ScheduleVisit(DateOnly date, TimeOnly time, string reason, string category)
        {
            var visit = Visit.Create(this, date, time, category, reason);

            if (visit.HasErrors)
                this.AddRangeNotification(visit.Errors);

            _visits.Add(visit);
        }
    }
}

