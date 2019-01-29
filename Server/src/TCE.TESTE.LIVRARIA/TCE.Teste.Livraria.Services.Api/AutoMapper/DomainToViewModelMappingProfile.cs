using AutoMapper;
using TCE.Teste.Livraria.Domain.Livros;
using TCE.Teste.Livraria.Services.Api.ViewModels;

namespace TCE.Teste.Livraria.Services.Api.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Livro, LivroViewModel>();
        }
    }
}
