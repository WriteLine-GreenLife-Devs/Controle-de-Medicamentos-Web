using System.Text.RegularExpressions;

namespace ControleDeMedicamentosWeb.WebApp;

class Servico
{
    public static string NormalizarTexto(string texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
            return string.Empty;

        // Remove acentos
        string normalizado = texto.Normalize(System.Text.NormalizationForm.FormD);
        var chars = normalizado
            .Where(c => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark)
            .ToArray();

        normalizado = new string(chars);

        // Remove caracteres especiais e espaços extras
        normalizado = Regex.Replace(normalizado, @"[^a-zA-Z0-9]", "");

        return normalizado.ToLowerInvariant();
    }

    public static string NormalizarNumeros(string texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
            return string.Empty;

        // Mantém apenas dígitos
        return Regex.Replace(texto, @"\D", "");
    }

    public static string FormatarTelefone(string telefone)
    {
        if (string.IsNullOrWhiteSpace(telefone))
            return string.Empty;

        string numeros = Regex.Replace(telefone, @"\D", "");

        if (numeros.Length == 8)
            return Regex.Replace(numeros, @"(\d{4})(\d{4})", "$1-$2");

        if (numeros.Length == 9)
            return Regex.Replace(numeros, @"(\d{5})(\d{4})", "$1-$2");

        if (numeros.Length == 10)
            return Regex.Replace(numeros, @"(\d{2})(\d{4})(\d{4})", "($1) $2-$3");

        if (numeros.Length == 11)
            return Regex.Replace(numeros, @"(\d{2})(\d{5})(\d{4})", "($1) $2-$3");

        return telefone;
    }

    public static string FormatarCNPJ(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj))
            return string.Empty;

        string numeros = Regex.Replace(cnpj, @"\D", "");

        if (numeros.Length == 14)
            return Regex.Replace(numeros, @"(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})", "$1.$2.$3/$4-$5");

        return cnpj;
    }

    public static string FormatarCpf(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return string.Empty;

        string numeros = Regex.Replace(cpf, @"\D", "");

        if (numeros.Length == 11)
            return Regex.Replace(numeros, @"(\d{3})(\d{3})(\d{3})(\d{2})", "$1.$2.$3-$4");

        return cpf;
    }

    public static string FormatarCartaoSus(string cartaoSus)
    {
        if (string.IsNullOrWhiteSpace(cartaoSus))
            return string.Empty;

        string numeros = Regex.Replace(cartaoSus, @"\D", "");

        // Cartão Nacional de Saúde (CNS)
        if (numeros.Length == 15)
            return Regex.Replace(numeros, @"(\d{3})(\d{4})(\d{4})(\d{4})", "$1 $2 $3 $4");

        return cartaoSus;
    }
}