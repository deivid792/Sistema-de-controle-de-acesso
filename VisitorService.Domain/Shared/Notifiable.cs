namespace VisitorService.Domain.Shared
{
    public abstract class Notifiable
    {
        private readonly List<Notification> _notifications = new();

        public bool HasErrors => _notifications.Any();

        public IReadOnlyCollection<Notification> Errors => _notifications.AsReadOnly();

        public void AddNotification(string key, string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
                _notifications.Add(new Notification(key, message));
        }

        public void AddRangeNotification(IEnumerable<Notification> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public void ClearNotifications()
        {
            _notifications.Clear();
        }

        public override string ToString() => string.Join("; ", _notifications.Select(e => $"{e.Key}: {e.Message}"));


    }
}