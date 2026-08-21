using System.ComponentModel.DataAnnotations;

namespace GestaoDeEquipamentos.WebApp.Modulos.Fabricantes.Apresentacao;

public record ListarFabricanteViewModel(
    int Id,
    string Nome,
    string Email,
    string Telefone
);

public record CadastrarFabricanteViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
    [StringLength(100, MinimumLength = 2,
        ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 100 caracteres.")]
    string? Nome,

    [Required(ErrorMessage = "O campo \"E-mail\" é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo \"E-mail\" deve conter um endereço válido.")]
    string? Email,

    [Required(ErrorMessage = "O campo \"Telefone\" é obrigatório.")]
    [RegularExpression(@"^\(\d{2}\) \d{4,5}-\d{4}$",
        ErrorMessage = "O campo \"Telefone\" deve estar no formato (DDD) 90000-0000.")]
    string? Telefone
);

public record EditarFabricanteViewModel(
    int Id,

    [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
    [StringLength(100, MinimumLength = 2,
        ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 100 caracteres.")]
    string? Nome,

    [Required(ErrorMessage = "O campo \"E-mail\" é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo \"E-mail\" deve conter um endereço válido.")]
    string? Email,

    [Required(ErrorMessage = "O campo \"Telefone\" é obrigatório.")]
    [RegularExpression(@"^\(\d{2}\) \d{4,5}-\d{4}$",
        ErrorMessage = "O campo \"Telefone\" deve estar no formato (DDD) 90000-0000.")]
    string? Telefone
);

public record ExcluirFabricanteViewModel(int Id, string Nome);
