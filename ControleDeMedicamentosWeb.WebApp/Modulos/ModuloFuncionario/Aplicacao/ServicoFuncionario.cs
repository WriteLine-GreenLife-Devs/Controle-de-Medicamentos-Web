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
    private bool VerificarCPFExistenteEditar(string cpf, Guid Id)
    {
        return repositorioFuncionario.SelecionarTodos().Any(f => f.CPF == cpf && f.Id != Id);
    }
    private static Result ValidarEntidade(Funcionario funcionario)
    {
        List<string> erros = funcionario.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        string erro = erros.First();
        string campo = erro.Contains("Nome") ? nameof(funcionario.Nome)
            : erro.Contains("Telefone") ? nameof(funcionario.Telefone)
            : erro.Contains("CPF") ? nameof(funcionario.CPF)
            : string.Empty;

        return Result.Fail(new Error(erro).WithMetadata("Campo", campo));
    }

    public Result Cadastrar(CadastrarFuncionarioDto dto)
    {
        if (VerificarCPFExistente(dto.cpf))
            return Falha(nameof(CadastrarFuncionarioDto.cpf), "Já existe um funcionário cadastrado com este CPF.");

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

        if (VerificarCPFExistenteEditar(dto.cpf, dto.Id))
            return Falha(nameof(CadastrarFuncionarioDto.cpf), "Já existe um funcionário cadastrado com este CPF.");

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
