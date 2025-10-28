using System.Text.RegularExpressions;
using VisitorService.Domain.Shared.results;

namespace VisitorService.Domain.ValueObject
{
    public sealed class Phone
    {
        public string Value { get; private set; }

        private Phone(string value)
        {
            Value = value;
        }

        public static Result<Phone> Create(string? phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return Result<Phone>.Fail("O telefone é obrigatório.");

            phone = phone.Trim();

            var regex = new Regex(@"^(\+55\s?)?(\(?\d{2}\)?\s?)?\d{4,5}-?\d{4}$");

            if (!regex.IsMatch(phone))
                return Result<Phone>.Fail("O formato do telefone é inválido. Use (XX) XXXXX-XXXX ou +55XXXXXXXXXXX.");

            var digitsOnly = Regex.Replace(phone, @"\D", "");

            if (digitsOnly.Length < 10 || digitsOnly.Length > 13)
                return Result<Phone>.Fail("O número de telefone deve ter entre 10 e 13 dígitos.");

            return Result<Phone>.Success(new Phone(digitsOnly));
        }
    }
}
