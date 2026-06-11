using AutoMapper;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Aplicacao;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Apresentacao;

public class EstoqueProfile : Profile
{
    public EstoqueProfile()
    {
        CreateMap<CadastrarEntradaViewModel, CadastrarEntradaDto>();
        CreateMap<CadastrarSaidaViewModel, CadastrarSaidaDto>();
        CreateMap<MedicamentoSaidaViewModel, MedicamentoSaidaDto>();

        CreateMap<ListarEntradaDto, ListarEntradaViewModel>();
        CreateMap<ListarSaidaDto, ListarSaidaViewModel>();
    }
}