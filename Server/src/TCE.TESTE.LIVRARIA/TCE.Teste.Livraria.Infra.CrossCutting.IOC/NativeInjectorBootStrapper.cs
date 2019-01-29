using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TCE.Teste.Livraria.Domain.Core.Notifications;
using TCE.Teste.Livraria.Domain.Handlers;
using TCE.Teste.Livraria.Domain.Interfaces;
using TCE.Teste.Livraria.Domain.Livros.Commands;
using TCE.Teste.Livraria.Domain.Livros.Events;
using TCE.Teste.Livraria.Domain.Livros.Handlers;
using TCE.Teste.Livraria.Domain.Livros.Repository;
using TCE.Teste.Livraria.Infra.CrossCutting.AspNetFilters;
using TCE.Teste.Livraria.Infra.Data.Context;
using TCE.Teste.Livraria.Infra.Data.Repository;
using TCE.Teste.Livraria.Infra.Data.UoW;

namespace TCE.Teste.Livraria.Infra.CrossCutting.IOC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASPNET
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<AtualizarLivroCommand>, LivroCommandHandler>();
            services.AddScoped<IRequestHandler<ExcluirLivroCommand>, LivroCommandHandler>();
            services.AddScoped<IRequestHandler<IncluirLivroCommand>, LivroCommandHandler>();

            // Domain - Eventos
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<LivroRegistradoEvent>, LivroEventHandler>();
            services.AddScoped<INotificationHandler<LivroAtualizadoEvent>, LivroEventHandler>();
            services.AddScoped<INotificationHandler<LivroExcluidoEvent>, LivroEventHandler>();



            //// Infra - Data
            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<LivrariaDbContext>();


            // Infra - Filtros
            services.AddScoped<ILogger<GlobalExceptionHandlingFilter>, Logger<GlobalExceptionHandlingFilter>>();
            services.AddScoped<GlobalExceptionHandlingFilter>();
        }
    }
}
