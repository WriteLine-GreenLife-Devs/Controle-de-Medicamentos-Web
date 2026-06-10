using AutoMapper;
using ControleDeMedicamentosWeb.WebApp.Compartilhado.Apresentacao.Extensions;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Aplicacao;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Apresentacao;

public class MedicamentoController(ServicoMedicamento servicoMedicamento, IMapper mapeador) : Controller
{

    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarMedicamentosDto> dtos = servicoMedicamento.SelecionarTodos();
        List<ListarMedicamentosViewModel> listarVms = mapeador.Map<List<ListarMedicamentosViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarMedicamentoViewModel cadastrarVm = new CadastrarMedicamentoViewModel(
            string.Empty,
            string.Empty,
            0,
            new List<OpcaoFornecedorViewModel>()
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarMedicamentoViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        CadastrarMedicamentoDto dto = mapeador.Map<CadastrarMedicamentoDto>(cadastrarVm);

        Result resultado = servicoMedicamento.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(cadastrarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<ListarMedicamentosDto> resultado = servicoMedicamento.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        EditarMedicamentoViewModel editarVm =
            mapeador.Map<EditarMedicamentoViewModel>(resultado.Value);

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarMedicamentoViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        EditarMedicamentoDto dto = mapeador.Map<EditarMedicamentoDto>(editarVm);

        Result resultado = servicoMedicamento.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(editarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<ListarMedicamentosDto> resultado = servicoMedicamento.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        ExcluirMedicamentoViewModel excluirVm = mapeador.Map<ExcluirMedicamentoViewModel>(resultado.Value);

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirMedicamentoViewModel excluirVm)
    {
        Result resultado = servicoMedicamento.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(nameof(Listar));
    }

}
