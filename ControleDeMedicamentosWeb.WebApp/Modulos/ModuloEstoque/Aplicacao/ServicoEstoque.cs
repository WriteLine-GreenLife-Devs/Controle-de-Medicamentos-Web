using FluentResults;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Dominio;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Dominio;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Dominio;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Aplicacao;

public class ServicoEstoque
{
    private readonly IRepositorioEstoque repositorioEstoque;
    private readonly IRepositorioPaciente repositorioPaciente;
    private readonly IRepositorioMedicamento repositorioMedicamento;
    private readonly IRepositorioFuncionario repositorioFuncionario;

    public ServicoEstoque(
        IRepositorioEstoque repositorioEstoque,
        IRepositorioPaciente repositorioPaciente,
        IRepositorioMedicamento repositorioMedicamento,
        IRepositorioFuncionario repositorioFuncionario)
    {
        this.repositorioEstoque = repositorioEstoque;
        this.repositorioPaciente = repositorioPaciente;
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioFuncionario = repositorioFuncionario;
    }

    private static Result Falha(string campo, string mensagem)
    {
        return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
    }

    private static Result ValidarEntidade(Estoque estoque)
    {
        List<string> erros = estoque.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        string erro = erros.First();
        string campo = erro.Contains("Medicamento") ? nameof(estoque.MedicamentoEntrada)
            : erro.Contains("Funcionário") ? nameof(estoque.FuncionarioEntrada)
            : erro.Contains("Paciente") ? nameof(estoque.PacienteSaida)
            : erro.Contains("Quantidade") ? nameof(estoque.QuantidadeEntrada)
            : string.Empty;

        return Result.Fail(new Error(erro).WithMetadata("Campo", campo));
    }

    public List<Paciente> SelecionarPacientes()
    {
        return repositorioPaciente.SelecionarTodos();
    }

    public List<Medicamento> SelecionarMedicamentos()
    {
        return repositorioMedicamento.SelecionarTodos();
    }

    public List<Funcionario> SelecionarFuncionarios()
    {
        return repositorioFuncionario.SelecionarTodos();
    }

    public Result CadastrarEntrada(CadastrarEntradaDto dto)
    {
        if (dto.Quantidade <= 0)
            return Falha(nameof(dto.Quantidade), "Quantidade deve ser positiva.");

        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(dto.MedicamentoId);
        if (medicamento == null)
            return Falha(nameof(dto.MedicamentoId), "Medicamento não encontrado.");

        Funcionario? funcionario = repositorioFuncionario.SelecionarPorId(dto.FuncionarioId);
        if (funcionario == null)
            return Falha(nameof(dto.FuncionarioId), "Funcionário não encontrado.");

        Estoque entrada = new(dto.Data, medicamento, funcionario, dto.Quantidade);

        Result resultadoValidacao = ValidarEntidade(entrada);
        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioEstoque.Cadastrar(entrada);

        return Result.Ok();
    }

    public Result CadastrarSaida(CadastrarSaidaDto dto)
    {
        if (dto.Medicamentos == null || dto.Medicamentos.Count == 0)
            return Falha(nameof(dto.Medicamentos), "É necessário informar ao menos um medicamento.");

        Paciente? paciente = repositorioPaciente.SelecionarPorId(dto.PacienteId);
        if (paciente == null)
            return Falha(nameof(dto.PacienteId), "Paciente não encontrado.");

        List<MedicamentoSaida> medicamentosSaida = new();

        foreach (MedicamentoSaidaDto m in dto.Medicamentos)
        {
            Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(m.MedicamentoId);
            if (medicamento == null)
                return Falha(nameof(m.MedicamentoId), "Medicamento não encontrado.");

            if (m.Quantidade <= 0)
                return Falha(nameof(m.Quantidade), "Quantidade deve ser positiva.");

            int entradas = repositorioEstoque.SelecionarTodos()
                .Where(e => e.TipoOperacao == "Entrada" && e.MedicamentoEntrada?.Id == medicamento.Id)
                .Sum(e => e.QuantidadeEntrada);

            int saidas = repositorioEstoque.SelecionarTodos()
                .Where(e => e.TipoOperacao == "Saída")
                .SelectMany(e => e.MedicamentosSaida)
                .Where(ms => ms.Medicamento?.Id == medicamento.Id)
                .Sum(ms => ms.Quantidade);

            int estoqueDisponivel = entradas - saidas;

            if (m.Quantidade > estoqueDisponivel)
                return Falha(nameof(m.Quantidade), $"Estoque insuficiente para o medicamento {medicamento.Nome}.");

            medicamentosSaida.Add(new MedicamentoSaida(medicamento, m.Quantidade));
        }

        Estoque saida = new(dto.Data, paciente, medicamentosSaida);

        Result resultadoValidacao = ValidarEntidade(saida);
        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioEstoque.Cadastrar(saida);

        return Result.Ok();
    }

    public List<ListarEntradaDto> SelecionarEntradas()
    {
        return repositorioEstoque
            .SelecionarTodos()
            .Where(e => e.TipoOperacao == "Entrada")
            .Select(e => new ListarEntradaDto(
                e.Id,
                e.Data,
                e.MedicamentoEntrada?.Nome ?? "",
                e.FuncionarioEntrada?.Nome ?? "",
                e.QuantidadeEntrada
            ))
            .ToList();
    }

    public List<ListarSaidaDto> SelecionarSaidas()
    {
        return repositorioEstoque
            .SelecionarTodos()
            .Where(e => e.TipoOperacao == "Saída")
            .Select(e => new ListarSaidaDto(
                e.Id,
                e.Data,
                e.PacienteSaida?.Nome ?? "",
                e.MedicamentosSaida.Select(m => m.Medicamento?.Nome ?? "").ToList(),
                e.QuantidadeSaida
            ))
            .ToList();
    }
}