using System.Threading.Tasks;
using TCE.Teste.Livraria.Domain.Core.Commands;
using TCE.Teste.Livraria.Domain.Core.Events;

namespace TCE.Teste.Livraria.Domain.Interfaces
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        Task EnviarComando<T>(T comando) where T : Command;
    }
}
