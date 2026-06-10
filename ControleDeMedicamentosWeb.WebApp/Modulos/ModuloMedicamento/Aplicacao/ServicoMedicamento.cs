using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Dominio;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Dominio;
using FluentResults;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Aplicacao;

public class ServicoMedicamento
{
    private readonly IRepositorioMedicamento repositorioMedicamento;
    private readonly IRepositorioFornecedor repositorioFornecedor;

    public ServicoMedicamento(IRepositorioMedicamento repositorioMedicamento, IRepositorioFornecedor repositorioFornecedor)
    {
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioFornecedor = repositorioFornecedor;
    }

    private static Result Falha(string campo, string mensagem)
    {
        return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
    }
    private static Result ValidarEntidade( Medicamento medicamento)
    {
        List<string> erros = medicamento.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        return Result.Fail(new Error(erros.First()).WithMetadata("Campo", string.Empty));
    }

    public Result Cadastrar(CadastrarMedicamentoDto dto)
    {
         if (repositorioFornecedor.SelecionarPorId(dto.idFornecedor) == null)
            return Falha(nameof(dto.idFornecedor), "Fornecedor não encontrado.");

        Medicamento novoMedicamento = new(
            nome: dto.nome,
            descricao: dto.descricao,
            quantidade: dto.quantidade,
            idFornecedor: dto.idFornecedor
        );

        Result resultadoValidacao = ValidarEntidade(novoMedicamento);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioMedicamento.Cadastrar(novoMedicamento);

        return Result.Ok();
    }
    public Result Excluir(Guid id)
    {
        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(id);

        if (medicamento == null)
            return Result.Fail("Medicamento não encontrado.");

        repositorioMedicamento.Excluir(id);

        return Result.Ok();
    }
    public Result Editar(EditarMedicamentoDto dto)
    {
        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(dto.Id);

        if (medicamento == null)
            return Result.Fail("Medicamento não encontrado.");

        Medicamento medicamentoAtualizado = new Medicamento(
            nome: dto.nome,
            descricao: dto.descricao,
            quantidade: dto.quantidade,
            idFornecedor: dto.idFornecedor
        );

        Result resultadoValidacao = ValidarEntidade(medicamentoAtualizado);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioMedicamento.Editar(dto.Id, medicamentoAtualizado);

        return Result.Ok();
    }
    public List<ListarMedicamentosDto> SelecionarTodos()
    {
        return repositorioMedicamento
            .SelecionarTodos()
            .Select(m =>
            {
                string fornecedorNome = repositorioFornecedor.SelecionarPorId(m.IdFornecedor)?.Nome ?? string.Empty;

                return new ListarMedicamentosDto(
                    m.Id,
                    m.Nome,
                    m.Descricao,
                    m.Quantidade,
                    m.IdFornecedor,
                    fornecedorNome
                );
            })
            .ToList();
    }

     public Result<ListarMedicamentosDto> SelecionarPorId(Guid id)
    {
        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(id);

        if (medicamento == null)
            return Result.Fail("Medicamento não encontrado.");

        string fornecedorNome = repositorioFornecedor.SelecionarPorId(medicamento.IdFornecedor)?.Nome ?? string.Empty;

        return Result.Ok(new ListarMedicamentosDto(
            medicamento.Id,
            medicamento.Nome,
            medicamento.Descricao,
            medicamento.Quantidade,
            medicamento.IdFornecedor,
            fornecedorNome
        ));
    }
}
