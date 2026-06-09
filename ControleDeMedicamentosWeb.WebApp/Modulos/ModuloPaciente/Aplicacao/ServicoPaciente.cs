using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Dominio;
using FluentResults;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Aplicacao;

public class ServicoPaciente
{
    private readonly IRepositorioPaciente repositorioPaciente;

    public ServicoPaciente(IRepositorioPaciente repositorioPaciente)
    {
        this.repositorioPaciente = repositorioPaciente;
    }

    private static Result Falha(string campo, string mensagem)
    {
        return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
    }
    private bool VerificarCartaoSUSExistente(string cartaoSUS)
    {
        return repositorioPaciente.SelecionarTodos().Any(p => p.CartaoSUS == cartaoSUS);
    }
    private static Result ValidarEntidade(Paciente paciente)
    {
        List<string> erros = paciente.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        return Result.Fail(new Error(erros.First()).WithMetadata("Campo", string.Empty));
    }

    public Result Cadastrar(CadastrarPacienteDto dto)
    {
        if (VerificarCartaoSUSExistente(dto.cartaoSUS))
            return Falha(nameof(dto.cartaoSUS), "Já existe um paciente cadastrado com este Cartão SUS.");

        Paciente novoPaciente = new(
            nome: dto.nome,
            telefone: dto.telefone,
            cartaoSUS: dto.cartaoSUS,
            cpf: dto.cpf
        );

        Result resultadoValidacao = ValidarEntidade(novoPaciente);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioPaciente.Cadastrar(novoPaciente);

        return Result.Ok();
    }
    public Result Excluir(Guid id)
    {
        Paciente? paciente = repositorioPaciente.SelecionarPorId(id);

        if (paciente == null)
            return Result.Fail("Paciente não encontrado.");

        repositorioPaciente.Excluir(id);

        return Result.Ok();
    }
    public Result Editar(EditarPacienteDto dto)
    {
        Paciente? paciente = repositorioPaciente.SelecionarPorId(dto.Id);

        if (paciente == null)
            return Result.Fail("Paciente não encontrado.");

        if (VerificarCartaoSUSExistente(dto.cartaoSUS))
            return Falha(nameof(dto.cartaoSUS), "Já existe um paciente cadastrado com este Cartão SUS.");

        Paciente pacienteAtualizado = new Paciente(
            nome: dto.nome,
            telefone: dto.telefone,
            cartaoSUS: dto.cartaoSUS,
            cpf: dto.cpf
        );

        Result resultadoValidacao = ValidarEntidade(pacienteAtualizado);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioPaciente.Editar(dto.Id, pacienteAtualizado);

        return Result.Ok();
    }
    public List<ListarPacientesDto> SelecionarTodos()
    {
        return repositorioPaciente
            .SelecionarTodos()
            .Select(p => new ListarPacientesDto(
                p.Id,
                p.Nome,
                p.Telefone,
                p.CartaoSUS,
                p.CPF
            ))
            .ToList();
    }

}
