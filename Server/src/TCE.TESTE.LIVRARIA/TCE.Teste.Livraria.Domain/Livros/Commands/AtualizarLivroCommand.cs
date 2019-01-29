using System;

namespace TCE.Teste.Livraria.Domain.Livros.Commands
{
    public class AtualizarLivroCommand: BaseLivroCommand
    {
        public AtualizarLivroCommand(Guid id,string isbn, string autor, string nome, decimal valor, DateTime dataPublicacao, byte[] imgCapa)
        {
            Id = id;
            Isbn = isbn;
            Autor = autor;
            Nome = nome;
            Valor = valor;
            DataPublicacao = dataPublicacao;
            ImgCapa = imgCapa;
        }
    }
}
