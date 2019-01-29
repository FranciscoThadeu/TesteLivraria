using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TCE.Teste.Livraria.Domain.Livros.Events;

namespace TCE.Teste.Livraria.Domain.Livros.Handlers
{
    public class LivroEventHandler :
        INotificationHandler<LivroRegistradoEvent>,
        INotificationHandler<LivroAtualizadoEvent>,
        INotificationHandler<LivroExcluidoEvent>
    {
        public Task Handle(LivroRegistradoEvent notification, CancellationToken cancellationToken)
        {
            // TODO: Disparar alguma ação
            return Task.CompletedTask;
        }

        public Task Handle(LivroAtualizadoEvent notification, CancellationToken cancellationToken)
        {
            // TODO: Disparar alguma ação
            return Task.CompletedTask;
        }

        public Task Handle(LivroExcluidoEvent notification, CancellationToken cancellationToken)
        {
            // TODO: Disparar alguma ação
            return Task.CompletedTask;
        }
    }
}
