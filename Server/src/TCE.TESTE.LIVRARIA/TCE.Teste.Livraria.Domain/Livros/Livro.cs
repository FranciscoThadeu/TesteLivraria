using FluentValidation;
using System;
using TCE.Teste.Livraria.Domain.Core.Models;

namespace TCE.Teste.Livraria.Domain.Livros
{
    public class Livro : Entity<Livro>
    {
        public string isbn;
        public string autor;

        public Livro(string isbn, string autor, string nome, decimal valor, DateTime dataPublicacao, byte[] imgCapa)
        {
            Id = Guid.NewGuid();
            Isbn = isbn;
            Autor = autor;
            Nome = nome;
            Valor = valor;
            DataPublicacao = dataPublicacao;
            ImgCapa = imgCapa;
        }

        private Livro() { }

        public string Isbn { get; private set; }
        public string Autor { get; private set; }
        public string Nome { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataPublicacao { get; private set; }
        public byte[] ImgCapa { get; private set; }

        public override bool EhValido()
        {
            Validar();
            return ValidationResult.IsValid;
        }

        #region Validações

        private void Validar()
        {
            ValidarIsbn();
            ValidarAutor();
            ValidarNome();
            ValidarValor();
            ValidarDataPublicacao();
            ValidationResult = Validate(this);
        }

        private void ValidarIsbn()
        {
            RuleFor(c => c.Isbn)
                .NotEmpty().WithMessage("O ISBN precisa ser fornecido");

            if (!ISBN.IsValid(Isbn))
                RuleFor(c => c.Isbn)
               .NotEmpty().WithMessage("O ISBN precisa ser válido");


        }
        private void ValidarAutor()
        {
            RuleFor(c => c.Autor)
                .NotEmpty().WithMessage("O nome do autor precisa ser fornecido")
                .Length(2, 150).WithMessage("O nome do autor precisa ter entre 2 e 150 caracteres");
        }
        private void ValidarNome()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome do livro precisa ser fornecido")
                .Length(2, 150).WithMessage("O nome do livro precisa ter entre 2 e 150 caracteres");
        }
        private void ValidarValor()
        {
                RuleFor(c => c.Valor)
                    .NotEqual(0)
                    .WithMessage("O preço deve ser diferente de 0(zero)");
        }
        private void ValidarDataPublicacao()
        {
            RuleFor(c => c.DataPublicacao)
                .LessThan(DateTime.Now.AddDays(1))
                .WithMessage("A data da publicação não deve ser maior que a data de amanhã");
        }

        #endregion
    }
}
