using System;

namespace TCE.Teste.Livraria.Domain.Livros.Commands
{
    public class IncluirLivroCommand : BaseLivroCommand
    {
        public IncluirLivroCommand(string isbn, string autor, string nome, decimal valor, DateTime dataPublicacao, byte[] imgCapa)
        {
            Isbn = isbn;
            Autor = autor;
            Nome = nome;
            Valor = valor;
            DataPublicacao = dataPublicacao;
            ImgCapa = imgCapa;
        }
    }
}
