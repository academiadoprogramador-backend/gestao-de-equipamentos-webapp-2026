namespace GestaoDeEquipamentos.WebApp.Modulos.Fabricantes.Dominio;

public interface IRepositorioFabricante
{
    void Cadastrar(Fabricante novoRegistro);
    bool Editar(int idSelecionado, Fabricante entidadeAtualizada);
    bool Excluir(int idSelecionado);
    Fabricante? SelecionarPorId(int idSelecionado);
    List<Fabricante> SelecionarTodos();
}