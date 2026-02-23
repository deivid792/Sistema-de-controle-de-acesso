using VisitorService.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace VisitorService.Domain.ValueObject
{
    [Owned]
    public sealed class Email : Notifiable
    {
        public string? Value { get; private set; }

        private Email() {}
        private Email(string value)
        {
            Value = value;
        }

        public static Email Create(string value)
        {
            var normalized = value?.Trim().ToLower() ?? string.Empty;

            var contract = new Contract()
            .Requires()
            .IsNotNullOrWhiteSpace(normalized,"Email")
            .MaxLength(normalized, 50, "Email")
            .IsEmail(normalized, "Email");

            var email = new Email(normalized);

            if (contract.HasErrors)
                email.AddRangeNotification(contract.Errors);

            return email;
        }

    }

}