using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Bico.Domain.Entities;

namespace Sistema.Bico.Infra.Mappers
{
    public class WorkerMap : IEntityTypeConfiguration<Worker>
    {
        private const string NOME_TABELA = "TB_Worker";
        public void Configure(EntityTypeBuilder<Worker> builder)
        {
            builder.ToTable(NOME_TABELA);

            builder.HasKey(x => new { x.Id }).HasName($"PK_{NOME_TABELA}");


            builder.HasMany(e => e.WorkerProfessional).WithOne(e => e.Worker).HasForeignKey(x => x.WorkerId);
        }
    }
}
