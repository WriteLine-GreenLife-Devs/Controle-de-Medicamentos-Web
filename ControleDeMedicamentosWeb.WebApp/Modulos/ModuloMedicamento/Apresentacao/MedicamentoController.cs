using AutoMapper;
using ControleDeMedicamentosWeb.WebApp.Compartilhado.Apresentacao.Extensions;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Dominio;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Aplicacao;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Apresentacao;

public class MedicamentoController : Controller
{
    private readonly ServicoMedicamento servicoMedicamento;
    private readonly IRepositorioFornecedor repositorioFornecedor;
    private readonly IMapper mapeador;

    public MedicamentoController(ServicoMedicamento servicoMedicamento, IRepositorioFornecedor repositorioFornecedor, IMapper mapeador)
    {
        this.servicoMedicamento = servicoMedicamento;
        this.repositorioFornecedor = repositorioFornecedor;
        this.mapeador = mapeador;
    }

    private static List<OpcaoFornecedorViewModel> ObterFornecedoresDisponiveis(IRepositorioFornecedor repositorioFornecedor)
    {
        return repositorioFornecedor
            .SelecionarTodos()
            .Select(f => new OpcaoFornecedorViewModel(f.Id, f.Nome, f.Telefone, f.CNPJ))
            .ToList();
    }

    private CadastrarMedicamentoViewModel CriarCadastrarVm()
    {
        return new CadastrarMedicamentoViewModel(
            string.Empty,
            string.Empty,
            0,
            Guid.Empty,
            ObterFornecedoresDisponiveis(repositorioFornecedor)
        );
    }

    private EditarMedicamentoViewModel CriarEditarVm(ListarMedicamentosDto dto)
    {
        return new EditarMedicamentoViewModel(
            dto.id,
            dto.nome,
            dto.descricao,
            dto.quantidade,
            dto.idFornecedor,
            ObterFornecedoresDisponiveis(repositorioFornecedor)
        );
    }

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
        CadastrarMedicamentoViewModel cadastrarVm = CriarCadastrarVm();

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarMedicamentoViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm with { Fornecedores = ObterFornecedoresDisponiveis(repositorioFornecedor) });

        CadastrarMedicamentoDto dto = mapeador.Map<CadastrarMedicamentoDto>(cadastrarVm);

        Result resultado = servicoMedicamento.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(cadastrarVm with { Fornecedores = ObterFornecedoresDisponiveis(repositorioFornecedor) });
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

        EditarMedicamentoViewModel editarVm = CriarEditarVm(resultado.Value);

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarMedicamentoViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm with { Fornecedores = ObterFornecedoresDisponiveis(repositorioFornecedor) });

        EditarMedicamentoDto dto = mapeador.Map<EditarMedicamentoDto>(editarVm);

        Result resultado = servicoMedicamento.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(editarVm with { Fornecedores = ObterFornecedoresDisponiveis(repositorioFornecedor) });
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
