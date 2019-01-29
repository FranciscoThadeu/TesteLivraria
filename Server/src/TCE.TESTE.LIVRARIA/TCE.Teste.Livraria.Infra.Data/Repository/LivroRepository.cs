using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TCE.Teste.Livraria.Domain.Livros;
using TCE.Teste.Livraria.Domain.Livros.Repository;
using TCE.Teste.Livraria.Infra.Data.Context;

namespace TCE.Teste.Livraria.Infra.Data.Repository
{
    public class LivroRepository : Repository<Livro>, ILivroRepository
    {
        public LivroRepository(LivrariaDbContext context) : base(context)
        {

        }

        public Livro ObterPorIsbn(string isbn)
        {

            var sql = @"SELECT * FROM LIVROS L " +
                   "WHERE L.ISBN =  @pIsbn ";

            var livro = Db.Database.GetDbConnection().Query<Livro>(sql, new { pIsbn = isbn } );

            return livro.FirstOrDefault();
        }

        public bool IsbnExiste(string isbn)
        {

            var sql = @"SELECT * FROM LIVROS L " +
                   "WHERE L.ISBN =  @pIsbn ";

            var livro = Db.Database.GetDbConnection().Query<Livro>(sql, new { pIsbn = isbn });

            if (livro.FirstOrDefault().Any()) return true;

            return false;
        }

        public bool ExcluirPorIsbn(string isbn)
        {

            var sql = @"DELETE FROM LIVROS " +
                   "WHERE ISBN =  @pIsbn ";

            var livro = Db.Database.GetDbConnection().Execute(sql, new { pIsbn = isbn });

            if (livro!= 0) return true;

            return false;
        }

      
    }
}
