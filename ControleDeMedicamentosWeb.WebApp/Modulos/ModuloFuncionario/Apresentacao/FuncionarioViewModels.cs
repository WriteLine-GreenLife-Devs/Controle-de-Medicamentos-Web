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
string Telefone,

    [Required(ErrorMessage = "O campo \"CPF\" deve ser preenchido.")]
string Cpf
);

public record EditarFuncionarioViewModel(
    Guid Id,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
[StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
string Nome,

    [Required(ErrorMessage = "O campo \"Telefone\" deve ser preenchido.")]
string Telefone,

    [Required(ErrorMessage = "O campo \"CPF\" deve ser preenchido.")]
string Cpf
);

public record ExcluirFuncionarioViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string Cpf
);
