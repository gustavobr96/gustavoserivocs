using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Bico.Domain.Entities;

namespace Sistema.Bico.Infra.Mappers
{
    public class ProfessionalProfileMap : IEntityTypeConfiguration<ProfessionalProfile>
    {
        private const string NOME_TABELA = "TB_ProfessionalProfile";
        public void Configure(EntityTypeBuilder<ProfessionalProfile> builder)
        {
            builder.ToTable(NOME_TABELA);

            builder.HasKey(x => new { x.Id }).HasName($"PK_{NOME_TABELA}");

            builder.HasMany(e => e.Especiality).WithOne(e => e.ProfessionalProfile).HasForeignKey(x => x.IdProfessionalProfile);
            builder.HasMany(e => e.WorkerProfessional).WithOne(e => e.ProfessionalProfile).HasForeignKey(x => x.ProfessionalProfileId);
            builder.HasMany(e => e.ProfessionalClient).WithOne(e => e.ProfessionalProfile).HasForeignKey(x => x.ProfessionalProfileId);
            builder.HasMany(e => e.WorkerDoneProfessional).WithOne(e => e.ProfessionalProfile).HasForeignKey(x => x.ProfessionalProfileId);
        }
    }
}
