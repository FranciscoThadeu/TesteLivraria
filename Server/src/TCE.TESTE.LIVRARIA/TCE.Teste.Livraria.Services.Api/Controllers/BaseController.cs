using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Linq;
using TCE.Teste.Livraria.Domain.Core.Notifications;
using TCE.Teste.Livraria.Domain.Interfaces;

namespace TCE.Teste.Livraria.Services.Api.Controllers
{
    [Produces("application/json")]
    public abstract class BaseController : Controller
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediator;


        protected BaseController(INotificationHandler<DomainNotification> notifications,
                                                  IMediatorHandler mediator)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediator = mediator;

        }

        protected new IActionResult Response(object result = null)
        {
            var collection = result as ICollection;
           

            if (OperacaoValida())
            {
                if (collection == null || collection.Count == 0)
                {
                    return NotFound(new
                    {
                        success = false,
                        data = "Dados não encontrados"
                    });

                }
                else
                {
                    return Ok(new
                    {
                        success = true,
                        data = result
                    });
                }
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifications.GetNotifications().Select(n => n.Value)
            });
        }

        protected bool OperacaoValida()
        {
            return (!_notifications.HasNotifications());
        }

        protected void NotificarErroModelInvalida()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(string.Empty, erroMsg);
            }
        }

        protected void NotificarErro(string codigo, string mensagem)
        {
            _mediator.PublicarEvento(new DomainNotification(codigo, mensagem));
        }


    }
}
