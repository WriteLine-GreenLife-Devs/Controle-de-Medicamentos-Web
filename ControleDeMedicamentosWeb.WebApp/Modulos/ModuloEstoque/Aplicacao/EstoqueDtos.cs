namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Aplicacao;

public record CadastrarEstoqueDto(
    DateTime Data,
    string Medicamento,
    string Funcionario
);

public record EditarEstoqueDto(
    Guid Id,
    DateTime Data,
    string Medicamento,
    string Funcionario
);

public record ListarEstoqueDto(
    Guid id,
    DateTime Data,
    string Medicamento,
    string Funcionario
);