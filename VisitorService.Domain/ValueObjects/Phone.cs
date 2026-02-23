using System.Text.RegularExpressions;
using VisitorService.Domain.Shared;

namespace VisitorService.Domain.ValueObject
{
    public sealed class Phone : Notifiable
    {
        public string? Value { get; }
        private Phone(string? value) => Value = value;

        public static Phone Create(string? value)
        {
            var normalized = value?.Trim() ?? string.Empty;

            var contract = new Contract()
                .IsNotNullOrWhiteSpace(normalized, "Phone")
                .IsPhoneNumber(normalized,"Phone");

            var cleanValue = Regex.Replace(normalized, @"\D", "");

            contract.PhoneLengthRange(cleanValue, "Phone");

            var phone = new Phone(cleanValue);

            if (contract.HasErrors)
            {
                phone.AddRangeNotification(contract.Errors);
            }
            return phone;
        }
        public static Phone FromDatabase(string value)
        {
            return new Phone(value);
        }
    }
}
