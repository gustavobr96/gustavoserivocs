using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Bico.Domain.Entities;

namespace Sistema.Bico.Infra.Mappers
{
    public class ProfessionalClientMap : IEntityTypeConfiguration<ProfessionalClient>
    {
        private const string NOME_TABELA = "TB_ProfessionalClient";
        public void Configure(EntityTypeBuilder<ProfessionalClient> builder)
        {
            builder.ToTable(NOME_TABELA);

            builder.HasKey(x => new { x.Id }).HasName($"PK_{NOME_TABELA}");
        }
    }
}
