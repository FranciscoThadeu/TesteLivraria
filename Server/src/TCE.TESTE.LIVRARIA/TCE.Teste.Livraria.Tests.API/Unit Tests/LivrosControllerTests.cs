using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TCE.Teste.Livraria.Domain.Core.Notifications;
using TCE.Teste.Livraria.Domain.Interfaces;
using TCE.Teste.Livraria.Domain.Livros.Commands;
using TCE.Teste.Livraria.Domain.Livros.Repository;
using TCE.Teste.Livraria.Services.Api.Controllers;
using TCE.Teste.Livraria.Services.Api.ViewModels;
using Xunit;

namespace TCE.Teste.Livraria.Tests.API.Unit_Tests
{
    public class LivrosControllerTests
    {
        public LivrosController livrosController { get; set; }
        public LivroViewModel livroViewModel { get; set; }
        public IncluirLivroCommand incluirLivroCommand { get; set; }
        public AtualizarLivroCommand atualizarLivroCommand { get; set; }
        public ExcluirLivroCommand excluirLivroCommand { get; set; }
        public Mock<IMapper> mockMapper { get; set; }
        public Mock<IMediatorHandler> mockMediator { get; set; }
        public Mock<DomainNotificationHandler> mockNotification { get; set; }
        public LivrosControllerTests()
        {
            mockMapper = new Mock<IMapper>();
            mockMediator = new Mock<IMediatorHandler>();
            mockNotification = new Mock<DomainNotificationHandler>();

            var mockRepository = new Mock<ILivroRepository>();

            livroViewModel = new LivroViewModel();

            incluirLivroCommand = new IncluirLivroCommand("978-8536509266", "Java 8. Programação de Computadores", "José Augusto N. G. Manzano e Roberto Affonso da Costa Junior", Convert.ToDecimal("94.70"),DateTime.Now,null);
            atualizarLivroCommand = new AtualizarLivroCommand(Guid.Parse("0f93729b-225f-425e-99c4-cc72f0720e2d"), "978-8536509266", "Java 8. Programação de Computadores", "José Augusto N. G. Manzano", Convert.ToDecimal("100.05"), DateTime.Now, null);
            excluirLivroCommand = new ExcluirLivroCommand("978-8536509266");

            livrosController = new LivrosController(
                mockNotification.Object,
                mockRepository.Object,
                mockMapper.Object,
                mockMediator.Object);
        }

        // Incluir um livro com sucesso
        // Incluir um livro com falha na viewmodel
        // Incluir um livro com falha na validacao de dominio

        // AAA => Arrange, Act, Assert
        [Fact(DisplayName = "Incluir livro - OK Result")]
        [Trait("Category", "Testes Livros Controller")]
        public void LivrosController_IncluirLivro_RetornarComSucesso()
        {
            // Arrange
            mockMapper.Setup(m => m.Map<IncluirLivroCommand>(livroViewModel)).Returns(incluirLivroCommand);

            // Act
            var result = livrosController.Post(livroViewModel);

            // Assert
            mockMediator.Verify(m => m.EnviarComando(incluirLivroCommand), Times.Once);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact(DisplayName = "Incluir Livro com erro de ModelState")]
        [Trait("Category", "Testes Livros Controller")]
        public void LivrosController_IncluirLivro_RetornarComErrosDeModelState()
        {
            // Arrange
            mockMapper.Setup(m => m.Map<IncluirLivroCommand>(livroViewModel)).Returns(incluirLivroCommand);
            var notificationList = new List<DomainNotification>
            {
                new DomainNotification("Erro","Model Error")
            };

            mockNotification.Setup(c => c.GetNotifications()).Returns(notificationList);
            mockNotification.Setup(c => c.HasNotifications()).Returns(true);

            livrosController.ModelState.AddModelError("Erro", "Model Error");

            // Act
            var result = livrosController.Post(livroViewModel);

            // Assert
            mockMapper.Verify(m => m.Map<IncluirLivroCommand>(livroViewModel), Times.Never);
            mockMediator.Verify(m => m.EnviarComando(incluirLivroCommand), Times.Never);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact(DisplayName = "Incluir Livro com erro de Dominio")]
        [Trait("Category", "Testes Livros Controller")]
        public void LivrosController_IncluirLivro_RetornarComErrosDeDominio()
        {
            // Arrange
            mockMapper.Setup(m => m.Map<IncluirLivroCommand>(livroViewModel)).Returns(incluirLivroCommand);
            var notificationList = new List<DomainNotification>
            {
                new DomainNotification("Erro","Domain Error")
            };

            mockNotification.Setup(c => c.GetNotifications()).Returns(notificationList);
            mockNotification.Setup(c => c.HasNotifications()).Returns(true);

            // Act
            var result = livrosController.Post(livroViewModel);

            // Assert
            mockMediator.Verify(m => m.EnviarComando(incluirLivroCommand), Times.Once);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact(DisplayName = "Atualizar Livro com Sucesso")]
        [Trait("Category", "Testes Livros Controller")]
        public void LivrosController_AtualizarLivro_RetornarComSucesso()
        {
            // Arrange
            mockMapper.Setup(m => m.Map<AtualizarLivroCommand>(livroViewModel)).Returns(atualizarLivroCommand);

            // Act
            var result = livrosController.Put(livroViewModel);

            // Assert
            mockMediator.Verify(m => m.EnviarComando(atualizarLivroCommand), Times.Once);
            Assert.IsType<OkObjectResult>(result);
                                 

        }
        [Fact(DisplayName = "Excluir Livro com Sucesso")]
        [Trait("Category", "Testes Livros Controller")]
        public void LivrosController_ExcluirLivro_RetornarComSucesso()
        {
            // Arrange
            mockMapper.Setup(m => m.Map<ExcluirLivroCommand>(livroViewModel)).Returns(excluirLivroCommand);

            // Act
            var result = livrosController.Delete(livroViewModel.Isbn);

            // Assert
           // mockMediator.Verify(m => m.EnviarComando(excluirLivroCommand),"success:true");
            Assert.IsType<OkObjectResult>(result);


        }
    }
}
