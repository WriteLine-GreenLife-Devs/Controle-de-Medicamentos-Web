using FluentResults;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Dominio;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Dominio;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Dominio;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Aplicacao;

public class ServicoEstoque
{
    private readonly IRepositorioEstoque repositorioEstoque;

    public ServicoEstoque(IRepositorioEstoque repositorioEstoque)
    {
        this.repositorioEstoque = repositorioEstoque;
    }

    public Result CadastrarEntrada(CadastrarEntradaDto dto)
    {
        if (dto.Quantidade <= 0)
            return Falha(nameof(dto.Quantidade), "Quantidade deve ser positiva.");

        var entrada = new Estoque(dto.Data, 
                                  new Medicamento { Id = dto.MedicamentoId }, 
                                  new Funcionario { Id = dto.FuncionarioId }, 
                                  dto.Quantidade);

        Result resultadoValidacao = ValidarEntidade(entrada);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioEstoque.Cadastrar(entrada);

        return Result.Ok();
    }

    public Result CadastrarSaida(CadastrarSaidaDto dto)
    {
        if (dto.Medicamentos.Count == 0)
            return Falha(nameof(dto.Medicamentos), "É necessário informar ao menos um medicamento.");

        var medicamentosSaida = dto.Medicamentos
            .Select(m => new MedicamentoSaida(new Medicamento { Id = m.MedicamentoId }, m.Quantidade))
            .ToList();

        var saida = new Estoque(dto.Data, new Paciente { Id = dto.PacienteId }, medicamentosSaida);

        Result resultadoValidacao = ValidarEntidade(saida);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        foreach (var med in medicamentosSaida)
        {
            if (med.Quantidade <= 0)
                return Falha(nameof(med.Quantidade), "Quantidade deve ser positiva.");
        }

        repositorioEstoque.Cadastrar(saida);

        return Result.Ok();
    }

    public List<ListarEntradaDto> SelecionarEntradas()
    {
        return repositorioEstoque
            .SelecionarTodos()
            .Where(e => e.TipoOperacao == "Entrada")
            .Select(e => new ListarEntradaDto(e.Id, e.Data, e.MedicamentoEntrada?.Nome ?? "", e.FuncionarioEntrada?.Nome ?? "", e.QuantidadeEntrada))
            .ToList();
    }

    public List<ListarSaidaDto> SelecionarSaidas()
    {
        return repositorioEstoque
            .SelecionarTodos()
            .Where(e => e.TipoOperacao == "Saída")
            .Select(e => new ListarSaidaDto(e.Id, e.Data, e.PacienteSaida?.Nome ?? "", e.MedicamentosSaida.Select(m => m.Medicamento?.Nome ?? "").ToList(), e.QuantidadeSaida))
            .ToList();
    }

    private static Result ValidarEntidade(Estoque estoque)
    {
        List<string> erros = estoque.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        return Result.Fail(new Error(erros.First()).WithMetadata("Campo", string.Empty));
    }

    private static Result Falha(string campo, string mensagem)
    {
        return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
    }
}