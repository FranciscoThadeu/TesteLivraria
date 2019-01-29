using System;

namespace TCE.Teste.Livraria.Tests.API.DTO
{
    public class LivroReturnJson
    {
        public bool success { get; set; }
        public LivroDTO data { get; set; }
    }
    public class LivroDTO
    {
        public Guid Id { get; set; }
        public string Isbn { get;  set; }
        public string Autor { get;  set; }
        public string Nome { get;  set; }
        public decimal Valor { get;  set; }
        public DateTime DataPublicacao { get;  set; }
        public byte[] ImgCapa { get;  set; }
    }
}
