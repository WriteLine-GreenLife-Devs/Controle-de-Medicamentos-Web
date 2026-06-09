using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Apresentacao;

public record ListarPacientesViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string CartaoSUS,
    string Cpf
);

public record CadastrarPacienteViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
[StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
string Nome,

    [Required(ErrorMessage = "O campo \"Telefone\" deve ser preenchido.")]
string Telefone,

    [Required(ErrorMessage = "O campo \"Cartão SUS\" deve ser preenchido.")]
[StringLength(15, MinimumLength = 15, ErrorMessage = "O campo \"Cartão SUS\" deve conter 15 caracteres.")]
string CartaoSUS,

    [Required(ErrorMessage = "O campo \"CPF\" deve ser preenchido.")]
string Cpf
);

public record EditarPacienteViewModel(
    Guid Id,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
[StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
string Nome,

    [Required(ErrorMessage = "O campo \"Telefone\" deve ser preenchido.")]
string Telefone,

    [Required(ErrorMessage = "O campo \"Cartão SUS\" deve ser preenchido.")]
[StringLength(15, MinimumLength = 15, ErrorMessage = "O campo \"Cartão SUS\" deve conter 15 caracteres.")]
string CartaoSUS,

    [Required(ErrorMessage = "O campo \"CPF\" deve ser preenchido.")]
string Cpf
);

public record ExcluirPacienteViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string CartaoSUS,
    string Cpf
);
