namespace VisitorService.Domain.Shared
{
    public sealed class Notification
    {
        private readonly List<NotificationItem> _erros = new();

        public bool HasErrors => _erros.Count > 0;

        public IReadOnlyCollection<NotificationItem> Errors => _erros.AsReadOnly();

        public void add(string key, string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
                _erros.Add(new NotificationItem(key, message));
        }

        public void addRange(IEnumerable<NotificationItem> itens)
        {
            _erros.AddRange(itens);
        }

        public void Clear()
        {
            _erros.Clear();
        }

        public override string ToString() => string.Join("; ", _erros.Select(e => $"{e.Key}: {e.Message}"));


    }
}