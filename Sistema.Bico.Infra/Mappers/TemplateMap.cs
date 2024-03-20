using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Enums;
using System;

namespace Sistema.Bico.Infra.Mappers
{
    public class TemplateMap : IEntityTypeConfiguration<Template>
    {
        private const string NOME_TABELA = "TB_Template";
        public void Configure(EntityTypeBuilder<Template> builder)
        {
            builder.ToTable(NOME_TABELA);

            builder.HasKey(x => new { x.Id }).HasName($"PK_{NOME_TABELA}");

            //builder.HasData(
            //    new Template { Id = Guid.NewGuid(), TypeTemplate = TypeTemplate.Cadastro, Description = "" });
        }
    }
}
