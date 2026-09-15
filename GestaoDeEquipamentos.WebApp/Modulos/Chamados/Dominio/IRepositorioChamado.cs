namespace GestaoDeEquipamentos.WebApp.Modulos.Chamados.Dominio;

public interface IRepositorioChamado
{
    void Cadastrar(Chamado novoRegistro);
    bool Editar(int idSelecionado, Chamado entidadeAtualizada);
    bool Excluir(int idSelecionado);
    Chamado? SelecionarPorId(int idSelecionado);
    List<Chamado> SelecionarTodos();
    bool ExisteParaEquipamento(int equipamentoId);
}
