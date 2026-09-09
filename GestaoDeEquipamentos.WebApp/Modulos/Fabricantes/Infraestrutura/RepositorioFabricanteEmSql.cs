using Dapper;
using Microsoft.Data.SqlClient;
using GestaoDeEquipamentos.WebApp.Modulos.Fabricantes.Dominio;

namespace GestaoDeEquipamentos.WebApp.Modulos.Fabricantes.Infraestrutura;

public sealed class RepositorioFabricanteEmSql : IRepositorioFabricante
{
    private readonly string connectionString;

    public RepositorioFabricanteEmSql(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public void Cadastrar(Fabricante novoRegistro)
    {
        const string query =
            """
            INSERT INTO dbo.TBFabricantes (Nome, Email, Telefone)
            OUTPUT INSERTED.Id
            VALUES (@Nome, @Email, @Telefone)
            """;

        using SqlConnection conexao = new(connectionString);

        novoRegistro.Id = conexao.QuerySingle<int>(query, novoRegistro);
    }

    public bool Editar(int idSelecionado, Fabricante entidadeAtualizada)
    {
        const string query =
            """
            UPDATE dbo.TBFabricantes
            SET Nome = @Nome,
                Email = @Email,
                Telefone = @Telefone
            WHERE Id = @Id
            """;

        using SqlConnection conexao = new(connectionString);

        int quantidadeRegistrosAlterados = conexao.Execute(query, new
        {
            Id = idSelecionado,
            entidadeAtualizada.Nome,
            entidadeAtualizada.Email,
            entidadeAtualizada.Telefone
        });

        return quantidadeRegistrosAlterados == 1;
    }

    public bool Excluir(int idSelecionado)
    {
        const string query = "DELETE FROM dbo.TBFabricantes WHERE Id = @Id";

        using SqlConnection conexao = new(connectionString);

        int quantidadeRegistrosExcluidos = conexao.Execute(query, new { Id = idSelecionado });

        return quantidadeRegistrosExcluidos == 1;
    }

    public Fabricante? SelecionarPorId(int idSelecionado)
    {
        const string query =
            """
            SELECT Id, Nome, Email, Telefone
            FROM dbo.TBFabricantes
            WHERE Id = @Id
            """;

        using SqlConnection conexao = new(connectionString);

        // Criação de um objeto anônimo
        return conexao.QuerySingleOrDefault<Fabricante>(query, new { Id = idSelecionado });
    }

    public List<Fabricante> SelecionarTodos()
    {
        const string query =
            """
            SELECT Id, Nome, Email, Telefone
            FROM dbo.TBFabricantes
            ORDER BY Id
            """;

        using SqlConnection conexao = new(connectionString);

        // Query = Consulta no banco (SQL = Structured Query Language)
        return conexao.Query<Fabricante>(query).ToList();
    }
}