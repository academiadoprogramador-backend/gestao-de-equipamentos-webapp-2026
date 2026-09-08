using GestaoDeEquipamentos.WebApp.Compartilhado.Infraestrutura.Arquivos;
using GestaoDeEquipamentos.WebApp.Modulos.Fabricantes.Dominio;

namespace GestaoDeEquipamentos.WebApp.Modulos.Fabricantes.Infraestrutura;

public sealed class RepositorioFabricanteEmArquivo :
    RepositorioBaseEmArquivo<Fabricante>, IRepositorioFabricante
{
    public RepositorioFabricanteEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Fabricante> ObterRegistros()
    {
        return contexto.Fabricantes;
    }
}