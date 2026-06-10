using ControleDeMedicamentosWeb.WebApp.Compartilhado.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Dominio;

public sealed class Medicamento : EntidadeBase<Medicamento>
{

    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int Quantidade { get; set; } = 0;
    public Guid IdFornecedor { get; set; } = Guid.Empty;

    public Medicamento() { }

    public Medicamento(string nome, string descricao, int quantidade, Guid idFornecedor)
    {
        Nome = nome;
        Descricao = descricao;
        Quantidade = quantidade;
        IdFornecedor = idFornecedor;
    }
    public override void Atualizar(Medicamento entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Descricao = entidadeAtualizada.Descricao;
        Quantidade = entidadeAtualizada.Quantidade;
        IdFornecedor = entidadeAtualizada.IdFornecedor;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo \"Nome\" deve ser preenchido.");

        else if (Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 3 e 100 caracteres.");

        if (string.IsNullOrWhiteSpace(Descricao))
            erros.Add("O campo \"Descrição\" deve ser preenchido.");

        else if (Descricao.Length < 5 || Descricao.Length > 255)
            erros.Add("O campo \"Descrição\" deve conter entre 5 e 255 caracteres.");

        if (Quantidade <= 0)
            erros.Add("O campo \"Quantidade\" deve ser um número positivo.");

        if (IdFornecedor == Guid.Empty)
            erros.Add("O campo \"Fornecedor\" deve ser selecionado.");

        return erros;
    }
}
