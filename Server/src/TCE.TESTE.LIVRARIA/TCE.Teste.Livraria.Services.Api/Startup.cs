using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Formatters;

using TCE.Teste.Livraria.Services.Api.Configurations;
using TCE.Teste.Livraria.Services.Api.Middlewares;
using TCE.Teste.Livraria.Infra.Data.Context;

namespace TCE.Teste.Livraria.Services.Api
{
    public class Startup
    {
        private readonly string _contentRootPath = "";
        public Startup(IHostingEnvironment env)
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Contexto do EF para o Identity
            string conn = Configuration.GetConnectionString("DefaultConnection");
            if (conn.Contains("%CONTENTROOTPATH%"))
            {
                conn = conn.Replace("%CONTENTROOTPATH%", _contentRootPath);
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
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Versionamento do WebApi
            services.AddApiVersioning("api/v{version}");

            // AutoMapper
          //  services.AddAutoMapper(typeof(Startup));
            services.AddAutoMapper();

            // Configurações do Swagger
            services.AddSwaggerConfig(_contentRootPath);

            // MediatR
            services.AddMediatR(typeof(Startup));

            // Registrar todos os DI
            services.AddDIConfiguration();


            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
           // app.UseAuthentication();
            app.UseMvc();

            #endregion

            #region Swagger

            if (env.IsProduction())
            {
                // Se não tiver um token válido no browser não funciona.
                // Descomente para ativar a segurança.
               // app.UseSwaggerAuthorized();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Teste.Livraria API v1.0");
            });

            #endregion
        }
    }
}
