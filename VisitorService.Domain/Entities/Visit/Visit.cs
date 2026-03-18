using VisitorService.Domain.Shared;

namespace VisitorService.Domain.Entities
{
    public sealed class Visit : BaseEntity
    {
        public Guid UserId { get; private set; }
        public DateOnly Date { get; private set; }
        public TimeOnly Time { get; private set; }
        public string? Reason { get; private set; } = default!;
        public string? Category { get; private set; } = default!;
        public string? Status { get; private set; } = default!;
        public DateTime? CheckIn { get; private set; }
        public DateTime? CheckOut { get; private set; }

        public User User { get; private set; } = default!;

        private Visit() : base() { }

        private Visit(User user, DateOnly date, TimeOnly time, string? reason, string category, string status) : base()
        {
            User = user;
            UserId = user.Id;
            Date = date;
            Time = time;
            Reason = reason;
            Category = category;
            Status = status;
        }

        public static Visit Create(User user, DateOnly date, TimeOnly time, string reason, string category )
        {
            var reasonNormalized = reason?.Trim() ?? string.Empty;
            var categoryNormalized = category?.Trim() ?? string.Empty;

            var contract = new Contract()
            .Requires()
            .IsNotNullOrWhiteSpaceList("Visit", [reasonNormalized,categoryNormalized])
            .MaxLengthList( [reasonNormalized, categoryNormalized], 50, "Visit")
            .IsNotNullOrWhiteSpaceList("Visit", [date])
            .GreaterOrEqualToToday("Visit", date)
            .IsNotNullOrWhiteSpaceList("Visit", [time]);


            var visit = new Visit(user, date, time, reasonNormalized, categoryNormalized, "Pending" );

            if (contract.HasErrors)
            {
                visit.User.AddRangeNotification(contract.Errors);
            }
            return visit;
        }

        public void UpdateStatus(string newStatus)
        {
            if (newStatus != "Approved" && newStatus != "Rejected")
            {
                AddNotification("Visit.Status", "Status inválido.");
                return;
            }

            Status = newStatus;
        }

        public void CheckInVisit()
        {
            if (Status != "Approved")
            {
                AddNotification("Visit.CheckIn", "Somente visitas aprovadas podem fazer check-in.");
                return;
            }

            if (CheckIn is not null)
            {
                AddNotification("Visit.CheckIn", "Check-in já realizado.");
                return;
            }

            CheckIn = DateTime.Now;

            Status = "InProgress";
        }

        public void CheckOutVisit()
        {
            if (CheckIn is null)
            {
                AddNotification("Visit.CheckOut", "A visita ainda não fez check-in.");
                return;
            }

            if (CheckOut is not null)
            {
                AddNotification("Visit.CheckOut", "Check-out já realizado.");
                return;
            }

            CheckOut = DateTime.Now;
            Status = "Completed";
        }
    }
}
