using System.Text.RegularExpressions;

namespace VisitorService.Domain.Shared;

public sealed class Contract : Notifiable
{
    public Contract Requires()
        => this;

    public Contract IsNotNull (object? value, string key)
    {
        if (value == null)
        AddNotification(key, "O valor não pode ser nulo.");

        return this;
    }

    public Contract IsNotNullOrWhiteSpace(string? value, string key)
    {
        if(string.IsNullOrWhiteSpace(value))
        AddNotification(key, "O valor não pode ser nulo ou ter espaços em branco.");

        return this;
    }

    public Contract MinLength(string? value, int min, string key)
    {
        if(!string.IsNullOrEmpty(value) && value.Length < min)
            AddNotification(key, $"A quantidade mínima de caracteres é {min}");

            return this;
    }

    public Contract MaxLength(string? value, int max, string key)
    {
        if(!string.IsNullOrEmpty(value) && value.Length > max)
            AddNotification(key, $"A quantidade máxima de caracteres é {max} ");

            return this;
    }

    public Contract HasLength(string? value, int equal, string key)
    {
        if(!string.IsNullOrEmpty(value) && value.Length != equal)
            AddNotification(key, $"A quantidade de caracteres deve ser igual a {equal}");

        return this;
    }

    public Contract IsRepeatedSequence(string? value, string key)
    {
        if(!string.IsNullOrEmpty(value) && value.Length > 1)
        {
            if(value.All(c => c == value[0]))
            {
                AddNotification(key, "O valor não deve conter todos os caracteres iguais");
            }
        }

        return this;
    }

    public Contract IsCnpj (string? value, string key)
    {
        if ((!string.IsNullOrEmpty(value)) && (value.Length != 14 || !ValidateDigitCalculation(value)))
            AddNotification(key, "Cnpj ínvalido");

        return this;
    }
    private static bool ValidateDigitCalculation(string cnpj)
{
    int[] multiplicator1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
    int[] multiplicator2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

    string tempCnpj = cnpj.Substring(0, 12);
    int sum = 0;

    for (int i = 0; i < 12; i++)
        sum += (tempCnpj[i] - '0') * multiplicator1[i];

    int remainder = (sum % 11);
    remainder = remainder < 2 ? 0 : 11 - remainder;

    string digit = remainder.ToString();
    tempCnpj += digit;
    sum = 0;

    for (int i = 0; i < 13; i++)
        sum += (tempCnpj[i] - '0') * multiplicator2[i];

    remainder = (sum % 11);
    remainder = remainder < 2 ? 0 : 11 - remainder;

    digit += remainder.ToString();

    return cnpj.EndsWith(digit);
}

    public Contract IsEmail(string value, string key)
    {   
        var regex = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
        if(!string.IsNullOrEmpty(value) && value.Length <= 254 && !Regex.IsMatch(value, regex))
           AddNotification(key,"Formato de Email inválido");

        return this;
    }

    public Contract IsStrongPassword(string value, string key)
    {
        var regex = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[!@#$%^&*(),.?"":{}|<>]).+$";

        if(!string.IsNullOrEmpty(value) && value.Length <= 20 && !Regex.IsMatch(value, regex))
           AddNotification(key,"A senha deve conter maiúsculas, minúsculas, números e caracteres especiais.");

        return this;
    }

    public Contract HasNoSpaces(string value, string key)
    {
        if(!string.IsNullOrEmpty(value) && value.Contains(" "))
             AddNotification(key,"Não pode haver espaços em branco no meio");
        
        return this;
    }

    public Contract IsPhoneNumber (string value, string key)
    {
        var regex = new Regex(@"^(\+55\s?)?(\(?\d{2}\)?\s?)?\d{4,5}-?\d{4}$");

        if(!string.IsNullOrEmpty(value) && value.Length > 20 && !regex.IsMatch(value))
            AddNotification(key, "O formato do telefone é inválido. Use (XX) XXXXX-XXXX ou +55XXXXXXXXXXX.");

        return this;
    }
    public Contract PhoneLengthRange(string value, string key)
    {
        if((!string.IsNullOrEmpty(value)) && (value.Length < 10 || value.Length > 13))
            AddNotification(key, "O número de telefone deve ter entre 10 e 13 dígitos.");

        return this;
    }
}