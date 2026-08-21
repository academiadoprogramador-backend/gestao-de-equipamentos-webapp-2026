using GestaoDeEquipamentos.WebApp.Compartilhado.Dominio;

public sealed class Fabricante : EntidadeBase
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;

    public Fabricante() { }

    public Fabricante(string nome, string email, string telefone) : this()
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;
    }

    public override void Atualizar(EntidadeBase entidadeAtualizada)
    {
        Fabricante fabricanteAtualizado = (Fabricante)entidadeAtualizada;

        Nome = fabricanteAtualizado.Nome;
        Email = fabricanteAtualizado.Email;
        Telefone = fabricanteAtualizado.Telefone;
    }
}