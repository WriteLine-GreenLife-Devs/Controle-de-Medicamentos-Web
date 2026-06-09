namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Aplicacao;

public record CadastrarPacienteDto(
    string nome,
    string telefone,
    string cartaoSUS,
    string cpf
);

public record EditarPacienteDto(
    Guid Id,
    string nome,
    string telefone,
    string cartaoSUS,
    string cpf
);

public record ListarPacientesDto(
    Guid id,
    string nome,
    string telefone,
    string cartaoSUS,
    string cpf
);
