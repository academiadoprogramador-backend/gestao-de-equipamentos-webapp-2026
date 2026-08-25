using System.ComponentModel.DataAnnotations;

namespace GestaoDeEquipamentos.WebApp.Modulos.Equipamentos.Apresentacao;

public record ListarEquipamentoViewModel(
    int Id,
    string Nome,
    decimal PrecoAquisicao,
    DateTime DataFabricacao,
    string NomeFabricante
);

public record SelecionarFabricanteViewModel(int Id, string Nome);

public record CadastrarEquipamentoViewModel(

    [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
    [StringLength(100, MinimumLength = 6,
        ErrorMessage = "O campo \"Nome\" deve conter entre 6 e 100 caracteres.")]
    string? Nome,

    [Required(ErrorMessage = "O campo \"Preço de aquisição\" é obrigatório.")]
    [Range(typeof(decimal), "0.01", "79228162514264337593543950335",
        ErrorMessage = "O campo \"Preço de aquisição\" deve ser maior que zero.")]
    decimal? PrecoAquisicao,

    [Required(ErrorMessage = "O campo \"Data de fabricação\" é obrigatório.")]
    [DataType(DataType.Date)]
    DateTime? DataFabricacao,

    [Range(1, int.MaxValue, ErrorMessage = "O campo \"Fabricante\" é obrigatório.")]
    int FabricanteId,

    List<SelecionarFabricanteViewModel>? FabricantesDisponiveis
);

public record EditarEquipamentoViewModel(
    int Id,

    [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
    [StringLength(100, MinimumLength = 6,
        ErrorMessage = "O campo \"Nome\" deve conter entre 6 e 100 caracteres.")]
    string? Nome,

    [Required(ErrorMessage = "O campo \"Preço de aquisição\" é obrigatório.")]
    [Range(typeof(decimal), "0.01", "79228162514264337593543950335",
        ErrorMessage = "O campo \"Preço de aquisição\" deve ser maior que zero.")]
    decimal? PrecoAquisicao,

    [Required(ErrorMessage = "O campo \"Data de fabricação\" é obrigatório.")]
    [DataType(DataType.Date)]
    DateTime? DataFabricacao,

    [Range(1, int.MaxValue, ErrorMessage = "O campo \"Fabricante\" é obrigatório.")]
    int FabricanteId,

    List<SelecionarFabricanteViewModel>? FabricantesDisponiveis
);

public record ExcluirEquipamentoViewModel(
    int Id,
    string Nome
);