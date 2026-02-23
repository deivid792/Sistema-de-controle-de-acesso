using System.Text.RegularExpressions;
using VisitorService.Domain.Shared;

namespace VisitorService.Domain.ValueObject
{
    public sealed class Cnpj : Notifiable
    {
        public string? Value { get; private set; }

        private Cnpj(string? value) => Value = value;

        public static Cnpj Create(string? value)
        {
            var normalized = value?.Trim() ?? string.Empty;

            var cleanValue = Regex.Replace(normalized, @"\D", "");

            var contract = new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(cleanValue, "Cnpj")
                .HasLength(cleanValue, 14, "Cnpj")
                .IsRepeatedSequence(cleanValue, "Cnpj")
                .IsCnpj(cleanValue, "Cnpj");

            string formatted = !contract.HasErrors?
            Convert.ToUInt64(cleanValue).ToString(@"00\.000\.000\/0000\-00")
            :cleanValue;

            var cnpj = new Cnpj(formatted);

            cnpj.AddRangeNotification(contract.Errors);

            return cnpj;
        }

    }
}