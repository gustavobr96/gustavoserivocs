using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Bico.Domain.Entities;

namespace Sistema.Bico.Infra.Mappers
{
    public class ProfessionalPaymentMap : IEntityTypeConfiguration<ProfessionalPayment>
    {
        private const string NOME_TABELA = "TB_ProfessionalPayment";
        public void Configure(EntityTypeBuilder<ProfessionalPayment> builder)
        {
            builder.ToTable(NOME_TABELA);

            builder.HasKey(x => new { x.Id }).HasName($"PK_{NOME_TABELA}");
        }
    }
}
