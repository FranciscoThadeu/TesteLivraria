using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using AutoMapper;
using TCE.Teste.Livraria.Domain.Interfaces;
using TCE.Teste.Livraria.Domain.Livros.Repository;
using TCE.Teste.Livraria.Domain.Core.Notifications;
using TCE.Teste.Livraria.Services.Api.ViewModels;
using TCE.Teste.Livraria.Domain.Livros.Commands;

namespace TCE.Teste.Livraria.Services.Api.Controllers
{
    public class LivrosController : BaseController
    {
        private readonly ILivroRepository _livroRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        public LivrosController(INotificationHandler<DomainNotification> notifications,
                                  ILivroRepository livroRepository,
                                 IMapper mapper,
                                 IMediatorHandler mediator) : base(notifications, mediator)
        {
            _livroRepository = livroRepository;
            _mapper = mapper;
            _mediator = mediator;
        }
        /// <summary>
        /// Incluir Livros. Permitido somente para perfil Administrador
        /// </summary>
        /// <param name="livroViewModel"> Isbn: Códido do sistema internacional de identificação de livros.
        /// Autor: Nome do Autor do livro
        /// Nome: Nome do livro
        /// DataPublicacao: Data da publicação do livro
        /// Valor: Preço do livro
        /// ImgCapa: Imagem da capa do Livro no formato .png ou .jpg
        /// </param>
        [HttpPost]
        [Route("livros")]
        [AllowAnonymous]
        public IActionResult Post([FromBody]LivroViewModel livroViewModel)
        {
            if (!ModelStateValida()) return Response();

            var livroCommand = _mapper.Map<IncluirLivroCommand>(livroViewModel);

            _mediator.EnviarComando(livroCommand);
            return Response(livroCommand);
        }

        /// <summary>
        /// Atualizar Livros. Permitido somente para perfil Administrador
        /// </summary>
        /// <param name="livroViewModel"> Isbn: Códido do sistema internacional de identificação de livros.
        /// Autor: Nome do Autor do livro
        /// Nome: Nome do livro
        /// DataPublicacao: Data da publicação do livro
        /// Valor: Preço do livro
        /// ImgCapa: Imagem da capa do Livro no formato .png ou .jpg
        /// </param>
        [HttpPut]
        [Route("livros")]
        [AllowAnonymous]
        public IActionResult Put([FromBody]LivroViewModel livroViewModel)
        {
           // if (!ModelStateValida()) return Response();
            var livroCommand = _mapper.Map<AtualizarLivroCommand>(livroViewModel);

            _mediator.EnviarComando(livroCommand);
            return Response(livroCommand);
        }

        /// <summary>
        /// Remover Livros. Permitido somente para perfil Administrador
        /// </summary>
        /// <param name="isbn"> Isbn
        /// </param>
        [HttpDelete]
        [Route("livros/{isbn}")]
        [AllowAnonymous]
        public IActionResult Delete(string isbn)
        {
            var livroViewModel = new LivroViewModel { Isbn = isbn };
            var livroCommand = _mapper.Map<ExcluirLivroCommand>(livroViewModel);

            _mediator.EnviarComando(livroCommand);
            return Response(livroCommand);
        }

        /// <summary>
        /// Listas todos os  Livros.
        /// </summary>
        [HttpGet]
        [Route("livros")]
        [AllowAnonymous]
        public IActionResult Get()
        {
            var livroCommand = _mapper.Map<IEnumerable<LivroViewModel>>(_livroRepository.ObterTodos());
            return Response(livroCommand);
        }

        /// <summary>
        /// Listas Livros por ID
        /// </summary>
        /// <param name="isbn"> ISBN 
        /// </param>
        [HttpGet]
        [AllowAnonymous]
        [Route("livros/{isbn}")]
        public IActionResult Get(string isbn)
        {
            var livroCommand = _mapper.Map<LivroViewModel>(_livroRepository.ObterPorIsbn(isbn));
            return Response(livroCommand);
        }
   

        private bool ModelStateValida()
        {
            if (ModelState.IsValid) return true;

            NotificarErroModelInvalida();
            return false;
        }
    }
}
