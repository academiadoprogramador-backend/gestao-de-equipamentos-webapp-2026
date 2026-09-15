using Dapper;
using GestaoDeEquipamentos.WebApp.Modulos.Chamados.Dominio;
using GestaoDeEquipamentos.WebApp.Modulos.Equipamentos.Dominio;
using Microsoft.Data.SqlClient;

namespace GestaoDeEquipamentos.WebApp.Modulos.Chamados.Infraestrutura;

public sealed class RepositorioChamadoEmSql : IRepositorioChamado
{
    private readonly string connectionString;

    public RepositorioChamadoEmSql(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public void Cadastrar(Chamado novoRegistro)
    {
        const string query =
            """
            INSERT INTO dbo.TBChamados (Titulo, Descricao, EquipamentoId, DataAbertura)
            OUTPUT INSERTED.Id
            VALUES (@Titulo, @Descricao, @EquipamentoId, @DataAbertura)
            """;

        using SqlConnection conexao = new(connectionString);

        novoRegistro.Id = conexao.QuerySingle<int>(query, new
        {
            novoRegistro.Titulo,
            novoRegistro.Descricao,
            EquipamentoId = novoRegistro.Equipamento.Id,
            novoRegistro.DataAbertura
        });
    }

    public bool Editar(int idSelecionado, Chamado entidadeAtualizada)
    {
        const string query =
            """
            UPDATE dbo.TBChamados
            SET
                Titulo = @Titulo,
                Descricao = @Descricao,
                EquipamentoId = @EquipamentoId,
                DataAbertura = @DataAbertura
            WHERE Id = @Id
            """;

        using SqlConnection conexao = new(connectionString);

        int quantidadeRegistrosAlterados = conexao.Execute(query, new
        {
            Id = idSelecionado,
            entidadeAtualizada.Titulo,
            entidadeAtualizada.Descricao,
            EquipamentoId = entidadeAtualizada.Equipamento.Id,
            entidadeAtualizada.DataAbertura
        });

        return quantidadeRegistrosAlterados == 1;
    }

    public bool Excluir(int idSelecionado)
    {
        const string query = "DELETE FROM dbo.TBChamados WHERE Id = @Id";

        using SqlConnection conexao = new(connectionString);

        int quantidadeRegistrosExcluidos = conexao.Execute(query, new { Id = idSelecionado });

        return quantidadeRegistrosExcluidos == 1;
    }

    public Chamado? SelecionarPorId(int idSelecionado)
    {
        const string query =
            """
            SELECT
                c.Id,
                c.Titulo,
                c.Descricao,
                c.DataAbertura,
                e.Id,
                e.Nome,
                e.PrecoAquisicao,
                e.DataFabricacao,
                f.Id,
                f.Nome,
                f.Email,
                f.Telefone
            FROM dbo.TBChamados c
            INNER JOIN dbo.TBEquipamentos e ON e.Id = c.EquipamentoId
            INNER JOIN dbo.TBFabricantes f ON f.Id = e.FabricanteId
            WHERE c.Id = @Id
            """;

        using SqlConnection conexao = new(connectionString);

        return conexao.Query<Chamado, Equipamento, Fabricante, Chamado>(
            query,
            MapearChamadoCompleto,
            new { Id = idSelecionado },
            splitOn: "Id,Id"
        ).SingleOrDefault();
    }

    public List<Chamado> SelecionarTodos()
    {
        const string query =
            """
            SELECT
                c.Id,
                c.Titulo,
                c.Descricao,
                c.DataAbertura,
                e.Id,
                e.Nome,
                e.PrecoAquisicao,
                e.DataFabricacao,
                f.Id,
                f.Nome,
                f.Email,
                f.Telefone
            FROM dbo.TBChamados c
            INNER JOIN dbo.TBEquipamentos e ON e.Id = c.EquipamentoId
            INNER JOIN dbo.TBFabricantes f ON f.Id = e.FabricanteId
            ORDER BY c.Id
            """;

        using SqlConnection conexao = new(connectionString);

        return conexao.Query<Chamado, Equipamento, Fabricante, Chamado>(
            query,
            MapearChamadoCompleto,
            splitOn: "Id,Id"
        ).ToList();
    }

    public bool ExisteParaEquipamento(int equipamentoId)
    {
        const string query =
            """
            SELECT CAST(CASE WHEN EXISTS (
                SELECT 1
                FROM dbo.TBChamados
                WHERE EquipamentoId = @EquipamentoId
            ) THEN 1 ELSE 0 END AS BIT)
            """;

        using SqlConnection conexao = new(connectionString);

        return conexao.QuerySingle<bool>(query, new { EquipamentoId = equipamentoId });
    }

    private static Chamado MapearChamadoCompleto(
        Chamado chamado,
        Equipamento equipamento,
        Fabricante fabricante
    )
    {
        equipamento.Fabricante = fabricante;
        chamado.Equipamento = equipamento;

        return chamado;
    }
}
