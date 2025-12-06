using System.Text.RegularExpressions;
using VisitorService.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace VisitorService.Domain.ValueObject
{
    [Owned]
    public sealed record Email
    {
        public string Value { get; }
        private readonly Notification _notification = new();
        public IReadOnlyCollection<NotificationItem> Notification => _notification.Errors;
        public bool HasErrors => _notification.HasErrors;
        private Email(string value)
        {
            Value = value;
        }

        public static Email Create(string value)
        {
            var tempEmail = new Email(value);
            var notification = tempEmail._notification;

            if (string.IsNullOrWhiteSpace(value))
            {
                notification.add("Email", "Email não pode estar vazio ou apenas espaços");
                return tempEmail;
            }
            var normalized = tempEmail.Value.Trim().ToLower();

            if (normalized.Length > 254)
                notification.add("Email", "Email excede tamanho máximo permitido (254 caracteres)");

            var regex = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
            if (!Regex.IsMatch(normalized, regex))
                notification.add("Email", "Formato de email inválido");

            if (notification.HasErrors)
                return tempEmail;

            return new Email(normalized);
        }

        public static Email FromDatabase(string value)
        {
            return new Email(value);
        }

    }

}