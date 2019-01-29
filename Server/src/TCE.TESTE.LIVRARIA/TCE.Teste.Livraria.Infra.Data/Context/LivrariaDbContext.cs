using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using TCE.Teste.Livraria.Domain.Livros;
using TCE.Teste.Livraria.Infra.Data.Extensions;
using TCE.Teste.Livraria.Infra.Data.Mappings;

namespace TCE.Teste.Livraria.Infra.Data.Context
{
    public class LivrariaDbContext : DbContext
    {
        private string _contentRootPath = "";
        public IConfigurationRoot Configuration { get; }
        public LivrariaDbContext(DbContextOptions<LivrariaDbContext> options, IHostingEnvironment env)
           : base(options)
        {
            _contentRootPath = env.ContentRootPath;
        }

        public DbSet<Livro> Livros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new LivroMapping());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string conn = config.GetConnectionString("DefaultConnection");
            if (conn.Contains("%CONTENTROOTPATH%"))
            {
                conn = conn.Replace("%CONTENTROOTPATH%", _contentRootPath);
                conn = conn.Replace("TCE.Teste.Livraria.Tests.API\\bin\\Debug\\netcoreapp2.1", "TCE.Teste.Livraria.Services.API");
            }

            optionsBuilder.UseSqlServer(conn);
        }
    }
}
