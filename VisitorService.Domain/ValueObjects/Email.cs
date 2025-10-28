using System.Text.RegularExpressions;
using VisitorService.Domain.Shared.results;

namespace VisitorService.Domain.ValueObject
{
    public class Email
    {
        public string Value { get; }

        private Email(string value)
        {
            Value = value;
        }

        public static Result<Email> create(string value)
        {
            if (value == null)
                return Result<Email>.Fail("Email não pode ser nulo");

            if (value.Trim().Length == 0)
                return Result<Email>.Fail("Email não pode estar vazio ou apenas espaços");

            value = value.Trim();

            if (value.Length > 254)
                return Result<Email>.Fail("Email excede tamanho máximo permitido (254 caracteres)");

            var regex = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
            if (!Regex.IsMatch(value, regex))
                return Result<Email>.Fail("Formato de email inválido");

            value = value.ToLower();

            return Result<Email>.Success(new Email(value));
        }
    }
}