using MediatR;
using System.Threading.Tasks;
using TCE.Teste.Livraria.Domain.Core.Commands;
using TCE.Teste.Livraria.Domain.Core.Events;
using TCE.Teste.Livraria.Domain.Interfaces;

namespace TCE.Teste.Livraria.Domain.Handlers
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task EnviarComando<T>(T comando) where T : Command
        {
            await _mediator.Send(comando);
        }

        public async Task PublicarEvento<T>(T evento) where T : Event
        {
            await _mediator.Publish(evento);
        }
    }
}
