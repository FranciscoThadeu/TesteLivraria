using TCE.Teste.Livraria.Domain.Interfaces;

namespace TCE.Teste.Livraria.Domain.Livros.Repository
{
    public interface ILivroRepository : IRepository<Livro>
    {
        Livro ObterPorIsbn(string isbn);
        bool IsbnExiste(string isbn);
        bool ExcluirPorIsbn(string isbn);
    }
}
