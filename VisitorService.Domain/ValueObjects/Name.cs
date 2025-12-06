using VisitorService.Domain.Shared;

namespace VisitorService.Domain.ValueObject
{
    public sealed class Name
    {
        public string Value { get; }
        private readonly Notification _notification = new();
        public IReadOnlyCollection<NotificationItem> Notification => _notification.Errors;
        public bool HasErrors => _notification.HasErrors;
        private Name(string value)
        {
            Value = value;
        }

        public static Name Create(string value)
        {
            var tempName = new Name(value);
            var notification = tempName._notification;

            if (string.IsNullOrWhiteSpace(value))
            {
                notification.add("Name", "O nome não pode ser vazio ou apenas espaços");
                return tempName;
            }
            var normalized = tempName.Value.Trim();

            if (normalized.Length < 2)
                notification.add("Name", "O nome deve ter pelo menos 2 caracteres.");

            if (normalized.Length > 100)
                notification.add("Name", "O nome deve ter no máximo 100 caracteres.");

            if (notification.HasErrors)
                return tempName;

            return new Name(normalized);
        }

        public static Name FromDatabase(string value)
            => new Name(value);

    }
}
