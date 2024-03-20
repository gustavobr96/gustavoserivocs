using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Bico.Domain.Entities;

namespace Sistema.Bico.Infra.Mappers
{
    public class ThreeAvaliationMap : IEntityTypeConfiguration<ThreeAvaliation>
    {
        private const string NOME_TABELA = "TB_ThreeAvaliation";

        public void Configure(EntityTypeBuilder<ThreeAvaliation> builder)
        {
            builder.ToTable(NOME_TABELA);

            builder.HasKey(x => new { x.Id }).HasName($"PK_{NOME_TABELA}");
        }
    }
}
