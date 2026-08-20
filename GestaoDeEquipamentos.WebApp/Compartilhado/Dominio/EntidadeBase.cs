namespace GestaoDeEquipamentos.WebApp.Compartilhado.Dominio;

public abstract class EntidadeBase
{
    public int Id { get; set; }

    public abstract void Atualizar(EntidadeBase entidadeAtualizada);
}