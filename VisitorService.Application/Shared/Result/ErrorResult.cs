using VisitorService.Domain.Shared;

namespace VisitorService.Application.Shared
{
    public class Error
    {
        public string Message { get; }

        public Error(string message)
        {
            Message = message;
        }

        public static implicit operator Error(string message)
            => new Error(message);

        public static implicit operator Error(NotificationItem item)
            => new Error(item.Message);
    }
}
