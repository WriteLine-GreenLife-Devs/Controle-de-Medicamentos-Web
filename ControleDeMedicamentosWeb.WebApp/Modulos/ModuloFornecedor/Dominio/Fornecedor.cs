using ControleDeMedicamentosWeb.WebApp.Compartilhado.Dominio;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;

public class Fornecedor : EntidadeBase<Fornecedor>
{
     public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string CNPJ { get; set; } = string.Empty;

    public Fornecedor() { }
    public Fornecedor(string nome, string telefone, string cnpj)
    {
        Nome = nome;
        Telefone = telefone;
        CNPJ = cnpj;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo \"Nome\" deve ser preenchido.");

        else if (Nome.Length > 50)
            erros.Add("O campo \"Nome\" deve conter no máximo 50 caracteres.");

        if (string.IsNullOrWhiteSpace(Telefone))
            erros.Add("O campo \"Telefone\" deve ser preenchido.");
        
        else if (Telefone.Length > 11)
            erros.Add("O campo \"Telefone\" deve conter no máximo 50 caracteres.");

        if (string.IsNullOrWhiteSpace(CNPJ))
            erros.Add("O campo \"CNPJ\" deve ser preenchido.");
        
        else if (CNPJ.Length > 14)
            erros.Add("O campo \"CNPJ\" deve conter no máximo 50 caracteres.");

        return erros;
    }

    public override void Atualizar(Fornecedor entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Telefone = entidadeAtualizada.Telefone;
        CNPJ = entidadeAtualizada.CNPJ;
    }
}