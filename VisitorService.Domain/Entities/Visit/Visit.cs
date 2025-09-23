using System;
using System.Collections.Generic;

namespace VisitorService.Domain.Entities
{
    public sealed class Visit
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime Date { get; private set; }
        public TimeSpan Time { get; private set; }
        public string Reason { get; private set; } = default!;
        public string Category { get; private set; } = default!;
        public string Status { get; private set; } = default!;
        public DateTime? CheckIn { get; private set; }
        public DateTime? CheckOut { get; private set; }

        public User User { get; private set; } = default!;
        public ICollection<VisitHistory> VisitHistories { get; private set; } = new List<VisitHistory>();

        private Visit() { }

        public Visit(User user, DateTime date, TimeSpan time, string reason, string category, string status)
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

        public void CheckInVisit(DateTime checkIn)
        {
            CheckIn = checkIn;
            Status = "In Progress";
        }

        public void CheckOutVisit(DateTime checkOut)
        {
            CheckOut = checkOut;
            Status = "Completed";
        }
    }
}
