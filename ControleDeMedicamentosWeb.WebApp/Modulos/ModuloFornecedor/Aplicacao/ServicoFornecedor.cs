using FluentResults;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Dominio;
using ControleDeMedicamentosWeb.WebApp.Compartilhado;
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

        if (ExisteFornecedorComTelefone(dto.Telefone))
            return Falha(nameof(dto.Telefone), "Já existe um fornecedor com este telefone.");

        if (ExisteFornecedorComCnpj(dto.CNPJ))
            return Falha(nameof(dto.CNPJ), "Já existe um fornecedor com este CNPJ.");

        Fornecedor novaFornecedor = new Fornecedor(dto.Nome, dto.Telefone, dto.CNPJ);

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

        Fornecedor FornecedorAtualizada = new Fornecedor(dto.Nome, dto.Telefone, dto.CNPJ);

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

    public List<ListarFornecedoresDto> SelecionarTodos()
    {
        return repositorioFornecedor
            .SelecionarTodos()
            .Select(f => new ListarFornecedoresDto(f.Id, f.Nome, f.Telefone, f.CNPJ))
            .ToList();
    }

    public Result<DetalhesFornecedoresDto> SelecionarPorId(Guid id)
    {
        Fornecedor? Fornecedor = repositorioFornecedor.SelecionarPorId(id);

        if (Fornecedor == null)
            return Result.Fail("Fornecedor não encontrada.");

        return Result.Ok(new DetalhesFornecedoresDto(Fornecedor.Id, Fornecedor.Nome, Fornecedor.Telefone, Fornecedor.CNPJ));
    }

    private bool ExisteFornecedorComNome(string nome, Guid? idIgnorado = null)
    {
        string nomeNormalizado = Servico.NormalizarTexto(nome);

        return repositorioFornecedor
            .SelecionarTodos()
            .Any(f =>
                f.Id != idIgnorado &&
                Servico.NormalizarTexto(f.Nome) == nomeNormalizado
            );
    }

    private bool ExisteFornecedorComTelefone(string telefone, Guid? idIgnorado = null)
    {
        string telefoneNormalizado = Servico.NormalizarNumeros(telefone);

        return repositorioFornecedor
            .SelecionarTodos()
            .Any(f =>
                f.Id != idIgnorado &&
                Servico.NormalizarNumeros(f.Telefone) == telefoneNormalizado
            );
    }

    private bool ExisteFornecedorComCnpj(string cnpj, Guid? idIgnorado = null)
    {
        string cnpjNormalizado = Servico.NormalizarNumeros(cnpj);

        return repositorioFornecedor
            .SelecionarTodos()
            .Any(f =>
                f.Id != idIgnorado &&
                Servico.NormalizarNumeros(f.CNPJ) == cnpjNormalizado
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
