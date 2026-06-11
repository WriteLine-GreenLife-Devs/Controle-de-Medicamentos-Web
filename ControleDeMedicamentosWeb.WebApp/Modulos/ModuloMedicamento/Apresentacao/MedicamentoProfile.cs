using AutoMapper;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Aplicacao;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Apresentacao;

public class MedicamentoProfile : Profile
{
    public MedicamentoProfile()
    {
        CreateMap<CadastrarMedicamentoViewModel, CadastrarMedicamentoDto>()
            .ForCtorParam("nome", opt => opt.MapFrom(src => src.Nome))
            .ForCtorParam("descricao", opt => opt.MapFrom(src => src.Descricao))
            .ForCtorParam("quantidade", opt => opt.MapFrom(src => src.quantidade))
            .ForCtorParam("idFornecedor", opt => opt.MapFrom(src => src.FornecedorId));

        CreateMap<EditarMedicamentoViewModel, EditarMedicamentoDto>()
            .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id))
            .ForCtorParam("nome", opt => opt.MapFrom(src => src.Nome))
            .ForCtorParam("descricao", opt => opt.MapFrom(src => src.Descricao))
            .ForCtorParam("quantidade", opt => opt.MapFrom(src => src.quantidade))
            .ForCtorParam("idFornecedor", opt => opt.MapFrom(src => src.FornecedorId))
            .ForCtorParam("fornecedorNome", opt => opt.MapFrom(src => string.Empty));

        CreateMap<CadastrarMedicamentoDto, Medicamento>();
        CreateMap<EditarMedicamentoDto, Medicamento>();
        CreateMap<Medicamento, ListarMedicamentosDto>();
        CreateMap<ListarMedicamentosDto, ListarMedicamentosViewModel>()
            .ForCtorParam("Id", opt => opt.MapFrom(src => src.id))
            .ForCtorParam("nome", opt => opt.MapFrom(src => src.nome))
            .ForCtorParam("descricao", opt => opt.MapFrom(src => src.descricao))
            .ForCtorParam("quantidade", opt => opt.MapFrom(src => src.quantidade))
            .ForCtorParam("fornecedorNome", opt => opt.MapFrom(src => src.nomeFornecedor));
        CreateMap<ListarMedicamentosDto, ExcluirMedicamentoViewModel>()
            .ForCtorParam("Id", opt => opt.MapFrom(src => src.id))
            .ForCtorParam("nome", opt => opt.MapFrom(src => src.nome))
            .ForCtorParam("descricao", opt => opt.MapFrom(src => src.descricao))
            .ForCtorParam("quantidade", opt => opt.MapFrom(src => src.quantidade))
            .ForCtorParam("fornecedorNome", opt => opt.MapFrom(src => src.nomeFornecedor));
    }
}
