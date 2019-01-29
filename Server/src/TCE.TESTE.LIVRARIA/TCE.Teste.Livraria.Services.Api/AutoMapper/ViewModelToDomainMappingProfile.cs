using AutoMapper;
using TCE.Teste.Livraria.Domain.Livros.Commands;
using TCE.Teste.Livraria.Services.Api.ViewModels;

namespace TCE.Teste.Livraria.Services.Api.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<LivroViewModel, IncluirLivroCommand>()
                .ConstructUsing(c => new IncluirLivroCommand(c.Isbn,c.Autor, c.Nome,c.Valor, c.DataPublicacao, c.ImgCapa));

            CreateMap<LivroViewModel, AtualizarLivroCommand>()
                .ConstructUsing(c => new AtualizarLivroCommand(c.Id, c.Isbn, c.Autor, c.Nome, c.Valor, c.DataPublicacao, c.ImgCapa));

            CreateMap<LivroViewModel, ExcluirLivroCommand>()
                .ConstructUsing(c => new ExcluirLivroCommand(c.Isbn));
        }
    }
}
