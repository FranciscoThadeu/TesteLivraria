using System;

namespace TCE.Teste.Livraria.Domain.Livros.Events
{
    public class LivroRegistradoEvent : BaseLivroEvent
    {
        public LivroRegistradoEvent(Guid id, string isbn, string autor, string nome, decimal valor, DateTime dataPublicacao, byte[] imgCapa)
        {
            Id = id;
            Isbn = isbn;
            Autor = autor;
            Nome = nome;
            Valor = valor;
            DataPublicacao = dataPublicacao;
            ImgCapa = imgCapa;

            AggregateId = id;
        }
    }
}
