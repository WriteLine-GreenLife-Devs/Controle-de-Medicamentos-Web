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
    [StringLength(16, MinimumLength = 10, ErrorMessage = "O campo \"Telefone\" deve conter entre 10 e 16 caracteres.")]
string Telefone,

    [Required(ErrorMessage = "O campo \"Cartão SUS\" deve ser preenchido.")]
[StringLength(15, MinimumLength = 15, ErrorMessage = "O campo \"Cartão SUS\" deve conter 15 caracteres.")]
string CartaoSUS,

    [Required(ErrorMessage = "O campo \"CPF\" deve ser preenchido.")]
    [StringLength(14, MinimumLength = 11, ErrorMessage = "O campo \"CPF\" deve conter entre 11 e 14 caracteres.")]
string Cpf
);

public record EditarPacienteViewModel(
    Guid Id,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
[StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
string Nome,

    [Required(ErrorMessage = "O campo \"Telefone\" deve ser preenchido.")]
    [StringLength(16, MinimumLength = 10, ErrorMessage = "O campo \"Telefone\" deve conter entre 10 e 16 caracteres.")]
string Telefone,

    [Required(ErrorMessage = "O campo \"Cartão SUS\" deve ser preenchido.")]
[StringLength(15, MinimumLength = 15, ErrorMessage = "O campo \"Cartão SUS\" deve conter 15 caracteres.")]
string CartaoSUS,

    [Required(ErrorMessage = "O campo \"CPF\" deve ser preenchido.")]
    [StringLength(14, MinimumLength = 11, ErrorMessage = "O campo \"CPF\" deve conter entre 11 e 14 caracteres.")]
string Cpf
);

public record ExcluirPacienteViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string CartaoSUS,
    string Cpf
);
