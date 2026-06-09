namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Aplicacao;

public record CadastrarFuncionarioDto(
    string nome,
    string telefone,
    string cpf
);

public record EditarFuncionarioDto(
    Guid Id,
    string nome,
    string telefone,
    string cpf
);

public record ListarFuncionariosDto(
    Guid id,
    string nome,
    string telefone,
    string cpf
);
