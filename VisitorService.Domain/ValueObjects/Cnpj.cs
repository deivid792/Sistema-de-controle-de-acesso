using System.Text.RegularExpressions;
using VisitorService.Domain.Shared.results;

namespace VisitorService.Domain.ValueObject
{
    public sealed class Cnpj
    {
        public string Value { get; private set; }

        private Cnpj(string value)
        {
            Value = value;
        }

        public static Result<Cnpj> Create(string? cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return Result<Cnpj>.Fail("O CNPJ é obrigatório.");

            // Remove caracteres não numéricos
            cnpj = Regex.Replace(cnpj, @"\D", "");

            if (cnpj.Length != 14)
                return Result<Cnpj>.Fail("O CNPJ deve conter 14 dígitos.");

            // Impede CNPJs com todos os dígitos iguais
            if (cnpj.All(c => c == cnpj[0]))
                return Result<Cnpj>.Fail("O CNPJ é inválido.");

            // Validação dos dígitos verificadores
            if (!IsValidCnpj(cnpj))
                return Result<Cnpj>.Fail("O CNPJ informado é inválido.");

            // Normaliza formato (XX.XXX.XXX/XXXX-XX)
            string formatted = Convert.ToUInt64(cnpj).ToString(@"00\.000\.000\/0000\-00");

            return Result<Cnpj>.Success(new Cnpj(formatted));
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

    }
}