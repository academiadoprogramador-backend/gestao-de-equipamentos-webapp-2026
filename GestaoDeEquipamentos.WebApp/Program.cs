using GestaoDeEquipamentos.WebApp.Compartilhado.Apresesntacao;
using GestaoDeEquipamentos.WebApp.Compartilhado.Infraestrutura;

var builder = WebApplication.CreateBuilder(args);

// Configurar a infraestrutura (Arquivos, Banco de Dados, Logs, Cachês, etc...)
builder.Services.AdicionarCamadaDeInfraestrutura(builder.Configuration);

// Configurar o MVC / Apresentação
builder.Services.AdicionarCamadaDeApresentacao();

var app = builder.Build();

// Middlewares
app.UseRouting();
app.MapDefaultControllerRoute();

app.UseStaticFiles();

// Executa o servidor
app.Run();
