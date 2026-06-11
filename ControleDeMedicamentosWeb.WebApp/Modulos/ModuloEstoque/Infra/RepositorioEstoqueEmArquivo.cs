using ControleDeMedicamentosWeb.WebApp.Compartilhado.Infra.Arquivos;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Infra;

public class RepositorioEstoqueEmArquivo : RepositorioBaseEmArquivo<Estoque>, IRepositorioEstoque
{
    public RepositorioEstoqueEmArquivo(ContextoJson contexto) : base(contexto) { }

    protected override List<Estoque> CarregarRegistros()
    {
        return contexto.Estoque;
    }
}