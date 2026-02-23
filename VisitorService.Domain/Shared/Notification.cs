namespace VisitorService.Domain.Shared
{
    public abstract class Notifiable
    {
        private readonly List<NotificationItem> _erros = new();

        public bool HasErrors => _erros.Count > 0;

        public IReadOnlyCollection<NotificationItem> Errors => _erros.AsReadOnly();

        public void AddNotification(string key, string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
                _erros.Add(new NotificationItem(key, message));
        }

        public void AddRangeNotification(IEnumerable<NotificationItem> itens)
        {
            _erros.AddRange(itens);
        }

        public void NotificationClear()
        {
            _erros.Clear();
        }

        public override string ToString() => string.Join("; ", _erros.Select(e => $"{e.Key}: {e.Message}"));


    }
}