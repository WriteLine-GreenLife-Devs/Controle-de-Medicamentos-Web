using AutoMapper;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Aplicacao;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Apresentacao;

public class FornecedorProfile : Profile
{
    public FornecedorProfile()
    {
        CreateMap<ListarFornecedorsDto, ListarFornecedorsViewModel>();
        CreateMap<CadastrarFornecedorViewModel, CadastrarFornecedorDto>();
        CreateMap<EditarFornecedorViewModel, EditarFornecedorDto>();
        CreateMap<DetalhesFornecedorDto, EditarFornecedorViewModel>();
        CreateMap<DetalhesFornecedorDto, ExcluirFornecedorViewModel>();
    }
}
