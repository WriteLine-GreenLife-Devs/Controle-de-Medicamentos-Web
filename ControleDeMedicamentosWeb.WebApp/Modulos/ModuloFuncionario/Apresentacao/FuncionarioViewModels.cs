using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Apresentacao;

public record ListarFuncionariosViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string Cpf
);

public record CadastrarFuncionarioViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
[StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
string Nome,

    [Required(ErrorMessage = "O campo \"Telefone\" deve ser preenchido.")]
    [StringLength(16, MinimumLength = 10, ErrorMessage = "O campo \"Telefone\" deve conter entre 10 e 16 caracteres.")]
string Telefone,

    [Required(ErrorMessage = "O campo \"CPF\" deve ser preenchido.")]
    [StringLength(14, MinimumLength = 11, ErrorMessage = "O campo \"CPF\" deve conter entre 11 e 14 caracteres.")]
string Cpf
);

public record EditarFuncionarioViewModel(
    Guid Id,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
[StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
string Nome,

    [Required(ErrorMessage = "O campo \"Telefone\" deve ser preenchido.")]
    [StringLength(16, MinimumLength = 10, ErrorMessage = "O campo \"Telefone\" deve conter entre 10 e 16 caracteres.")]
string Telefone,

    [Required(ErrorMessage = "O campo \"CPF\" deve ser preenchido.")]
    [StringLength(14, MinimumLength = 11, ErrorMessage = "O campo \"CPF\" deve conter entre 11 e 14 caracteres.")]
string Cpf
);

public record ExcluirFuncionarioViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string Cpf
);
