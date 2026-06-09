using ControleDeMedicamentosWeb.WebApp.Compartilhado.Infra.Arquivos;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Infra;

public class RepositorioFornecedor : RepositorioBase<Fornecedor>, IRepositorioFornecedor
{
    public RepositorioFornecedor(ContextoJson contexto) : base(contexto) { }

    protected override List<Fornecedor> CarregarRegistros()
    {
        return contexto.Fornecedor;
    }
}
