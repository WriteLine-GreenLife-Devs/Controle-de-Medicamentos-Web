using ControleDeMedicamentosWeb.WebApp.Compartilhado.Dominio;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Dominio;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Dominio;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Dominio;

public sealed class Estoque : EntidadeBase<Estoque>
{
    public DateTime Data { get; set; } = DateTime.Now;
    public Medicamento? MedicamentoEntrada { get; set; }
    public Funcionario? FuncionarioEntrada { get; set; }
    public int QuantidadeEntrada { get; set; }
    public string TipoOperacao { get; set; } = string.Empty;
    public Paciente? PacienteSaida { get; set; }
    public List<MedicamentoSaida> MedicamentosSaida { get; set; } = new();
    public int QuantidadeSaida { get; set; }

    public Estoque() { }

    public Estoque(DateTime data, Medicamento medicamento, Funcionario funcionario, int quantidade)
    {
        Data = data;
        MedicamentoEntrada = medicamento;
        FuncionarioEntrada = funcionario;
        QuantidadeEntrada = quantidade;
        TipoOperacao = "Entrada";
    }

    public Estoque(DateTime data, Paciente paciente, List<MedicamentoSaida> medicamentos)
    {
        Data = data;
        PacienteSaida = paciente;
        MedicamentosSaida = medicamentos;
        TipoOperacao = "Saída";
        QuantidadeSaida = medicamentos.Sum(m => m.Quantidade);
    }

    public override void Atualizar(Estoque entidadeAtualizada)
    {
        Data = entidadeAtualizada.Data;
        TipoOperacao = entidadeAtualizada.TipoOperacao;
        MedicamentoEntrada = entidadeAtualizada.MedicamentoEntrada;
        FuncionarioEntrada = entidadeAtualizada.FuncionarioEntrada;
        QuantidadeEntrada = entidadeAtualizada.QuantidadeEntrada;
        PacienteSaida = entidadeAtualizada.PacienteSaida;
        MedicamentosSaida = entidadeAtualizada.MedicamentosSaida;
        QuantidadeSaida = entidadeAtualizada.QuantidadeSaida;
    }

    public override List<string> Validar()
    {
        List<string> erros = new();

        if (TipoOperacao == "Entrada")
        {
            if (MedicamentoEntrada == null)
                erros.Add("O campo \"Medicamento\" deve ser informado.");

            if (FuncionarioEntrada == null)
                erros.Add("O campo \"Funcionário\" deve ser informado.");

            if (QuantidadeEntrada <= 0)
                erros.Add("O campo \"Quantidade\" deve ser um número positivo.");
        }

        if (TipoOperacao == "Saída")
        {
            if (PacienteSaida == null)
                erros.Add("O campo \"Paciente\" deve ser informado.");

            if (MedicamentosSaida == null || MedicamentosSaida.Count == 0)
                erros.Add("É necessário informar ao menos um medicamento.");

            if (QuantidadeSaida <= 0)
                erros.Add("O campo \"Quantidade\" deve ser um número positivo.");
        }

        return erros;
    }
}

public sealed class MedicamentoSaida
{
    public Medicamento? Medicamento { get; set; }
    public int Quantidade { get; set; }

    public MedicamentoSaida() { }

    public MedicamentoSaida(Medicamento medicamento, int quantidade)
    {
        Medicamento = medicamento;
        Quantidade = quantidade;
    }
}