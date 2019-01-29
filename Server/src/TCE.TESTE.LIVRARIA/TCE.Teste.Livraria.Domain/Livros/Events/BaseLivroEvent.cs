using System;
using TCE.Teste.Livraria.Domain.Core.Events;

namespace TCE.Teste.Livraria.Domain.Livros.Events
{
    public class BaseLivroEvent : Event
    {
        public Guid Id { get; protected set; }
        public string Isbn { get; protected set; }
        public string Autor { get; protected set; }
        public string Nome { get; protected set; }
        public decimal Valor { get; protected set; }
        public DateTime DataPublicacao { get; protected set; }
        public byte[] ImgCapa { get; protected set; }
    }
}
