namespace GestaoDeEquipamentos.WebApp.Modulos.Equipamentos.Dominio;

public interface IRepositorioEquipamento
{
    void Cadastrar(Equipamento novoRegistro);
    bool Editar(int idSelecionado, Equipamento entidadeAtualizada);
    bool Excluir(int idSelecionado);
    Equipamento? SelecionarPorId(int idSelecionado);
    List<Equipamento> SelecionarTodos();
}