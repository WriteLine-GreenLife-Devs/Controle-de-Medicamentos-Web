using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Apresentacao;

public record OpcaoMedicamentoViewModel(Guid Id, string Nome);
public record OpcaoFuncionarioViewModel(Guid Id, string Nome);
public record OpcaoPacienteViewModel(Guid Id, string Nome);

public record CadastrarEntradaViewModel(
    [Required] DateTime Data,
    [Required] Guid MedicamentoId,
    [Required] Guid FuncionarioId,
    [Range(1, int.MaxValue, ErrorMessage = "Quantidade deve ser positiva.")]
    int Quantidade,
    [ValidateNever] List<OpcaoMedicamentoViewModel> MedicamentosDisponiveis,
    [ValidateNever] List<OpcaoFuncionarioViewModel> Funcionarios
);

public record CadastrarSaidaViewModel(
    [Required] DateTime Data,
    [Required] Guid PacienteId,
    [Required] List<MedicamentoSaidaViewModel> Medicamentos,
    [ValidateNever] List<OpcaoPacienteViewModel> Pacientes,
    [ValidateNever] List<OpcaoMedicamentoViewModel> MedicamentosDisponiveis
);

public record MedicamentoSaidaViewModel(
    Guid MedicamentoId,
    [Range(1, int.MaxValue, ErrorMessage = "Quantidade deve ser positiva.")]
    int Quantidade
);

public record ListarEntradaViewModel(
    Guid Id,
    DateTime Data,
    string Medicamento,
    string Funcionario,
    int Quantidade
);

public record ListarSaidaViewModel(
    Guid Id,
    DateTime Data,
    string Paciente,
    List<string> Medicamentos,
    int QuantidadeTotal
);

public record ListarEstoqueViewModel(
    List<ListarEntradaViewModel> Entradas,
    List<ListarSaidaViewModel> Saidas
);

public record EditarEstoqueViewModel(
    Guid Id,
    [Required] DateTime Data,
    [Required] string TipoOperacao,
    Guid? MedicamentoId,
    Guid? FuncionarioId,
    Guid? PacienteId,
    int Quantidade,
    List<MedicamentoSaidaViewModel>? Medicamentos
);

public record ExcluirEstoqueViewModel(
    Guid Id,
    DateTime Data,
    string TipoOperacao,
    string? Medicamento,
    string? Funcionario,
    string? Paciente,
    int Quantidade,
    List<string>? Medicamentos
);