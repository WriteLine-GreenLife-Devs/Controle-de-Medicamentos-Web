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
        return View(new CadastrarEntradaViewModel(DateTime.Now, Guid.Empty, Guid.Empty, 0));
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
        return View(new CadastrarSaidaViewModel(DateTime.Now, Guid.Empty, new List<MedicamentoSaidaViewModel>()));
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