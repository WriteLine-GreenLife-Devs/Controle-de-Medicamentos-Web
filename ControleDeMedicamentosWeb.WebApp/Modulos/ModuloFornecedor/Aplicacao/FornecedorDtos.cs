namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Aplicacao;

public record ListarFornecedoresDto(
    Guid Id,
    string Nome,
    string Telefone,
    string CNPJ
);

public record CadastrarFornecedorDto(
    string Nome,
    string Telefone,
    string CNPJ
);

public record EditarFornecedorDto(
    Guid Id,
    string Nome,
    string Telefone,
    string CNPJ
);

public record DetalhesFornecedoresDto(
    Guid Id,
    string Nome,
    string Telefone,
    string CNPJ
);
