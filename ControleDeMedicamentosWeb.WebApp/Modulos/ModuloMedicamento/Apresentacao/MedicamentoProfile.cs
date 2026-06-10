using AutoMapper;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Aplicacao;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Apresentacao;

public class MedicamentoProfile : Profile
{
    public MedicamentoProfile()
    {
        CreateMap<CadastrarMedicamentoViewModel, CadastrarMedicamentoDto>();
        CreateMap<EditarMedicamentoViewModel, EditarMedicamentoDto>();
        CreateMap<CadastrarMedicamentoDto, Medicamento>();
        CreateMap<EditarMedicamentoDto, Medicamento>();
        CreateMap<Medicamento, ListarMedicamentosDto>();
        CreateMap<ListarMedicamentosDto, ListarMedicamentosViewModel>();
        CreateMap<ListarMedicamentosDto, EditarMedicamentoViewModel>();
        CreateMap<ListarMedicamentosDto, ExcluirMedicamentoViewModel>();
    }
}
