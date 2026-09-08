using GestaoDeEquipamentos.WebApp.Modulos.Fabricantes.Dominio;

namespace GestaoDeEquipamentos.WebApp.Modulos.Fabricantes.Infraestrutura;

public sealed class RepositorioFabricanteEmSql : IRepositorioFabricante
{
    public void Cadastrar(Fabricante novoRegistro)
    {
        throw new NotImplementedException();
    }

    public bool Editar(int idSelecionado, Fabricante entidadeAtualizada)
    {
        throw new NotImplementedException();
    }

    public bool Excluir(int idSelecionado)
    {
        throw new NotImplementedException();
    }

    public Fabricante? SelecionarPorId(int idSelecionado)
    {
        throw new NotImplementedException();
    }

    public List<Fabricante> SelecionarTodos()
    {
        throw new NotImplementedException();
    }
}