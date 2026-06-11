using ControleDeMedicamentosWeb.WebApp.Compartilhado.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Dominio;

public sealed class Funcionario : EntidadeBase<Funcionario>
{

    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;

    public Funcionario() { }

    public Funcionario(string nome, string telefone, string cpf)
    {
        Nome = nome;
        Telefone = telefone;
        CPF = cpf;
    }
    public override void Atualizar(Funcionario entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Telefone = entidadeAtualizada.Telefone;
        CPF = entidadeAtualizada.CPF;
    }

    public string VerificarTelefone(string telefone)
    {
        string apenasNumeros = System.Text.RegularExpressions.Regex.Replace(telefone ?? "", @"[^\d]", "");

        int tamanho = apenasNumeros.Length;

        if (tamanho == 10)
        {
            Telefone = long.Parse(apenasNumeros).ToString(@"(00) 0000-0000");
            return long.Parse(apenasNumeros).ToString(@"(00) 0000-0000");
        }
        else if (tamanho == 11)
        {
            Telefone = long.Parse(apenasNumeros).ToString(@"(00) 0 0000-0000");
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
            CPF = Convert.ToUInt64(apenasNumeros).ToString(@"000\.000\.000\-00");
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
            erros.Add("O campo \"Telefone\" é inválido.");

        if (string.IsNullOrWhiteSpace(CPF))
            erros.Add("O campo \"CPF\" deve ser preenchido.");

        else if (VerificarCPF(CPF) == "")
            erros.Add("O campo \"CPF\" é inválido.");

        return erros;
    }
}
