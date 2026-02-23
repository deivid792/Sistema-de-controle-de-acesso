using VisitorService.Domain.Shared;

namespace VisitorService.Domain.ValueObject
{

    public class Password : Notifiable
    {
        public string? Value { get; }

        private Password(string? value) => Value = value;

        public static Password Create(string value)
        {
            var normalized = value?.Trim() ?? string.Empty;

            var contract = new Contract()
                .IsNotNullOrWhiteSpace(normalized, "Password")
                .MinLength(normalized, 8 , "Password")
                .MaxLength(normalized, 20 , "Password")
                .IsStrongPassword(normalized, "Password")
                .HasNoSpaces(normalized, "Password");

            var password = new Password(normalized);

            if (contract.HasErrors)
            {
                password.AddRangeNotification(contract.Errors);
            }

            return password;
        }

        public static Password FromHash(string value)
        {
            return new Password(value);
        }
    }
}