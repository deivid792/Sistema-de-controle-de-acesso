using System.Text.RegularExpressions;
using VisitorService.Domain.Shared;

namespace VisitorService.Domain.ValueObject
{

    public class Password
    {
        public string Value { get; }
        private readonly Notification _notification = new();
        public IReadOnlyCollection<NotificationItem> Notification => _notification.Errors;
        public bool HasErrors => _notification.HasErrors;
        private Password(string value)
        {
            Value = value;
        }

        public static Password Create(string value)
        {
            var tempPassword = new Password(value);
            var notification = tempPassword._notification;

            if (string.IsNullOrWhiteSpace(value))
            {
                notification.add("Password", "A senha não pode estar vazia ou apenas espaços");
                return tempPassword;
            }

            var normalized = tempPassword.Value.Trim();

            if (normalized.Length < 8)
                notification.add("Password", "A senha deve conter pelo menos 8 caracteres");

            if (normalized.Length > 12)
                notification.add("Password", "A senha não deve exceder 12 caracteres");

            if (!Regex.IsMatch(normalized, "[A-Z]"))
                notification.add("Password", "A senha deve conter pelo menos uma letra maiúscula");

            if (!Regex.IsMatch(normalized, "[a-z]"))
                notification.add("Password", "A senha deve conter pelo menos uma letra minúscula");

            if (!Regex.IsMatch(normalized, "[0-9]"))
                notification.add("Password", "A senha deve conter pelo menos um número");

            if (!Regex.IsMatch(normalized, "[!@#$%^&*(),.?\":{}|<>]"))
                notification.add("Password", "A senha deve conter pelo menos um caractere especial");

            if (normalized.Contains(" "))
                notification.add("Password", "A senha não deve conter espaços em branco no meio");

            if (notification.HasErrors)
                return tempPassword;

            return new Password(normalized);
        }

        public static Password FromHash(string value)
        {
            return new Password(value);
        }
    }
}