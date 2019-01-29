using Microsoft.Extensions.DependencyInjection;
using TCE.Teste.Livraria.Infra.CrossCutting.IOC;

namespace TCE.Teste.Livraria.Services.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDIConfiguration(this IServiceCollection services)
        {
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
