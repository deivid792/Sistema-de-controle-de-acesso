using System.Text.RegularExpressions;
using VisitorService.Domain.Shared;

namespace VisitorService.Domain.ValueObject
{
    public sealed class Cnpj
    {
        public string Value { get; private set; }

        private readonly Notification _notification = new();
        public IReadOnlyCollection<NotificationItem> Notification => _notification.Errors;
        public bool HasErrors => _notification.HasErrors;

        private Cnpj(string value)
        {
            Value = value;
        }

        public static Cnpj Create(string? cnpj)
        {
            var tempCnpj = new Cnpj(cnpj);
            var notification = tempCnpj._notification;

            if (string.IsNullOrWhiteSpace(cnpj))
                notification.add("CNPJ", "O CNPJ não pode ser nulo");

            var normalized = Regex.Replace(cnpj, @"\D", "");

            if (normalized.Length != 14)
                notification.add("CNPJ", "O CNPJ deve conter 14 dígitos.");

            if (normalized.All(c => c == cnpj[0]))
                notification.add("CNPJ", "O CNPJ é inválido.");

            if (!IsValidCnpj(normalized))
                notification.add("CNPJ", "O CNPJ informado é inválido.");

            string formatted = Convert.ToUInt64(normalized).ToString(@"00\.000\.000\/0000\-00");

            return new Cnpj(formatted);
        }

        private static bool IsValidCnpj(string cnpj)
        {
            int[] multiplicator1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicator2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCnpj, digit;
            int sum, remainder;

            tempCnpj = cnpj.Substring(0, 12);
            sum = 0;
            for (int i = 0; i < 12; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multiplicator1[i];

            remainder = (sum % 11);
            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;

            digit = remainder.ToString();
            tempCnpj += digit;
            sum = 0;
            for (int i = 0; i < 13; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multiplicator2[i];

            remainder = (sum % 11);
            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;

            digit += remainder.ToString();

            return cnpj.EndsWith(digit);
        }
        public static Cnpj FromDatabase(string value)
    => new Cnpj(value);

    }
}