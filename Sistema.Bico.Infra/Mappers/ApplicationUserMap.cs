using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Bico.Domain.Generics.Entities;

namespace Sistema.Bico.Infra.Mappers
{
    public class ApplicationUserMap : IEntityTypeConfiguration<ApplicationUser>
    {
        private const string NOME_TABELA = "TB_AspNetUsers";
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable(NOME_TABELA);

            builder.HasKey(x => new { x.Id }).HasName($"PK_{NOME_TABELA}");

            builder.HasOne(pt => pt.Client).WithMany(p => p.ApplicationUser).HasForeignKey(pt => pt.ClientId);
        }
    }

}
