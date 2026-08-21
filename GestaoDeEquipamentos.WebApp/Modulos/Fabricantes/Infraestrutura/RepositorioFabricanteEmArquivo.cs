using GestaoDeEquipamentos.WebApp.Compartilhado.Infraestrutura.Arquivos;

namespace GestaoDeEquipamentos.WebApp.Modulos.Fabricantes.Infraestrutura;

public sealed class RepositorioFabricanteEmArquivo : RepositorioBaseEmArquivo<Fabricante>
{
    public RepositorioFabricanteEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Fabricante> ObterRegistros()
    {
        return contexto.Fabricantes;
    }
}