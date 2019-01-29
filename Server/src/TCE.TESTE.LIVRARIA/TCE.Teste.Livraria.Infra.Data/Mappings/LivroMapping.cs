using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCE.Teste.Livraria.Domain.Livros;
using TCE.Teste.Livraria.Infra.Data.Extensions;

namespace TCE.Teste.Livraria.Infra.Data.Mappings
{
    public class LivroMapping : EntityTypeConfiguration<Livro>
    {
        public override void Map(EntityTypeBuilder<Livro> builder)
        {
            builder.Property(e => e.Isbn)
              .HasColumnType("varchar(150)")
              .IsRequired();

            builder.Property(e => e.Autor)
               .HasColumnType("varchar(150)")
               .IsRequired();

            builder.Property(e => e.Nome)
               .HasColumnType("varchar(150)")
               .IsRequired();

            builder.Property(e => e.Valor)
              .HasColumnType("decimal(10,2)");

            builder.Ignore(e => e.ValidationResult);

            builder.Ignore(e => e.CascadeMode);

            builder.ToTable("Livros");
        }
    }
}
