using FluentResults;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Dominio;
//using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Aplicacao;

public class ServicoFornecedor
{
    private readonly IRepositorioFornecedor repositorioFornecedor;

    public ServicoFornecedor(
        IRepositorioFornecedor repositorioFornecedor
        //IRepositorioMedicamento repositorioMedicamento
    )
    {
        this.repositorioFornecedor = repositorioFornecedor;
        //this.repositorioMedicamento = repositorioMedicamento;
    }

    public Result Cadastrar(CadastrarFornecedorDto dto)
    {
        if (ExisteFornecedorComNome(dto.Nome))
            return Falha(nameof(dto.Nome), "Já existe uma Fornecedor com este nome.");

        Fornecedor novaFornecedor = new Fornecedor();

        Result resultadoValidacao = ValidarEntidade(novaFornecedor);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioFornecedor.Cadastrar(novaFornecedor);

        return Result.Ok();
    }

    public Result Editar(EditarFornecedorDto dto)
    {
        if (ExisteFornecedorComNome(dto.Nome, dto.Id))
            return Falha(nameof(dto.Nome), "Já existe uma Fornecedor com este nome.");

        Fornecedor FornecedorAtualizada = new Fornecedor();

        Result resultadoValidacao = ValidarEntidade(FornecedorAtualizada);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        bool conseguiuEditar = repositorioFornecedor.Editar(dto.Id, FornecedorAtualizada);

        if (!conseguiuEditar)
            return Result.Fail("Fornecedor não encontrada.");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Fornecedor? Fornecedor = repositorioFornecedor.SelecionarPorId(id);

        if (Fornecedor == null)
            return Result.Fail("Fornecedor não encontrada.");
        /*
        bool possuiMedicamentos = repositorioMedicamento
            .SelecionarTodos()
            .Any(p => p.Fornecedor.Id == id);
        
        if (possuiMedicamentos)
            return Result.Fail("Esta Fornecedor não pode ser excluída pois possui Medicamentos vinculados.");
        */
        repositorioFornecedor.Excluir(id);

        return Result.Ok();
    }

    public List<ListarFornecedorsDto> SelecionarTodos()
    {
        return repositorioFornecedor
            .SelecionarTodos()
            .Select(c => new ListarFornecedorsDto(c.Id, c.Nome, c.Cor))
            .ToList();
    }

    public Result<DetalhesFornecedorDto> SelecionarPorId(Guid id)
    {
        Fornecedor? Fornecedor = repositorioFornecedor.SelecionarPorId(id);

        if (Fornecedor == null)
            return Result.Fail("Fornecedor não encontrada.");

        return Result.Ok(new DetalhesFornecedorDto(Fornecedor.Id, Fornecedor.Nome, Fornecedor.Cor));
    }

    private bool ExisteFornecedorComNome(string nome, Guid? idIgnorado = null)
    {
        return repositorioFornecedor
            .SelecionarTodos()
            .Any(c =>
                c.Id != idIgnorado &&
                string.Equals(c.Nome, nome, StringComparison.OrdinalIgnoreCase)
            );
    }

    private static Result ValidarEntidade(Fornecedor Fornecedor)
    {
        List<string> erros = Fornecedor.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        return Result.Fail(new Error(erros.First()).WithMetadata("Campo", string.Empty));
    }

    private static Result Falha(string campo, string mensagem)
    {
        return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
    }
}
