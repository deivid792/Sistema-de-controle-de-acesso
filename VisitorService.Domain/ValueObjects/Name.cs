using VisitorService.Domain.Shared;

namespace VisitorService.Domain.ValueObject
{
    public sealed class Name : Notifiable
    {
        public string? Value { get; private set; }

        private Name() { }
        private Name(string? value) => Value = value;

        public static Name Create(string? value)
        {
            var normalized = value?.Trim() ?? string.Empty;

            var contract = new Contract()
            .Requires()
            .IsNotNullOrWhiteSpace(normalized, "Name")
            .MinLength(normalized, 2, "Name")
            .MaxLength(normalized, 100, "Name");

            var name = new Name(normalized);

            if (contract.HasErrors)
                name.AddRangeNotification(contract.Errors);

            return name;
        }

        [Obsolete("Use apenas para mapeamento do EF Core. Para criar novos nomes, use Name.Create().", false)]
        public static Name FromDatabase(string? value) => new Name(value);

    }
}
