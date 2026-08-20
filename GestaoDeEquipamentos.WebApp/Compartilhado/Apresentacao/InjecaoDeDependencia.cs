namespace GestaoDeEquipamentos.WebApp.Compartilhado.Apresesntacao;

public static class InjecaoDeDependencia
{
    public static void AdicionarCamadaDeApresentacao(this IServiceCollection services)
    {
        // Razor = CSHTML
        services.AddControllersWithViews().AddRazorOptions(options =>
        {
            // Reseta o mecanismo de busca de views
            options.ViewLocationFormats.Clear();

            // Configura localização das views compartilhadas
            options.ViewLocationFormats.Add("/Compartilhado/Apresentacao/Views/{0}.cshtml");

            // Configura localização das views de módulos
            options.ViewLocationFormats.Add("/Modulos/{1}s/Apresentacao/Views/{0}.cshtml");
        });
    }
}