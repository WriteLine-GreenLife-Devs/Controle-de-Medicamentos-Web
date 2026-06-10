using System.Dynamic;
using System.Linq;
using ControleDeMedicamentosWeb.WebApp.Compartilhado.Dominio;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Dominio;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Dominio;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque;

public sealed class Estoque : EntidadeBase<Estoque>
{
    public DateTime Data { get; set; } = DateTime.Now;
    public Medicamento? MedicamentoEntrada { get; set; } = null;
    public Funcionario? FuncionarioEntrada { get; set; } = null;
    public int QuantidadeEntrada { get; set; } = 0;
    public string TipoOperacao { get; set; } = "";
    public Paciente? PacienteSaida { get; set; } = null;
    public List<MedicamentoSaida> MedicamentosSaida { get; set; } = [];
    public int QuantidadeSaida { get; set; } = 0;
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
        List<string> erros = new List<string>();

        //mensagens das validações

        return erros;
    }
}

public class MedicamentoSaida
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