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
        List<ListarEntradaDto> entradas = servicoEstoque.SelecionarEntradas();
        List<ListarSaidaDto> saidas = servicoEstoque.SelecionarSaidas();

        List<ListarEntradaViewModel> entradasVm = mapeador.Map<List<ListarEntradaViewModel>>(entradas);
        List<ListarSaidaViewModel> saidasVm = mapeador.Map<List<ListarSaidaViewModel>>(saidas);

        ListarEstoqueViewModel listarVm = new ListarEstoqueViewModel(entradasVm, saidasVm);

        return View(listarVm);
    }

    [HttpGet]
    public ActionResult CadastrarEntrada()
    {
        CadastrarEntradaViewModel vm = new(
            DateTime.Now,
            Guid.Empty,
            Guid.Empty,
            0,
            servicoEstoque.SelecionarMedicamentos().Select(m => new OpcaoMedicamentoViewModel(m.Id, m.Nome)).ToList(),
            servicoEstoque.SelecionarFuncionarios().Select(f => new OpcaoFuncionarioViewModel(f.Id, f.Nome)).ToList()
        );

        return View(vm);
    }

    [HttpPost]
    public ActionResult CadastrarEntrada(CadastrarEntradaViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        CadastrarEntradaDto dto = mapeador.Map<CadastrarEntradaDto>(vm);
        Result resultado = servicoEstoque.CadastrarEntrada(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            return View(vm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult CadastrarSaida()
    {
        CadastrarSaidaViewModel vm = new(
            DateTime.Now,
            Guid.Empty,
            new List<MedicamentoSaidaViewModel>(),
            servicoEstoque.SelecionarPacientes().Select(p => new OpcaoPacienteViewModel(p.Id, p.Nome)).ToList(),
            servicoEstoque.SelecionarMedicamentos().Select(m => new OpcaoMedicamentoViewModel(m.Id, m.Nome)).ToList()
        );

        return View(vm);
    }

    [HttpPost]
    public ActionResult CadastrarSaida(CadastrarSaidaViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        CadastrarSaidaDto dto = mapeador.Map<CadastrarSaidaDto>(vm);
        Result resultado = servicoEstoque.CadastrarSaida(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            return View(vm);
        }

        return RedirectToAction(nameof(Listar));
    }
}