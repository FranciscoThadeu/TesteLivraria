using MediatR;
using System;
using TCE.Teste.Livraria.Domain.Core.Events;

namespace TCE.Teste.Livraria.Domain.Core.Commands
{
    public class Command : Message, IRequest
    {
        public DateTime Timestamp { get; private set; }

        public Command()
        {
            Timestamp = DateTime.Now;
        }
    }
}
