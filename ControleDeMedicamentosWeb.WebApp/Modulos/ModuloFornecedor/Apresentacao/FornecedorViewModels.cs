using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Apresentacao;

public record ListarFornecedoresViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string CNPJ
);

public record CadastrarFornecedorViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, ErrorMessage = "O campo \"Nome\" deve conter no máximo 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Telefone\" deve ser preenchido.")]
    [StringLength(11, MinimumLength = 8, ErrorMessage = "O campo \"Telefone\" deve conter entre 8 e 11 dígitos.")]
    [RegularExpression(@"^\d{8,11}$", ErrorMessage = "O campo \"Telefone\" deve conter apenas números.")]
    string Telefone,

    [Required(ErrorMessage = "O campo \"CNPJ\" deve ser preenchido.")]
    [StringLength(14, ErrorMessage = "O campo \"CNPJ\" deve conter no máximo 14 caracteres.")]
    string CNPJ
);

public record EditarFornecedorViewModel(
    Guid Id,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(50, ErrorMessage = "O campo \"Nome\" deve conter no máximo 50 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Telefone\" deve ser preenchido.")]
    [StringLength(11, ErrorMessage = "O campo \"Telefone\" deve conter no máximo 11 caracteres.")]
    string Telefone,

    [Required(ErrorMessage = "O campo \"CNPJ\" deve ser preenchido.")]
    [StringLength(14, ErrorMessage = "O campo \"CNPJ\" deve conter no máximo 14 caracteres.")]
    string CNPJ
);

public record ExcluirFornecedorViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string CNPJ
);