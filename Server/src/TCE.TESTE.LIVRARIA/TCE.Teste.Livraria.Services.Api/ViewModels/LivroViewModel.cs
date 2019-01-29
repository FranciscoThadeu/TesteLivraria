using System;
using System.ComponentModel.DataAnnotations;

namespace TCE.Teste.Livraria.Services.Api.ViewModels
{
    public class LivroViewModel
    {
        public LivroViewModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O ISBN é requerido")]
        [Display(Name = "ISBN")]
        public string Isbn { get; set; }

        [Required(ErrorMessage = "O Autor é requerido")]
        [MinLength(2, ErrorMessage = "O tamanho minimo do Autor é {1}")]
        [MaxLength(150, ErrorMessage = "O tamanho máximo do Autor é {1}")]
        [Display(Name = "Nome do Autor")]
        public string Autor { get; set; }

        [Required(ErrorMessage = "O nome do livro é requerido")]
        [MinLength(2, ErrorMessage = "O tamanho minimo do nome do livro é {1}")]
        [MaxLength(150, ErrorMessage = "O tamanho máximo do nome do livro é {1}")]
        [Display(Name = "Nome do Livro")]
        public string Nome { get; set; }

        [Display(Name = "Data da Puplicação")]
        [Required(ErrorMessage = "A data da publicação é requerida")]
        public DateTime DataPublicacao { get; set; }

        [Display(Name = "Preço")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [DataType(DataType.Currency, ErrorMessage = "Moeda em formato inválido")]
        public decimal Valor { get; set; }

        [Display(Name = "Imagem do Livro")]
        public byte[] ImgCapa { get; set; }
    }
}
