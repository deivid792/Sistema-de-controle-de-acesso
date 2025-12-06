using System.Text.RegularExpressions;
using VisitorService.Domain.Shared;

namespace VisitorService.Domain.ValueObject
{
    public sealed class Phone
    {
        public string Value { get; }
        private readonly Notification _notification = new();
        public IReadOnlyCollection<NotificationItem> Notification => _notification.Errors;
        public bool HasErrors => _notification.HasErrors;

        private Phone(string value)
        {
            Value = value;
        }

        public static Phone Create(string? value)
        {
            var tempPhone = new Phone(value!);
            var notification = tempPhone._notification;

            if (string.IsNullOrWhiteSpace(value))
            {
                notification.add("Phone", "O telefone não pode ser nulo");
                return tempPhone;
            }

            var normalized = tempPhone.Value!.Trim();

            var regex = new Regex(@"^(\+55\s?)?(\(?\d{2}\)?\s?)?\d{4,5}-?\d{4}$");

            if (!regex.IsMatch(normalized))
                notification.add("phone", "O formato do telefone é inválido. Use (XX) XXXXX-XXXX ou +55XXXXXXXXXXX.");

            var digitsOnly = Regex.Replace(normalized, @"\D", "");

            if (digitsOnly.Length < 10 || digitsOnly.Length > 13)
                notification.add("phone", "O número de telefone deve ter entre 10 e 13 dígitos.");

            if (notification.HasErrors)
                return tempPhone;

            return new Phone(digitsOnly);
        }
        public static Phone FromDatabase(string value)
        {
            return new Phone(value);
        }
    }
}
