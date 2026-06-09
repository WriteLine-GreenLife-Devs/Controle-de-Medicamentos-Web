namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Aplicacao;

public record ListarFornecedorsDto(
    Guid Id,
    string Nome,
    string Cor
);

public record CadastrarFornecedorDto(
    string Nome,
    string Cor
);

public record EditarFornecedorDto(
    Guid Id,
    string Nome,
    string Cor
);

public record DetalhesFornecedorDto(
    Guid Id,
    string Nome,
    string Cor
);
