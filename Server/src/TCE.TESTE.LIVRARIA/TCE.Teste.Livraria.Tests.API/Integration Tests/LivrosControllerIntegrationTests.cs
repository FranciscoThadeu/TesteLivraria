using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TCE.Teste.Livraria.Services.Api.ViewModels;
using TCE.Teste.Livraria.Tests.API.DTO;
using Xunit;
using Xunit.Priority;

namespace TCE.Teste.Livraria.Tests.API.Integration_Tests
{
    [TestCaseOrderer(PriorityOrderer.Assembly, PriorityOrderer.Name)]
    public class LivrosControllerIntegrationTests
    {
        private static string caminho = AppDomain.CurrentDomain.BaseDirectory.ToString().Replace("\\bin\\Debug\\netcoreapp2.1\\", "");
        protected byte[] image = File.ReadAllBytes(caminho + "\\Integration Tests\\Imagens\\DDD.JPG");
        // = System.IO.File.ReadAllBytes(Context.Request.PathRootPath + "\\Imagens\\DDD.JPG");
        public LivrosControllerIntegrationTests()
        {
            Environment.CriarServidor();
        
        }
        [Fact(DisplayName = "1-Livro incluído com sucesso"), Priority(1)]
        [Trait("Category", "Testes de integração API")]
        public async Task LivrosController_IncluirNovoLivro_RetornarComSucesso()
        {

            var livro = new LivroViewModel
            {
                Isbn = "978-85-508-0065-3",
                Autor = "Eric Evans",
                Nome = "Domain Driven Design",
                Valor = 500,
                DataPublicacao = DateTime.Now.AddDays(1),
                ImgCapa = image
            };

            // Act
            var response = await Environment.Server
                .CreateRequest("api/v1/livros")
                .And(
                    request =>
                        request.Content =
                            new StringContent(JsonConvert.SerializeObject(livro), Encoding.UTF8, "application/json"))
                //.And(request => request.Method = HttpMethod.Put)
                .PostAsync();

            var livroResult = JsonConvert.DeserializeObject<LivroReturnJson>(await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.IsType<LivroDTO>(livroResult.data);
        }

        [Fact(DisplayName = "2-Livro não incluído, mesmo isbn, com sucesso"), Priority(2)]
        [Trait("Category", "Testes de integração API")]
        public async Task LivrosController_IncluirLivroExistente_RetornarSemSucesso()
        {

            var livro = new LivroViewModel
            {
                Isbn = "978-85-508-0065-3",
                Autor = "Mike Evans",
                Nome = "Domain Driven Design 2",
                Valor = 400,
                DataPublicacao = DateTime.Now.AddDays(-3),
                ImgCapa = image
            };

            // Act
            var response = await Environment.Server
                .CreateRequest("api/v1/livros")
                .And(
                    request =>
                        request.Content =
                            new StringContent(JsonConvert.SerializeObject(livro), Encoding.UTF8, "application/json"))
                //.And(request => request.Method = HttpMethod.Put)
                .PostAsync();

            var livroResult = JsonConvert.DeserializeObject<LivroReturnJson>(await response.Content.ReadAsStringAsync());

            // Assert
            //response.IsSuccessStatusCode();
            Assert.IsNotType<LivroDTO>(livroResult.data);
        }

        [Fact(DisplayName = "3-Livro excluído com sucesso"), Priority(3)]
        [Trait("Category", "Testes de integração API")]
        public async Task LivrosController_ExcluirLivroExistente_RetornarComSucesso()
        {
            string Isbn = "978-85-508-0065-3";
            var response = await Environment.Server
                .CreateRequest("api/v1/livros/" + Isbn)
                .SendAsync("Delete");

            var livroResult = JsonConvert.DeserializeObject<LivroReturnJson>(await response.Content.ReadAsStringAsync());

            // Assert
            // response.IsSuccessStatusCode();
            Assert.Null(livroResult.data);
        }


    }
}
