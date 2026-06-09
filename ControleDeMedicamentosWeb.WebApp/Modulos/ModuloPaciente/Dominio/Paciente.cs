using ControleDeMedicamentosWeb.WebApp.Compartilhado.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Dominio;

public sealed class Paciente : EntidadeBase<Paciente>
{

    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string CartaoSUS { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;

    public Paciente() { }

    public Paciente(string nome, string telefone, string cartaoSUS, string cpf)
    {
        Nome = nome;
        Telefone = telefone;
        CartaoSUS = cartaoSUS;
        CPF = cpf;
    }
    public override void Atualizar(Paciente entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Telefone = entidadeAtualizada.Telefone;
        CartaoSUS = entidadeAtualizada.CartaoSUS;
        CPF = entidadeAtualizada.CPF;
    }

    public string VerificarTelefone(string telefone)
    {
        string apenasNumeros = System.Text.RegularExpressions.Regex.Replace(telefone ?? "", @"[^\d]", "");

        int tamanho = apenasNumeros.Length;

        if (tamanho == 10)
        {
            return long.Parse(apenasNumeros).ToString(@"(00) 0000-0000");
        }
        else if (tamanho == 11)
        {
            return long.Parse(apenasNumeros).ToString(@"(00) 0 0000-0000");
        }
        else
        {
            return "";
        }
    }

    public string VerificarCPF(string cpf)
    {
        string apenasNumeros = System.Text.RegularExpressions.Regex.Replace(cpf ?? "", @"[^\d]", "");
        int tamanho = apenasNumeros.Length;

        if (tamanho == 11)
        {
            return Convert.ToUInt64(apenasNumeros).ToString(@"000\.000\.000\-00");
        }
        else
        {
            return "";
        }
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo \"Nome\" deve ser preenchido.");

        else if (Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 3 e 100 caracteres.");

        if (string.IsNullOrWhiteSpace(Telefone))
            erros.Add("O campo \"Telefone\" deve ser preenchido.");

        else if (VerificarTelefone(Telefone) == "")
            erros.Add("O campo \"Telefone\" é inválido (formato validado: 10-11 dígitos).");

        if (string.IsNullOrWhiteSpace(CartaoSUS))
            erros.Add("O campo \"Cartão SUS\" deve ser preenchido.");

        else if (CartaoSUS.Length != 15)
            erros.Add("O campo \"CartaoSUS\" deve conter 15 caracteres.");

        if (string.IsNullOrWhiteSpace(CPF))
            erros.Add("O campo \"CPF\" deve ser preenchido.");

        else if (VerificarCPF(CPF) == "")
            erros.Add("O campo \"CPF\" é inválido (formato validado: 11 dígitos).");

        return erros;
    }
}
