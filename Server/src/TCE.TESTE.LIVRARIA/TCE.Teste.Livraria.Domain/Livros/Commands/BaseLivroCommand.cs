using System;
using TCE.Teste.Livraria.Domain.Core.Commands;

namespace TCE.Teste.Livraria.Domain.Livros.Commands
{
    public class BaseLivroCommand : Command
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
