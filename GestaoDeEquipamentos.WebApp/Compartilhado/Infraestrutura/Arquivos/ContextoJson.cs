using System.Text.Json;
using System.Text.Json.Serialization;
using GestaoDeEquipamentos.WebApp.Modulos.Chamados.Dominio;
using GestaoDeEquipamentos.WebApp.Modulos.Equipamentos.Dominio;

namespace GestaoDeEquipamentos.WebApp.Compartilhado.Infraestrutura.Arquivos;

public sealed class ContextoJson
{
    private readonly string caminhoArquivoDados;

    public List<Fabricante> Fabricantes { get; set; } = new List<Fabricante>();
    public List<Equipamento> Equipamentos { get; set; } = new List<Equipamento>();
    public List<Chamado> Chamados { get; set; } = new List<Chamado>();

    public ContextoJson()
    {
        string caminhoAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        string caminhoDiretorioAplicativo = Path.Join(caminhoAppData, "GestaoDeEquipamentos-Backend");

        Directory.CreateDirectory(caminhoDiretorioAplicativo);

        caminhoArquivoDados = Path.Join(caminhoDiretorioAplicativo, "dados.json");
    }

    public void Salvar()
    {
        JsonSerializerOptions options = new JsonSerializerOptions();
        options.WriteIndented = true;
        options.ReferenceHandler = ReferenceHandler.Preserve;

        string jsonString = JsonSerializer.Serialize(this, options);

        File.WriteAllText(caminhoArquivoDados, jsonString);
    }

    public void Carregar()
    {
        if (!File.Exists(caminhoArquivoDados))
        {
            Carregar(CarregarDadosPredefinidos());
            return;
        }

        string jsonString = File.ReadAllText(caminhoArquivoDados);

        if (string.IsNullOrWhiteSpace(jsonString))
        {
            Carregar(CarregarDadosPredefinidos());
            return;
        }

        JsonSerializerOptions options = new JsonSerializerOptions();
        options.WriteIndented = true;
        options.ReferenceHandler = ReferenceHandler.Preserve;

        ContextoJson? contextoSalvo =
            JsonSerializer.Deserialize<ContextoJson>(jsonString, options);

        if (contextoSalvo == null || !contextoSalvo.PossuiDados())
            contextoSalvo = CarregarDadosPredefinidos();

        Carregar(contextoSalvo);
    }

    private void Carregar(ContextoJson contexto)
    {
        Fabricantes = contexto.Fabricantes;
        Equipamentos = contexto.Equipamentos;
        Chamados = contexto.Chamados;
    }

    public ContextoJson CarregarDadosPredefinidos()
    {
        ContextoJson contextoPredefinido = new ContextoJson();

        contextoPredefinido.Fabricantes.AddRange(new List<Fabricante>
        {
            new Fabricante("TechNova Equipamentos Ltda.", "contato@technova.com.br", "(11) 3456-7801") { Id = 1 },
            new Fabricante("SoluMaq Industrial Ltda.", "vendas@solumaq.com.br", "(21) 2345-6702") { Id = 2 },
            new Fabricante("NorteSul Tecnologia S.A.", "atendimento@nortesultec.com.br", "(31) 3234-5603") { Id = 3 },
            new Fabricante("InovaOffice Suprimentos Ltda.", "comercial@inovaoffice.com.br", "(41) 3345-6704") { Id = 4 },
            new Fabricante("PrimeData Sistemas Ltda.", "suporte@primedata.com.br", "(51) 3123-4505") { Id = 5 }
        });

        contextoPredefinido.Equipamentos.AddRange(new List<Equipamento>
        {
            new("Notebook Dell", 3000m, DateTime.Parse("02/10/2023"), contextoPredefinido.Fabricantes[0]) { Id = 1 },
            new("Monitor Acer", 600m, DateTime.Parse("08/25/2025"), contextoPredefinido.Fabricantes[3]) { Id = 2 },
        });

        contextoPredefinido.Chamados.AddRange(new List<Chamado>
        {
            new("Tela não liga", "O notebook não apresenta imagem ao ser ligado.", contextoPredefinido.Equipamentos[0], DateTime.Today.AddDays(-3)) { Id = 1 },
            new("Monitor piscando", "A imagem do monitor pisca durante o uso.", contextoPredefinido.Equipamentos[1], DateTime.Today.AddDays(-1)) { Id = 2 }
        });

        return contextoPredefinido;
    }

    private bool PossuiDados()
    {
        return
            Fabricantes.Count > 0 &&
            Equipamentos.Count > 0;
    }
}
