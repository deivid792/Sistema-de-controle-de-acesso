using System;
using System.Collections.Generic;

namespace VisitorService.Domain.Entities
{
    public sealed class VisitHistory
    {
        public Guid Id { get; private set; }
        public Guid VisitId { get; private set; }
        public Guid? ManagerId { get; private set; }
        public string Action { get; private set; } = default!;
        public DateTime DateTime { get; private set; }

        public Visit Visit { get; private set; } = default!;

        private VisitHistory() { }

        public VisitHistory(Visit visit, string action, Guid? managerId = null)
        {
            Id = Guid.NewGuid();
            Visit = visit;
            VisitId = visit.Id;
            Action = action;
            DateTime = DateTime.Now;
            ManagerId = managerId;
        }
    }
}
