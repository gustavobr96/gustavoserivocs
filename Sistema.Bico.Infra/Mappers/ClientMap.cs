using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Bico.Domain.Entities;

namespace Sistema.Bico.Infra.Mappers
{
    public class ClientMap : IEntityTypeConfiguration<Client>
    {
        private const string NOME_TABELA = "TB_Client";

        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable(NOME_TABELA);

            builder.HasKey(x => new { x.Id }).HasName($"PK_{NOME_TABELA}");
            builder.HasMany(e => e.ProfessionalClient).WithOne(e => e.Client).HasForeignKey(x => x.ClientId);
        }
    }
}
