using System.Text.RegularExpressions;
using VisitorService.Domain.Shared.results;

namespace VisitorService.Domain.ValueObject
{

    public class Password
    {
        public string Value { get; }

        private Password(string value)
        {
            Value = value;
        }

        public static Result<Password> Create(string value)
        {
            if (value == null)
                return Result<Password>.Fail("A senha não pode ser nula");

            if (value.Trim().Length == 0)
                return Result<Password>.Fail("A senha não pode estar vazia ou apenas espaços");

            value = value.Trim();

            if (value.Length < 8)
                return Result<Password>.Fail("A senha deve conter pelo menos 8 caracteres");

            if (value.Length > 128)
                return Result<Password>.Fail("A senha não deve exceder 128 caracteres");

            if (!Regex.IsMatch(value, "[A-Z]"))
                return Result<Password>.Fail("A senha deve conter pelo menos uma letra maiúscula");

            if (!Regex.IsMatch(value, "[a-z]"))
                return Result<Password>.Fail("A senha deve conter pelo menos uma letra minúscula");

            if (!Regex.IsMatch(value, "[0-9]"))
                return Result<Password>.Fail("A senha deve conter pelo menos um número");

            if (!Regex.IsMatch(value, "[!@#$%^&*(),.?\":{}|<>]"))
                return Result<Password>.Fail("A senha deve conter pelo menos um caractere especial");

            if (value.Contains(" "))
                return Result<Password>.Fail("A senha não deve conter espaços em branco no meio");

            return Result<Password>.Success(new Password(value));
        }
    }
}