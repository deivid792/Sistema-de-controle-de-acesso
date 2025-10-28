using VisitorService.Domain.Shared.results;

namespace VisitorService.Domain.ValueObject
{
    public sealed class Name
    {
        public string Value { get; }

        private Name(string value)
        {
            Value = value;
        }

        public static Result<Name> Create(string value)
        {
            if (value == null)
                return Result<Name>.Fail("O nome não pode ser vazio");

            if (value.Trim().Length == 0)
                return Result<Name>.Fail("O nome não pode ser vazio ou apenas espaços");

            value = value.Trim();

            if (value.Length < 2)
                return Result<Name>.Fail("O nome deve ter pelo menos 2 caracteres.");

            if (value.Length > 100)
                return Result<Name>.Fail("O nome deve ter no máximo 100 caracteres.");

            return Result<Name>.Success(new Name(value));
        }
    }
}
