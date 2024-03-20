using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sistema.Bico.Infra.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TB_ProfessionalArea",
                columns: new[] { "Id", "Codigo", "Created", "Description", "Update" },
                values: new object[,]
                {
                    { new Guid("1a92ee06-2f5c-443b-954d-d21ba742c693"), 12, new DateTime(2023, 3, 15, 13, 58, 1, 934, DateTimeKind.Local).AddTicks(7195), "Edição de Vídeos", null },
                    { new Guid("260656e6-2295-4e8d-99d5-05e151cded13"), 8, new DateTime(2023, 3, 15, 13, 58, 1, 934, DateTimeKind.Local).AddTicks(7189), "Design/Tecnologia", null },
                    { new Guid("279cd0f1-21d4-46c5-8eaa-ec681331ecec"), 11, new DateTime(2023, 3, 15, 13, 58, 1, 934, DateTimeKind.Local).AddTicks(7193), "Engenharias", null },
                    { new Guid("27f1af35-f460-4104-b46c-7a4c04b2992e"), 2, new DateTime(2023, 3, 15, 13, 58, 1, 934, DateTimeKind.Local).AddTicks(7161), "Administação e Finanças", null },
                    { new Guid("4318304a-c7b6-41ce-8883-54784cf0f751"), 7, new DateTime(2023, 3, 15, 13, 58, 1, 934, DateTimeKind.Local).AddTicks(7170), "Comunicação", null },
                    { new Guid("53e872b0-fd3a-49c1-b47d-d03af76e11f9"), 1, new DateTime(2023, 3, 15, 13, 58, 1, 934, DateTimeKind.Local).AddTicks(7117), "Assistência Técnica", null },
                    { new Guid("5e41ee15-6f50-4303-a541-a6b2f90ac202"), 9, new DateTime(2023, 3, 15, 13, 58, 1, 934, DateTimeKind.Local).AddTicks(7191), "Eventos", null },
                    { new Guid("766a8025-d225-43e5-9d61-5b7e084f32df"), 6, new DateTime(2023, 3, 15, 13, 58, 1, 934, DateTimeKind.Local).AddTicks(7169), "Criador de Conteúdos", null },
                    { new Guid("8a0a311f-4ea0-4dc5-b298-1f8bd38663a0"), 17, new DateTime(2023, 3, 15, 13, 58, 1, 934, DateTimeKind.Local).AddTicks(7204), "Reformas e Reparos", null },
                    { new Guid("8f80f502-2dae-411f-89c8-c21fd1150591"), 4, new DateTime(2023, 3, 15, 13, 58, 1, 934, DateTimeKind.Local).AddTicks(7166), "Autos", null },
                    { new Guid("a9f3ab80-ca51-435a-8b12-23ebb2be1a70"), 5, new DateTime(2023, 3, 15, 13, 58, 1, 934, DateTimeKind.Local).AddTicks(7168), "Consultoria", null },
                    { new Guid("ac7d28ca-0a2d-43fd-984c-ae48ac80de1b"), 14, new DateTime(2023, 3, 15, 13, 58, 1, 934, DateTimeKind.Local).AddTicks(7198), "Marketing e Vendas", null },
                    { new Guid("b8171e9d-c777-4d39-bc3d-0d70ea845491"), 13, new DateTime(2023, 3, 15, 13, 58, 1, 934, DateTimeKind.Local).AddTicks(7196), "Tradução e Conteúdos", null },
                    { new Guid("c34c845c-9593-403d-848e-a9ea6782a99e"), 19, new DateTime(2023, 3, 15, 13, 58, 1, 934, DateTimeKind.Local).AddTicks(7207), "Serviços Domésticos", null },
                    { new Guid("d0770626-8dfc-4ae9-a995-5da95f3dff5b"), 18, new DateTime(2023, 3, 15, 13, 58, 1, 934, DateTimeKind.Local).AddTicks(7206), "Saúde", null },
                    { new Guid("d110d08a-a92e-4b1b-a3d7-bc25b2cb3955"), 10, new DateTime(2023, 3, 15, 13, 58, 1, 934, DateTimeKind.Local).AddTicks(7192), "Entregador", null },
                    { new Guid("e23d54a7-6c6f-4506-8dea-a3a480bdc447"), 15, new DateTime(2023, 3, 15, 13, 58, 1, 934, DateTimeKind.Local).AddTicks(7199), "Moda e Beleza", null },
                    { new Guid("f5f77db0-9656-4e8d-bc41-e0149d3a7ec1"), 16, new DateTime(2023, 3, 15, 13, 58, 1, 934, DateTimeKind.Local).AddTicks(7203), "Jurídico", null },
                    { new Guid("f95de07a-ab29-44b5-9987-1b11c104cfaf"), 3, new DateTime(2023, 3, 15, 13, 58, 1, 934, DateTimeKind.Local).AddTicks(7164), "Veículos", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TB_ProfessionalArea",
                keyColumn: "Id",
                keyValue: new Guid("1a92ee06-2f5c-443b-954d-d21ba742c693"));

            migrationBuilder.DeleteData(
                table: "TB_ProfessionalArea",
                keyColumn: "Id",
                keyValue: new Guid("260656e6-2295-4e8d-99d5-05e151cded13"));

            migrationBuilder.DeleteData(
                table: "TB_ProfessionalArea",
                keyColumn: "Id",
                keyValue: new Guid("279cd0f1-21d4-46c5-8eaa-ec681331ecec"));

            migrationBuilder.DeleteData(
                table: "TB_ProfessionalArea",
                keyColumn: "Id",
                keyValue: new Guid("27f1af35-f460-4104-b46c-7a4c04b2992e"));

            migrationBuilder.DeleteData(
                table: "TB_ProfessionalArea",
                keyColumn: "Id",
                keyValue: new Guid("4318304a-c7b6-41ce-8883-54784cf0f751"));

            migrationBuilder.DeleteData(
                table: "TB_ProfessionalArea",
                keyColumn: "Id",
                keyValue: new Guid("53e872b0-fd3a-49c1-b47d-d03af76e11f9"));

            migrationBuilder.DeleteData(
                table: "TB_ProfessionalArea",
                keyColumn: "Id",
                keyValue: new Guid("5e41ee15-6f50-4303-a541-a6b2f90ac202"));

            migrationBuilder.DeleteData(
                table: "TB_ProfessionalArea",
                keyColumn: "Id",
                keyValue: new Guid("766a8025-d225-43e5-9d61-5b7e084f32df"));

            migrationBuilder.DeleteData(
                table: "TB_ProfessionalArea",
                keyColumn: "Id",
                keyValue: new Guid("8a0a311f-4ea0-4dc5-b298-1f8bd38663a0"));

            migrationBuilder.DeleteData(
                table: "TB_ProfessionalArea",
                keyColumn: "Id",
                keyValue: new Guid("8f80f502-2dae-411f-89c8-c21fd1150591"));

            migrationBuilder.DeleteData(
                table: "TB_ProfessionalArea",
                keyColumn: "Id",
                keyValue: new Guid("a9f3ab80-ca51-435a-8b12-23ebb2be1a70"));

            migrationBuilder.DeleteData(
                table: "TB_ProfessionalArea",
                keyColumn: "Id",
                keyValue: new Guid("ac7d28ca-0a2d-43fd-984c-ae48ac80de1b"));

            migrationBuilder.DeleteData(
                table: "TB_ProfessionalArea",
                keyColumn: "Id",
                keyValue: new Guid("b8171e9d-c777-4d39-bc3d-0d70ea845491"));

            migrationBuilder.DeleteData(
                table: "TB_ProfessionalArea",
                keyColumn: "Id",
                keyValue: new Guid("c34c845c-9593-403d-848e-a9ea6782a99e"));

            migrationBuilder.DeleteData(
                table: "TB_ProfessionalArea",
                keyColumn: "Id",
                keyValue: new Guid("d0770626-8dfc-4ae9-a995-5da95f3dff5b"));

            migrationBuilder.DeleteData(
                table: "TB_ProfessionalArea",
                keyColumn: "Id",
                keyValue: new Guid("d110d08a-a92e-4b1b-a3d7-bc25b2cb3955"));

            migrationBuilder.DeleteData(
                table: "TB_ProfessionalArea",
                keyColumn: "Id",
                keyValue: new Guid("e23d54a7-6c6f-4506-8dea-a3a480bdc447"));

            migrationBuilder.DeleteData(
                table: "TB_ProfessionalArea",
                keyColumn: "Id",
                keyValue: new Guid("f5f77db0-9656-4e8d-bc41-e0149d3a7ec1"));

            migrationBuilder.DeleteData(
                table: "TB_ProfessionalArea",
                keyColumn: "Id",
                keyValue: new Guid("f95de07a-ab29-44b5-9987-1b11c104cfaf"));
        }
    }
}
