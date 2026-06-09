using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Apresentacao;

public record ListarFornecedoresViewModel(
    Guid Id,
    string Nome,
    string Cor
);

public record CadastrarFornecedorViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(50, ErrorMessage = "O campo \"Nome\" deve conter no máximo 50 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Cor\" deve ser preenchido.")]
    string Cor
);

public record EditarFornecedorViewModel(
    Guid Id,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(50, ErrorMessage = "O campo \"Nome\" deve conter no máximo 50 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Cor\" deve ser preenchido.")]
    string Cor
);

public record ExcluirFornecedorViewModel(
    Guid Id,
    string Nome,
    string Cor
);
