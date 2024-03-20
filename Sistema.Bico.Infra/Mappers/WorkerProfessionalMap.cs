using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Bico.Domain.Entities;

namespace Sistema.Bico.Infra.Mappers
{

    public class WorkerProfessionalMap : IEntityTypeConfiguration<WorkerProfessional>
    {
        private const string NOME_TABELA = "TB_WorkerProfessional";
        public void Configure(EntityTypeBuilder<WorkerProfessional> builder)
        {
            builder.ToTable(NOME_TABELA);

            builder.HasKey(x => new { x.Id }).HasName($"PK_{NOME_TABELA}");
        }
    }
}
