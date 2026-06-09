using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Dominio;
using FluentResults;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Aplicacao;

public class ServicoFuncionario
{
    private readonly IRepositorioFuncionario repositorioFuncionario;

    public ServicoFuncionario(IRepositorioFuncionario repositorioFuncionario)
    {
        this.repositorioFuncionario = repositorioFuncionario;
    }

    private static Result Falha(string campo, string mensagem)
    {
        return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
    }
    private bool VerificarCPFExistente(string cpf)
    {
        return repositorioFuncionario.SelecionarTodos().Any(f => f.CPF == cpf);
    }
    private static Result ValidarEntidade(Funcionario funcionario)
    {
        List<string> erros = funcionario.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        return Result.Fail(new Error(erros.First()).WithMetadata("Campo", string.Empty));
    }

    public Result Cadastrar(CadastrarFuncionarioDto dto)
    {
        if (VerificarCPFExistente(dto.cpf))
            return Falha(nameof(dto.cpf), "Já existe um funcionário cadastrado com este CPF.");

        Funcionario novoFuncionario = new(
            nome: dto.nome,
            telefone: dto.telefone,
            cpf: dto.cpf
        );

        Result resultadoValidacao = ValidarEntidade(novoFuncionario);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioFuncionario.Cadastrar(novoFuncionario);

        return Result.Ok();
    }
    public Result Excluir(Guid id)
    {
        Funcionario? funcionario = repositorioFuncionario.SelecionarPorId(id);

        if (funcionario == null)
            return Result.Fail("Funcionário não encontrado.");

        repositorioFuncionario.Excluir(id);

        return Result.Ok();
    }
    public Result Editar(EditarFuncionarioDto dto)
    {
        Funcionario? funcionario = repositorioFuncionario.SelecionarPorId(dto.Id);

        if (funcionario == null)
            return Result.Fail("Funcionário não encontrado.");

        if (VerificarCPFExistente(dto.cpf))
            return Falha(nameof(dto.cpf), "Já existe um funcionário cadastrado com este CPF.");

        Funcionario funcionarioAtualizado = new Funcionario(
            nome: dto.nome,
            telefone: dto.telefone,
            cpf: dto.cpf
        );

        Result resultadoValidacao = ValidarEntidade(funcionarioAtualizado);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioFuncionario.Editar(dto.Id, funcionarioAtualizado);

        return Result.Ok();
    }
    public List<ListarFuncionariosDto> SelecionarTodos()
    {
        return repositorioFuncionario
            .SelecionarTodos()
            .Select(f => new ListarFuncionariosDto(
                f.Id,
                f.Nome,
                f.Telefone,
                f.CPF
            ))
            .ToList();
    }

     public Result<ListarFuncionariosDto> SelecionarPorId(Guid id)
    {
        Funcionario? funcionario = repositorioFuncionario.SelecionarPorId(id);

        if (funcionario == null)
            return Result.Fail("Funcionário não encontrado.");

        return Result.Ok(new ListarFuncionariosDto(
            funcionario.Id,
            funcionario.Nome,
            funcionario.Telefone,
            funcionario.CPF
        ));
    }
}
