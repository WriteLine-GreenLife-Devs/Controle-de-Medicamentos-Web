namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Aplicacao;

public record CadastrarEntradaDto(
    DateTime Data,
    Guid MedicamentoId,
    Guid FuncionarioId,
    int Quantidade
);

public record CadastrarSaidaDto(
    DateTime Data,
    Guid PacienteId,
    List<MedicamentoSaidaDto> Medicamentos
);

public record MedicamentoSaidaDto(
    Guid MedicamentoId,
    int Quantidade
);

public record ListarEntradaDto(
    Guid Id,
    DateTime Data,
    string Medicamento,
    string Funcionario,
    int Quantidade
);

public record ListarSaidaDto(
    Guid Id,
    DateTime Data,
    string Paciente,
    List<string> Medicamentos,
    int QuantidadeTotal
);

public record DetalhesEstoqueDto(
    Guid Id,
    DateTime Data,
    string TipoOperacao,
    string? Medicamento,
    string? Funcionario,
    string? Paciente,
    int Quantidade
);