using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Bico.Domain.Entities;

namespace Sistema.Bico.Infra.Mappers
{
    public class WorkerDoneMap : IEntityTypeConfiguration<WorkerDone>
    {
        private const string NOME_TABELA = "TB_WorkerDone";
        public void Configure(EntityTypeBuilder<WorkerDone> builder)
        {
            builder.ToTable(NOME_TABELA);

            builder.HasKey(x => new { x.Id }).HasName($"PK_{NOME_TABELA}");
        }
    }
}
