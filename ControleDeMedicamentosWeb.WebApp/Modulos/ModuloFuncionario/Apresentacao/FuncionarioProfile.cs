using AutoMapper;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Aplicacao;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Apresentacao;

public class FuncionarioProfile : Profile
{
    public FuncionarioProfile()
    {
        CreateMap<CadastrarFuncionarioDto, Funcionario>();
        CreateMap<EditarFuncionarioDto, Funcionario>();
        CreateMap<Funcionario, ListarFuncionariosDto>();
    }
}
