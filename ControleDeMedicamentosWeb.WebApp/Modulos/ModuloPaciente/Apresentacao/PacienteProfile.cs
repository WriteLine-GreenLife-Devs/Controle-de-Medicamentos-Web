using AutoMapper;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Aplicacao;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Apresentacao;

public class PacienteProfile : Profile
{
    public PacienteProfile()
    {
        CreateMap<CadastrarPacienteDto, Paciente>();
        CreateMap<EditarPacienteDto, Paciente>();
        CreateMap<Paciente, ListarPacientesDto>();
    }
}
