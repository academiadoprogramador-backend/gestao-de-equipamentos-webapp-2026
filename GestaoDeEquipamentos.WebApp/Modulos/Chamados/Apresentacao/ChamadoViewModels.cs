using System.ComponentModel.DataAnnotations;

namespace GestaoDeEquipamentos.WebApp.Modulos.Chamados.Apresentacao;

public record ListarChamadoViewModel(
    int Id,
    string Titulo,
    string NomeEquipamento,
    DateTime DataAbertura,
    int DiasEmAberto
);

public record SelecionarEquipamentoViewModel(int Id, string Nome);

public record CadastrarChamadoViewModel(
    [Required(ErrorMessage = "O campo \"Título\" é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo \"Título\" deve conter no máximo 100 caracteres.")]
    string? Titulo,

    [Required(ErrorMessage = "O campo \"Descrição\" é obrigatório.")]
    [StringLength(1000, ErrorMessage = "O campo \"Descrição\" deve conter no máximo 1000 caracteres.")]
    string? Descricao,

    [Range(1, int.MaxValue, ErrorMessage = "O campo \"Equipamento\" é obrigatório.")]
    int EquipamentoId,

    [Required(ErrorMessage = "O campo \"Data de abertura\" é obrigatório.")]
    [DataType(DataType.Date)]
    DateTime? DataAbertura,

    List<SelecionarEquipamentoViewModel>? EquipamentosDisponiveis
);

public record EditarChamadoViewModel(
    int Id,

    [Required(ErrorMessage = "O campo \"Título\" é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo \"Título\" deve conter no máximo 100 caracteres.")]
    string? Titulo,

    [Required(ErrorMessage = "O campo \"Descrição\" é obrigatório.")]
    [StringLength(1000, ErrorMessage = "O campo \"Descrição\" deve conter no máximo 1000 caracteres.")]
    string? Descricao,

    [Range(1, int.MaxValue, ErrorMessage = "O campo \"Equipamento\" é obrigatório.")]
    int EquipamentoId,

    [Required(ErrorMessage = "O campo \"Data de abertura\" é obrigatório.")]
    [DataType(DataType.Date)]
    DateTime? DataAbertura,

    List<SelecionarEquipamentoViewModel>? EquipamentosDisponiveis
);

public record ExcluirChamadoViewModel(int Id, string Titulo);
