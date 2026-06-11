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
}