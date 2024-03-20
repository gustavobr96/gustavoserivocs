using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Bico.Domain.Entities;

namespace Sistema.Bico.Infra.Mappers
{
    public class WorkerDoneProfessionalMap : IEntityTypeConfiguration<WorkerDoneProfessional>
    {
        private const string NOME_TABELA = "TB_WorkerDoneProfessional";
        public void Configure(EntityTypeBuilder<WorkerDoneProfessional> builder)
        {
            builder.ToTable(NOME_TABELA);

            builder.HasKey(x => new { x.Id }).HasName($"PK_{NOME_TABELA}");


            builder.HasMany(e => e.WorkerDone).WithOne(e => e.WorkerDoneProfessional).HasForeignKey(x => x.WorkerDoneProfessionalId);
        }
    }
}
