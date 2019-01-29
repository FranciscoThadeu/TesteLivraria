using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;

namespace TCE.Teste.Livraria.Services.Api.Configurations
{
    public static class SwaggerConfiguration
    {

        public static void AddSwaggerConfig(this IServiceCollection services, string caminhoAplicacao)
        {


            string nomeAplicacao = "TCE.Teste.Livraria.Services.Api";
            string caminhoXmlDoc =  Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Teste.Livraria API",
                    Description = "API para teste do TCE - Livraria",
                    TermsOfService = "Nenhum",
                    Contact = new Contact { Name = "Desenvolvedor Francisco Tadeu", Email = "fsouza.thadeu@gmail.com" },
                   // License = new License { Name = "MIT", Url = "" }
                });

               // s.OperationFilter<AuthorizationHeaderParameterOperationFilter>();

                s.IncludeXmlComments(caminhoXmlDoc);
            });

            //services.ConfigureSwaggerGen(opt =>
            //{
            //    opt.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
            //});



        }
    }
}
