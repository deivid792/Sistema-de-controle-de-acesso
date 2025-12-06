    namespace VisitorService.Domain.Shared{
    public sealed class NotificationItem
    {
        public string Key { get; }
        public string Message { get; }

        public NotificationItem(string key, string message)
        {
            Key = key;
            Message = message;
        }
    }
}