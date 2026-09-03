using GestaoDeEquipamentos.WebApp.Compartilhado.Infraestrutura.Arquivos;
using GestaoDeEquipamentos.WebApp.Modulos.Chamados.Dominio;

namespace GestaoDeEquipamentos.WebApp.Modulos.Chamados.Infraestrutura;

public sealed class RepositorioChamadoEmArquivo : RepositorioBaseEmArquivo<Chamado>
{
    public RepositorioChamadoEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Chamado> ObterRegistros()
    {
        return contexto.Chamados;
    }
}
