using FluentValidation.Results;
using MediatR;
using TCE.Teste.Livraria.Domain.Core.Notifications;
using TCE.Teste.Livraria.Domain.Interfaces;

namespace TCE.Teste.Livraria.Domain.Handlers
{
    public abstract class CommandHandler
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediatorHandler _mediator;
        private readonly DomainNotificationHandler _notifications;

        public CommandHandler(IUnitOfWork uow, IMediatorHandler mediator, INotificationHandler<DomainNotification> notifications)
        {
            _uow = uow;
            _mediator = mediator;
            _notifications = (DomainNotificationHandler)notifications;

        }

        protected void NotificarValidacoesErro(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                _mediator.PublicarEvento(new DomainNotification(error.PropertyName, error.ErrorMessage));
            }
        }

        protected bool Commit()
        {
            if (_notifications.HasNotifications()) return false;
            if (_uow.Commit()) return true;

            _mediator.PublicarEvento(new DomainNotification("Commit", "Ocorreu um erro ao salvar os dados no banco"));
            return false;
        }


    }
}
