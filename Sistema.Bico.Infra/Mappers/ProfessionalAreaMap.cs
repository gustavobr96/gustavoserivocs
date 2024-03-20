using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Bico.Domain.Entities;
using System;

namespace Sistema.Bico.Infra.Mappers
{
    public class ProfessionalAreaMap : IEntityTypeConfiguration<ProfessionalArea>
    {
        private const string NOME_TABELA = "TB_ProfessionalArea";
        public void Configure(EntityTypeBuilder<ProfessionalArea> builder)
        {
            builder.ToTable(NOME_TABELA);

            builder.HasKey(x => new { x.Id }).HasName($"PK_{NOME_TABELA}");

            builder.HasData(
                new ProfessionalArea { Id = Guid.NewGuid(), Codigo = 1, Description = "Assistência Técnica" },
                new ProfessionalArea { Id = Guid.NewGuid(), Codigo = 2, Description = "Administação e Finanças" },
                new ProfessionalArea { Id = Guid.NewGuid(), Codigo = 3, Description = "Veículos" },
                new ProfessionalArea { Id = Guid.NewGuid(), Codigo = 4, Description = "Autos" },
                new ProfessionalArea { Id = Guid.NewGuid(), Codigo = 5, Description = "Consultoria" },
                new ProfessionalArea { Id = Guid.NewGuid(), Codigo = 6, Description = "Criador de Conteúdos" },
                new ProfessionalArea { Id = Guid.NewGuid(), Codigo = 7, Description = "Comunicação" },
                new ProfessionalArea { Id = Guid.NewGuid(), Codigo = 8, Description = "Design/Tecnologia" },
                new ProfessionalArea { Id = Guid.NewGuid(), Codigo = 9, Description = "Eventos" },
                new ProfessionalArea { Id = Guid.NewGuid(), Codigo = 10, Description = "Entregador" },
                new ProfessionalArea { Id = Guid.NewGuid(), Codigo = 11, Description = "Engenharias" },
                new ProfessionalArea { Id = Guid.NewGuid(), Codigo = 12, Description = "Edição de Vídeos" },
                new ProfessionalArea { Id = Guid.NewGuid(), Codigo = 13, Description = "Tradução e Conteúdos" },
                new ProfessionalArea { Id = Guid.NewGuid(), Codigo = 14, Description = "Marketing e Vendas" },
                new ProfessionalArea { Id = Guid.NewGuid(), Codigo = 15, Description = "Moda e Beleza" },
                new ProfessionalArea { Id = Guid.NewGuid(), Codigo = 16, Description = "Jurídico" },
                new ProfessionalArea { Id = Guid.NewGuid(), Codigo = 17, Description = "Reformas e Reparos" },
                new ProfessionalArea { Id = Guid.NewGuid(), Codigo = 18, Description = "Saúde" },
                new ProfessionalArea { Id = Guid.NewGuid(), Codigo = 19, Description = "Serviços Domésticos" }
                );
        }
    }
}
