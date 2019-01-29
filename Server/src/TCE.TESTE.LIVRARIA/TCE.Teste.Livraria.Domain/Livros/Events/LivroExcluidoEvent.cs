using System;

namespace TCE.Teste.Livraria.Domain.Livros.Events
{
    public class LivroExcluidoEvent : BaseLivroEvent
    {
        public LivroExcluidoEvent(string isbn)
        {
            Isbn = isbn;
        }
    }
}
