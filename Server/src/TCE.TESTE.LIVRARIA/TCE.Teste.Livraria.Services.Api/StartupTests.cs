using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Formatters;
using TCE.Teste.Livraria.Services.Api.Configurations;
using TCE.Teste.Livraria.Infra.Data.Context;

namespace TCE.Teste.Livraria.Services.Api
{

    public class StartupTests
    {
        private string _contentRootPath = "";
        public StartupTests(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            _contentRootPath = env.ContentRootPath;
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Contexto do EF para o Identity
            string conn = Configuration.GetConnectionString("DefaultConnection");
            if (conn.Contains("%CONTENTROOTPATH%"))
            {
                conn = conn.Replace("%CONTENTROOTPATH%", _contentRootPath);
                conn = conn.Replace("TCE.Teste.Livraria.Tests.API\\bin\\Debug\\netcoreapp2.1", "TCE.Teste.Livraria.Services.API");
            }


            services.AddDbContext<LivrariaDbContext>(options =>
              options.UseSqlServer(conn));

            // Configurações de Autenticação, Autorização e JWT.
            services.AddMvcSecurity(Configuration);

            // Options para configurações customizadas
            services.AddOptions();

            // MVC com restrição de XML e adição de filtro de ações.
            services.AddMvc(options =>
            {
                options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
            });

            // Versionamento do WebApi
            services.AddApiVersioning("api/v{version}");

            // AutoMapper
            // Necessário add os assemblies para TestServer
            services.AddAutoMapper(typeof(Startup).GetTypeInfo().Assembly);
            //var assembly = typeof(Program).GetTypeInfo().Assembly;
            //services.AddAutoMapper(assembly);

            // MediatR
            services.AddMediatR(typeof(Startup));

            // Registrar todos os DI
            services.AddDIConfiguration();
        }

        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            IHttpContextAccessor accessor)
        {


            #region Configurações MVC

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseStaticFiles();
            //app.UseAuthentication();
            app.UseMvc();

            #endregion
        }
    }
}
