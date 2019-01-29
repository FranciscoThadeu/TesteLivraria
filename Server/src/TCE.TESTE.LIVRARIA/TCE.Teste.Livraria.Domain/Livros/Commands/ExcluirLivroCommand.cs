using System;

namespace TCE.Teste.Livraria.Domain.Livros.Commands
{
    public class ExcluirLivroCommand : BaseLivroCommand
    {
        public ExcluirLivroCommand(string isbn)
        {
            Isbn = isbn;
        }
    }
}
