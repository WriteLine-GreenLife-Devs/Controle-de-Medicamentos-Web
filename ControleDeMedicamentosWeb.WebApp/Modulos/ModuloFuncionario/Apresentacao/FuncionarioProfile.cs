using AutoMapper;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Aplicacao;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Apresentacao;

public class FuncionarioProfile : Profile
{
    public FuncionarioProfile()
    {
        CreateMap<CadastrarFuncionarioViewModel, CadastrarFuncionarioDto>();
        CreateMap<EditarFuncionarioViewModel, EditarFuncionarioDto>();
        CreateMap<CadastrarFuncionarioDto, Funcionario>();
        CreateMap<EditarFuncionarioDto, Funcionario>();
        CreateMap<Funcionario, ListarFuncionariosDto>();
        CreateMap<ListarFuncionariosDto, ListarFuncionariosViewModel>();
        CreateMap<ListarFuncionariosDto, EditarFuncionarioViewModel>();
        CreateMap<ListarFuncionariosDto, ExcluirFuncionarioViewModel>();
    }
}
