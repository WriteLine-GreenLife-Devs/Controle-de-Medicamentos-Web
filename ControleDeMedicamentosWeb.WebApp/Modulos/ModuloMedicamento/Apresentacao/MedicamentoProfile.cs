using AutoMapper;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Aplicacao;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Apresentacao;

public class MedicamentoProfile : Profile
{
    public MedicamentoProfile()
    {
        CreateMap<CadastrarMedicamentoDto, Medicamento>();
        CreateMap<EditarMedicamentoDto, Medicamento>();
        CreateMap<Medicamento, ListarMedicamentosDto>();
    }
}
