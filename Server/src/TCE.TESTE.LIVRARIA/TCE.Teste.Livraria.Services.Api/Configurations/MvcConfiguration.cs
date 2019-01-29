using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TCE.Teste.Livraria.Services.Api.Configurations
{
    public static class MvcConfiguration
    {
        public static void AddMvcSecurity(this IServiceCollection services, IConfigurationRoot configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

        }
    }
}
