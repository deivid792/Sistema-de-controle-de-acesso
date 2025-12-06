using VisitorService.Domain.Shared;

namespace VisitorService.Domain.Entities
{
    public sealed class Visit
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public DateOnly Date { get; private set; }
        public TimeOnly Time { get; private set; }
        public string Reason { get; private set; } = default!;
        public string Category { get; private set; } = default!;
        public string Status { get; private set; } = default!;
        public DateTime? CheckIn { get; private set; }
        public DateTime? CheckOut { get; private set; }

        public User User { get; private set; } = default!;
        public ICollection<VisitHistory> VisitHistories { get; private set; } = new List<VisitHistory>();
        private readonly Notification _notification = new();
        public IReadOnlyCollection<NotificationItem> Notification => _notification.Errors;
        public bool HasErrors => _notification.HasErrors;

        private Visit() { }

        public Visit(User user, DateOnly date, TimeOnly time, string reason, string category, string status)
        {
            Id = Guid.NewGuid();
            User = user;
            UserId = user.Id;
            Date = date;
            Time = time;
            Reason = reason;
            Category = category;
            Status = status;
        }

        public void UpdateStatus(string newStatus)
        {
            if (newStatus != "Aprovada" && newStatus != "Rejeitada")
            {
                _notification.add("Visit.Status", "Status inválido.");
                return;
            }

            Status = newStatus;
        }

        public void CheckInVisit()
        {
            if (Status != "Aprovada")
            {
                _notification.add("Visit.CheckIn", "Somente visitas aprovadas podem fazer check-in.");
                return;
            }

            if (CheckIn is not null)
            {
                _notification.add("Visit.CheckIn", "Check-in já realizado.");
                return;
            }

            CheckIn = DateTime.Now;

            Status = "Em Progresso";
        }

        public void CheckOutVisit()
        {
            if (CheckIn is null)
            {
                _notification.add("Visit.CheckOut", "A visita ainda não fez check-in.");
                return;
            }

            if (CheckOut is not null)
            {
                _notification.add("Visit.CheckOut", "Check-out já realizado.");
                return;
            }

            CheckOut = DateTime.Now;
            Status = "Completo";
        }
    }
}
