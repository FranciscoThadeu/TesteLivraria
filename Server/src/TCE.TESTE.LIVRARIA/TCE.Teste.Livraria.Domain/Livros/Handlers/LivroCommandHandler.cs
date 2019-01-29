using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TCE.Teste.Livraria.Domain.Core.Notifications;
using TCE.Teste.Livraria.Domain.Handlers;
using TCE.Teste.Livraria.Domain.Interfaces;
using TCE.Teste.Livraria.Domain.Livros.Commands;
using TCE.Teste.Livraria.Domain.Livros.Events;
using TCE.Teste.Livraria.Domain.Livros.Repository;

namespace TCE.Teste.Livraria.Domain.Livros.Handlers
{
    public class LivroCommandHandler : CommandHandler,
        IRequestHandler<IncluirLivroCommand>,
        IRequestHandler<AtualizarLivroCommand>,
        IRequestHandler<ExcluirLivroCommand>
    {

        private readonly ILivroRepository _livroRepository;
        private readonly IMediatorHandler _mediator;
        public LivroCommandHandler(ILivroRepository livroRepository,
                                   IUnitOfWork uow,
                                   INotificationHandler<DomainNotification> notifications,
                                   IMediatorHandler mediator) : base(uow, mediator, notifications)
        {
            _livroRepository = livroRepository;
            _mediator = mediator;
        }
        public Task Handle(IncluirLivroCommand request, CancellationToken cancellationToken)
        {
            var livro = new Livro(request.Isbn, request.Autor, request.Nome, request.Valor, request.DataPublicacao, request.ImgCapa);

            if (!LivroValido(livro))
            {
                NotificarValidacoesErro(livro.ValidationResult);
                return Task.CompletedTask;
            }

            if (IsbnExistente(livro.Isbn, request.MessageType))
            {
                NotificarValidacoesErro(livro.ValidationResult);
                return Task.CompletedTask;
            }

            _livroRepository.Adicionar(livro);

            if (Commit())
            {
                _mediator.PublicarEvento(new LivroRegistradoEvent(request.Id, request.Isbn, request.Autor, request.Nome, request.Valor, request.DataPublicacao, request.ImgCapa));
            }

            return Task.CompletedTask;
        }

        public Task Handle(AtualizarLivroCommand request, CancellationToken cancellationToken)
        {
            var livroAtual = _livroRepository.ObterPorId(request.Id);

            if (!LivroExistente(request.Id, request.MessageType)) return Task.CompletedTask;

            var livro = new Livro(request.Isbn, request.Autor, request.Nome, request.Valor, request.DataPublicacao, request.ImgCapa);

            if (IsbnExistente(livro.Isbn, request.MessageType))
            {
                NotificarValidacoesErro(livro.ValidationResult);
                return Task.CompletedTask;
            }

            _livroRepository.Atualizar(livro);

            if (Commit())
            {
                _mediator.PublicarEvento(new LivroAtualizadoEvent(request.Id, request.Isbn, request.Autor, request.Nome, request.Valor, request.DataPublicacao, request.ImgCapa));
            }

            return Task.CompletedTask;
        }

        public Task Handle(ExcluirLivroCommand request, CancellationToken cancellationToken)
        {
            if (!IsbnExistente(request.Isbn, request.MessageType)) return Task.CompletedTask;

            _livroRepository.ExcluirPorIsbn(request.Isbn);

            if (Commit())
            {
                _mediator.PublicarEvento(new LivroExcluidoEvent(request.Isbn));
            }

            return Task.CompletedTask;
        }

        private bool LivroValido(Livro livro)
        {
            if (livro.EhValido()) return true;

            NotificarValidacoesErro(livro.ValidationResult);
            return false;
        }

        private bool LivroExistente(Guid id, string messageType)
        {
            var livro = _livroRepository.ObterPorId(id);

            if (livro != null) return true;

            _mediator.PublicarEvento(new DomainNotification(messageType, "Livro não encontrado."));
            return false;
        }

        private bool IsbnExistente(string isbn, string messageType)
        {
            var livro = _livroRepository.ObterPorIsbn(isbn);

            if (livro == null) return false;

            _mediator.PublicarEvento(new DomainNotification(messageType, "ISBN já cadastrado."));
            return true;
        }

    }
}
