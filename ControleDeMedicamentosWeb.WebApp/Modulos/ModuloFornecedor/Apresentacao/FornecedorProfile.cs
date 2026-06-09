using AutoMapper;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Aplicacao;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Apresentacao;

public class FornecedorProfile : Profile
{
    public FornecedorProfile()
    {
        CreateMap<ListarFornecedoresDto, ListarFornecedoresViewModel>();
        CreateMap<CadastrarFornecedorViewModel, CadastrarFornecedoresDto>();
        CreateMap<EditarFornecedorViewModel, EditarFornecedoresDto>();
        CreateMap<DetalhesFornecedoresDto, EditarFornecedorViewModel>();
        CreateMap<DetalhesFornecedoresDto, ExcluirFornecedorViewModel>();
    }
}
