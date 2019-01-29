using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using TCE.Teste.Livraria.Services.Api;

namespace TCE.Teste.Livraria.Tests.API
{
    public class Environment
    {
        public static TestServer Server { get; set; }
        public static HttpClient Client { get; set; }

        public static void CriarServidor()
        {
            Server = new TestServer(
                new WebHostBuilder()
                    .UseKestrel()
                    .UseEnvironment("Testing")
                    .UseApplicationInsights()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseUrls("http://localhost:8285")
                    .UseStartup<StartupTests>());

            Client = Server.CreateClient();





        }
    }
}
