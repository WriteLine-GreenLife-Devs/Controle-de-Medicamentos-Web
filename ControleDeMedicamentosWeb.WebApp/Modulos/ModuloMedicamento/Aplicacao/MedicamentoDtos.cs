namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Aplicacao;

public record CadastrarMedicamentoDto(
    string nome,
    string descricao,
    int quantidade,
    Guid idFornecedor
);

public record EditarMedicamentoDto(
    Guid Id,
    string nome,
    string descricao,
    int quantidade,
    Guid idFornecedor,
    string fornecedorNome
);

public record ListarMedicamentosDto(
    Guid id,
    string nome,
    string descricao,
    int quantidade,
    Guid idFornecedor,
    string nomeFornecedor
);
