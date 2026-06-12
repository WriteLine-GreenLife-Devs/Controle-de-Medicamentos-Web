using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Aplicacao;
using ControleDeMedicamentosWeb.WebApp.Compartilhado.Apresentacao.Extensions;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Apresentacao;

public class EstoqueController(ServicoEstoque servicoEstoque, IMapper mapeador) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        var entradasVm = mapeador.Map<List<ListarEntradaViewModel>>(servicoEstoque.SelecionarEntradas());
        var saidasVm = mapeador.Map<List<ListarSaidaViewModel>>(servicoEstoque.SelecionarSaidas());

        return View(new ListarEstoqueViewModel(entradasVm, saidasVm));
    }

    [HttpGet]
    public ActionResult CadastrarEntrada()
    {
        var vm = new CadastrarEntradaViewModel(
        DateTime.Now,
        Guid.Empty,
        Guid.Empty,
        0,
        servicoEstoque.SelecionarMedicamentos()
            .Select(m => new OpcaoMedicamentoViewModel(m.Id, m.Nome)).ToList(),
        servicoEstoque.SelecionarFuncionarios()
            .Select(f => new OpcaoFuncionarioViewModel(f.Id, f.Nome)).ToList()
        );
        
        return View(vm);
    }

    [HttpPost]
    public ActionResult CadastrarEntrada(CadastrarEntradaViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            vm = new CadastrarEntradaViewModel(
            vm.Data,
            vm.MedicamentoId,
            vm.FuncionarioId,
            vm.Quantidade,
            servicoEstoque.SelecionarMedicamentos()
                .Select(m => new OpcaoMedicamentoViewModel(m.Id, m.Nome)).ToList(),
            servicoEstoque.SelecionarFuncionarios()
                .Select(f => new OpcaoFuncionarioViewModel(f.Id, f.Nome)).ToList()
            );

            return View(vm);
        }

        var dto = mapeador.Map<CadastrarEntradaDto>(vm);
        var resultado = servicoEstoque.CadastrarEntrada(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            vm = new CadastrarEntradaViewModel(
            vm.Data,
            vm.MedicamentoId,
            vm.FuncionarioId,
            vm.Quantidade,
            servicoEstoque.SelecionarMedicamentos()
                .Select(m => new OpcaoMedicamentoViewModel(m.Id, m.Nome)).ToList(),
            servicoEstoque.SelecionarFuncionarios()
                .Select(f => new OpcaoFuncionarioViewModel(f.Id, f.Nome)).ToList()
            );

            return View(vm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult CadastrarSaida()
    {
        var vm = new CadastrarSaidaViewModel(
        DateTime.Now,
        Guid.Empty,
        new List<MedicamentoSaidaViewModel>(),
        servicoEstoque.SelecionarPacientes()
            .Select(p => new OpcaoPacienteViewModel(p.Id, p.Nome)).ToList(),
        servicoEstoque.SelecionarMedicamentos()
            .Select(m => new OpcaoMedicamentoViewModel(m.Id, m.Nome)).ToList()
        );

        return View(vm);
    }

    [HttpPost]
    public ActionResult CadastrarSaida(CadastrarSaidaViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            vm = new CadastrarSaidaViewModel(
            vm.Data,
            vm.PacienteId,
            vm.Medicamentos,
            servicoEstoque.SelecionarPacientes()
                .Select(p => new OpcaoPacienteViewModel(p.Id, p.Nome)).ToList(),
            servicoEstoque.SelecionarMedicamentos()
                .Select(m => new OpcaoMedicamentoViewModel(m.Id, m.Nome)).ToList()
            );

            return View(vm);
        }

        var dto = mapeador.Map<CadastrarSaidaDto>(vm);
        var resultado = servicoEstoque.CadastrarSaida(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            vm = new CadastrarSaidaViewModel(
            vm.Data,
            vm.PacienteId,
            vm.Medicamentos,
            servicoEstoque.SelecionarPacientes()
                .Select(p => new OpcaoPacienteViewModel(p.Id, p.Nome)).ToList(),
            servicoEstoque.SelecionarMedicamentos()
                .Select(m => new OpcaoMedicamentoViewModel(m.Id, m.Nome)).ToList()
            );

            return View(vm);
        }

        return RedirectToAction(nameof(Listar));
    }
}