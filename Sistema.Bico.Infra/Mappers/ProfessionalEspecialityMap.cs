using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Bico.Domain.Entities;

namespace Sistema.Bico.Infra.Mappers
{
    public class ProfessionalEspecialityMap : IEntityTypeConfiguration<ProfessionalEspeciality>
    {
        private const string NOME_TABELA = "TB_ProfessionalEspeciality";
        public void Configure(EntityTypeBuilder<ProfessionalEspeciality> builder)
        {
            builder.ToTable(NOME_TABELA);

            builder.HasKey(x => new { x.Id }).HasName($"PK_{NOME_TABELA}");
        }
    }
}
