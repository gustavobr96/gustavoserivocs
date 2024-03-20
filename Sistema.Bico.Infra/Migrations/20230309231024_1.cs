using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Sistema.Bico.Infra.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Logradouro = table.Column<string>(type: "text", nullable: true),
                    Number = table.Column<string>(type: "text", nullable: true),
                    Bairro = table.Column<string>(type: "text", nullable: true),
                    Complemento = table.Column<string>(type: "text", nullable: true),
                    ZipCode = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    State = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Update = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_Client",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    PerfilPicture = table.Column<byte[]>(type: "bytea", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    TypePeople = table.Column<int>(type: "integer", nullable: false),
                    CpfCnpj = table.Column<string>(type: "text", nullable: true),
                    IsServiceProvider = table.Column<bool>(type: "boolean", nullable: false),
                    Cancellation = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Enable = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Update = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_ProfessionalArea",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Update = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ProfessionalArea", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_Template",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    TypeTemplate = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Update = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Template", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_TermUse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    TypeTerm = table.Column<int>(type: "integer", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Update = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_TermUse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_TB_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "TB_Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_ProfessionalProfile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Perfil = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    PerfilPicture = table.Column<byte[]>(type: "bytea", nullable: true),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    AddressId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProfessionalAreaId = table.Column<Guid>(type: "uuid", nullable: true),
                    About = table.Column<string>(type: "text", nullable: true),
                    Profession = table.Column<string>(type: "text", nullable: true),
                    Avaliation = table.Column<decimal>(type: "numeric", nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    Premium = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Update = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ProfessionalProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_ProfessionalProfile_TB_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "TB_Address",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TB_ProfessionalProfile_TB_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "TB_Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_ProfessionalProfile_TB_ProfessionalArea_ProfessionalArea~",
                        column: x => x.ProfessionalAreaId,
                        principalTable: "TB_ProfessionalArea",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_ProfessionalClient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfessionalProfileId = table.Column<Guid>(type: "uuid", nullable: true),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: true),
                    StatusWorker = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Update = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ProfessionalClient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_ProfessionalClient_TB_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "TB_Client",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TB_ProfessionalClient_TB_ProfessionalProfile_ProfessionalPr~",
                        column: x => x.ProfessionalProfileId,
                        principalTable: "TB_ProfessionalProfile",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TB_ProfessionalPayment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    PagamentoId = table.Column<long>(type: "bigint", nullable: true),
                    ProfessionalId = table.Column<Guid>(type: "uuid", nullable: false),
                    Enable = table.Column<bool>(type: "boolean", nullable: false),
                    StatusPayment = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Update = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ProfessionalPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_ProfessionalPayment_TB_ProfessionalProfile_ProfessionalId",
                        column: x => x.ProfessionalId,
                        principalTable: "TB_ProfessionalProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_ThreeAvaliation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Deadline = table.Column<decimal>(type: "numeric", nullable: false),
                    Quality = table.Column<decimal>(type: "numeric", nullable: false),
                    Communication = table.Column<decimal>(type: "numeric", nullable: false),
                    NumberAvaliation = table.Column<int>(type: "integer", nullable: false),
                    ProfessionalProfileId = table.Column<Guid>(type: "uuid", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Update = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ThreeAvaliation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_ThreeAvaliation_TB_ProfessionalProfile_ProfessionalProfi~",
                        column: x => x.ProfessionalProfileId,
                        principalTable: "TB_ProfessionalProfile",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TB_Worker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    About = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<double>(type: "double precision", nullable: true),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    AddressId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProfessionalAreaId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProfessionalProfileConcludedId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsConcluded = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Update = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Worker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_Worker_TB_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "TB_Address",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TB_Worker_TB_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "TB_Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_Worker_TB_ProfessionalArea_ProfessionalAreaId",
                        column: x => x.ProfessionalAreaId,
                        principalTable: "TB_ProfessionalArea",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TB_Worker_TB_ProfessionalProfile_ProfessionalProfileConclud~",
                        column: x => x.ProfessionalProfileConcludedId,
                        principalTable: "TB_ProfessionalProfile",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TB_WorkerDoneProfessional",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfessionalProfileId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Update = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_WorkerDoneProfessional", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_WorkerDoneProfessional_TB_ProfessionalProfile_Profession~",
                        column: x => x.ProfessionalProfileId,
                        principalTable: "TB_ProfessionalProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_ProfessionalEspeciality",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IdProfessionalProfile = table.Column<Guid>(type: "uuid", nullable: false),
                    IdWorker = table.Column<Guid>(type: "uuid", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Update = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ProfessionalEspeciality", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_ProfessionalEspeciality_TB_ProfessionalProfile_IdProfess~",
                        column: x => x.IdProfessionalProfile,
                        principalTable: "TB_ProfessionalProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_ProfessionalEspeciality_TB_Worker_IdWorker",
                        column: x => x.IdWorker,
                        principalTable: "TB_Worker",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TB_WorkerProfessional",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfessionalProfileId = table.Column<Guid>(type: "uuid", nullable: true),
                    WorkerId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsConcluded = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Update = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_WorkerProfessional", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_WorkerProfessional_TB_ProfessionalProfile_ProfessionalPr~",
                        column: x => x.ProfessionalProfileId,
                        principalTable: "TB_ProfessionalProfile",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TB_WorkerProfessional_TB_Worker_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "TB_Worker",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TB_WorkerDone",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Avaliation = table.Column<decimal>(type: "numeric", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    WorkerDoneProfessionalId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Update = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_WorkerDone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_WorkerDone_TB_WorkerDoneProfessional_WorkerDoneProfessio~",
                        column: x => x.WorkerDoneProfessionalId,
                        principalTable: "TB_WorkerDoneProfessional",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TB_ProfessionalArea",
                columns: new[] { "Id", "Codigo", "Created", "Description", "Update" },
                values: new object[,]
                {
                    { new Guid("1573fc16-fa86-4278-b5b3-d0fa36a1da26"), 7, new DateTime(2023, 3, 9, 20, 10, 24, 194, DateTimeKind.Local).AddTicks(746), "Moda e Beleza", null },
                    { new Guid("2dd03559-9dba-4ed2-822c-e90d5dd0e86c"), 5, new DateTime(2023, 3, 9, 20, 10, 24, 194, DateTimeKind.Local).AddTicks(718), "Design/Tecnologia", null },
                    { new Guid("2f6272bd-d0e2-49b8-b329-636498994c11"), 6, new DateTime(2023, 3, 9, 20, 10, 24, 194, DateTimeKind.Local).AddTicks(720), "Eventos", null },
                    { new Guid("30ef9462-0718-4fdf-82cf-068d5cd184a6"), 4, new DateTime(2023, 3, 9, 20, 10, 24, 194, DateTimeKind.Local).AddTicks(717), "Consultoria", null },
                    { new Guid("88d451d3-74ef-470e-ae0f-3f50d8a8b6b5"), 8, new DateTime(2023, 3, 9, 20, 10, 24, 194, DateTimeKind.Local).AddTicks(748), "Reformas e Reparos", null },
                    { new Guid("ae15bca4-f396-4271-b401-62184512bab3"), 9, new DateTime(2023, 3, 9, 20, 10, 24, 194, DateTimeKind.Local).AddTicks(750), "Saúde", null },
                    { new Guid("b2baeafe-79bc-442f-87fc-3ddd5009451c"), 2, new DateTime(2023, 3, 9, 20, 10, 24, 194, DateTimeKind.Local).AddTicks(712), "Aulas", null },
                    { new Guid("bde41330-9098-4ccc-9461-4f93c7f7a5d9"), 1, new DateTime(2023, 3, 9, 20, 10, 24, 194, DateTimeKind.Local).AddTicks(661), "Assistência Técnica", null },
                    { new Guid("d158dafa-9f1f-41fb-a7a3-c67ae966fd4c"), 10, new DateTime(2023, 3, 9, 20, 10, 24, 194, DateTimeKind.Local).AddTicks(751), "Serviços Domésticos", null },
                    { new Guid("e38dcbe0-c263-4c25-aa4f-4e2375c66ba5"), 3, new DateTime(2023, 3, 9, 20, 10, 24, 194, DateTimeKind.Local).AddTicks(715), "Autos", null }
                });

            migrationBuilder.InsertData(
                table: "TB_Template",
                columns: new[] { "Id", "Created", "Description", "TypeTemplate", "Update" },
                values: new object[] { new Guid("9c51f5b4-8138-48de-8a11-d264664898c6"), new DateTime(2023, 3, 9, 20, 10, 24, 196, DateTimeKind.Local).AddTicks(4557), "", 1, null });

            migrationBuilder.InsertData(
                table: "TB_TermUse",
                columns: new[] { "Id", "Active", "Created", "Description", "TypeTerm", "Update", "Version" },
                values: new object[] { new Guid("ab04e16e-2740-49ce-837b-f5331d2020d5"), true, new DateTime(2023, 3, 9, 20, 10, 24, 195, DateTimeKind.Local).AddTicks(3002), "<h3>TERMO DE USO</h3>\r\n\r\n<p>&nbsp;</p>\r\n\r\n<h4>1. Quais informa&ccedil;&otilde;es est&atilde;o presentes neste documento?</h4>\r\n\r\n<p>Neste Termo de Uso, o usu&aacute;rio do servi&ccedil;o do Di&aacute;rio Oficial da Uni&atilde;o (DOU) disponibilizado pelo aplicativo mobile encontrar&aacute; informa&ccedil;&otilde;es sobre: o funcionamento do servi&ccedil;o e as regras aplic&aacute;veis a ele; o arcabou&ccedil;o legal relacionado &agrave; presta&ccedil;&atilde;o do servi&ccedil;o; as responsabilidades do usu&aacute;rio ao utilizar o servi&ccedil;o; as responsabilidades da Imprensa Nacional ao prover o servi&ccedil;o; informa&ccedil;&otilde;es para contato, caso exista alguma d&uacute;vida ou seja necess&aacute;rio atualizar informa&ccedil;&otilde;es; e o foro respons&aacute;vel por eventuais reclama&ccedil;&otilde;es caso quest&otilde;es deste Termo de Uso tenham sido violadas.</p>\r\n\r\n<h4>2. Aceita&ccedil;&atilde;o do Termo de Uso e Pol&iacute;tica de Privacidade</h4>\r\n\r\n<p>Ao utilizar os servi&ccedil;os, o usu&aacute;rio confirma que leu e compreendeu os Termos e Pol&iacute;ticas aplic&aacute;veis ao servi&ccedil;o Di&aacute;rio Oficial da Uni&atilde;o (DOU) disponibilizado pelo aplicativo mobile e concorda em ficar vinculado a eles.</p>\r\n\r\n<h4>3. Defini&ccedil;&otilde;es</h4>\r\n\r\n<p>Para melhor compreens&atilde;o deste documento, neste Termo de Uso e Pol&iacute;tica de Privacidade, consideram-se:</p>\r\n\r\n<p><strong>Internet:</strong>&nbsp;o sistema constitu&iacute;do do conjunto de protocolos l&oacute;gicos, estruturado em escala mundial para uso p&uacute;blico e irrestrito, com a finalidade de possibilitar a comunica&ccedil;&atilde;o de dados entre terminais por meio de diferentes redes.</p>\r\n\r\n<p><strong>S&iacute;tios e aplicativos:</strong>&nbsp;s&iacute;tios e aplicativos por meio dos quais o usu&aacute;rio acessa os servi&ccedil;os e conte&uacute;dos disponibilizados.</p>\r\n\r\n<p><strong>Usu&aacute;rios (ou &quot;Usu&aacute;rio&quot;, quando individualmente considerado):</strong>&nbsp;todas as pessoas naturais que utilizarem o servi&ccedil;o Di&aacute;rio Oficial da Uni&atilde;o (DOU).</p>\r\n\r\n<p><strong>Leitura em texto:</strong>&nbsp;modo de consulta ao conte&uacute;do do DOU que permite acesso individualizado aos atos publicados, em formato html.</p>\r\n\r\n<p><strong>Vers&atilde;o Certificada:</strong>&nbsp;modo de consulta ao conte&uacute;do do DOU que permite acesso &agrave; p&aacute;gina da edi&ccedil;&atilde;o contendo o ato pesquisado, certificada digitalmente, em formato pdf.</p>\r\n\r\n<h4>4. Quais s&atilde;o as leis e normativos aplic&aacute;veis a esse servi&ccedil;o?</h4>\r\n\r\n<p>- Decreto n&ordm; 8.777, de 11 de maio de 2016: Institui a Pol&iacute;tica de Dados Abertos do Poder Executivo federal.</p>\r\n\r\n<p>- Decreto n&ordm; 9.215, de 29 de novembro de 2017: Disp&otilde;e sobre a publica&ccedil;&atilde;o do Di&aacute;rio Oficial da Uni&atilde;o.</p>\r\n\r\n<p>- Portaria IN/SG/PR n&ordm; 9, de 4 de fevereiro de 2021: Disp&otilde;e sobre publica&ccedil;&atilde;o de atos no Di&aacute;rio Oficial da Uni&atilde;o.</p>\r\n\r\n<h4>5. Descri&ccedil;&atilde;o do servi&ccedil;o</h4>\r\n\r\n<p>Com o objetivo facilitar o acesso imediato aos atos oficiais publicados no Di&aacute;rio Oficial da Uni&atilde;o (DOU), a Imprensa Nacional da Secretaria-Geral da Presid&ecirc;ncia da Rep&uacute;blica disponibiliza gratuitamente o aplicativo DOU, que permite acesso ao conte&uacute;do da edi&ccedil;&atilde;o do dia, busca por edi&ccedil;&otilde;es passadas e ainda: filtros de leitura por &oacute;rg&atilde;o e/ou tipo de ato; sele&ccedil;&atilde;o</p>\r\n\r\n<p>de leitura preferencial di&aacute;ria; possibilidade de &ldquo;favoritar&rdquo;, salvar no dispositivo e compartilhar as mat&eacute;rias; e baixar a vers&atilde;o em pdf.</p>\r\n\r\n<h4>6. Quais s&atilde;o as obriga&ccedil;&otilde;es dos usu&aacute;rios que utilizam o servi&ccedil;o?</h4>\r\n\r\n<p>O Usu&aacute;rio &eacute; respons&aacute;vel pela repara&ccedil;&atilde;o de todos e quaisquer danos, diretos ou indiretos (inclusive decorrentes de viola&ccedil;&atilde;o de quaisquer direitos de outros usu&aacute;rios, de terceiros, inclusive direitos de propriedade intelectual, de sigilo e de personalidade), que sejam causados &agrave; Imprensa Nacional, a qualquer outro Usu&aacute;rio, ou, ainda, a qualquer terceiro, inclusive em virtude do descumprimento do disposto nestes Termos de Uso e Pol&iacute;tica de Privacidade ou de qualquer ato praticado a partir da utiliza&ccedil;&atilde;o do servi&ccedil;o.</p>\r\n\r\n<p>A Imprensa Nacional n&atilde;o poder&aacute; ser responsabilizada pelos seguintes fatos:</p>\r\n\r\n<p>a. Equipamento infectado ou invadido por atacantes;</p>\r\n\r\n<p>b. Equipamento avariado no momento do consumo de servi&ccedil;os;</p>\r\n\r\n<p>c. Prote&ccedil;&atilde;o do equipamento;</p>\r\n\r\n<p>d. Prote&ccedil;&atilde;o das informa&ccedil;&otilde;es baseadas nos equipamentos dos usu&aacute;rios; e. Abuso de uso dos equipamentos dos usu&aacute;rios;</p>\r\n\r\n<p>f. Monitora&ccedil;&atilde;o clandestina do equipamento dos usu&aacute;rios;</p>\r\n\r\n<p>g. Vulnerabilidades ou instabilidades existentes nos sistemas dos usu&aacute;rios;</p>\r\n\r\n<p>O uso comercial das express&otilde;es utilizadas em aplicativos como marca, nome empresarial ou nome de dom&iacute;nio, al&eacute;m dos conte&uacute;dos do servi&ccedil;o, assim como os programas, bancos de dados, redes e arquivos est&atilde;o protegidos pelas leis e tratados internacionais de direito autoral, marcas, patentes, modelos e desenhos industriais.</p>\r\n\r\n<p>Ao acessar o aplicativo, os usu&aacute;rios declaram que ir&atilde;o respeitar todos os direitos de propriedade intelectual e os decorrentes da prote&ccedil;&atilde;o de marcas, patentes e/ou desenhos industriais, depositados ou registrados em, bem como todos os direitos referentes a terceiros que porventura estejam, ou estiverem de alguma forma, dispon&iacute;veis no servi&ccedil;o. O simples acesso ao servi&ccedil;o n&atilde;o confere aos usu&aacute;rios qualquer direito ao uso dos nomes, t&iacute;tulos, palavras, frases, marcas, patentes, imagens, dados e informa&ccedil;&otilde;es, dentre outras, que nele estejam ou estiverem dispon&iacute;veis.</p>\r\n\r\n<p>A reprodu&ccedil;&atilde;o de conte&uacute;do descritos anteriormente est&aacute; proibida, salvo com pr&eacute;via autoriza&ccedil;&atilde;o por escrito ou caso se destinem ao uso exclusivamente pessoal e sem que em nenhuma circunst&acirc;ncia os usu&aacute;rios adquiram qualquer direito sobre esses conte&uacute;dos.</p>\r\n\r\n<p>&Eacute; vedada a utiliza&ccedil;&atilde;o do servi&ccedil;o para finalidades comerciais, publicit&aacute;rias ou qualquer outra que contrarie a finalidade para a qual foi concebido, conforme definido neste documento, sob pena de sujei&ccedil;&atilde;o &agrave;s san&ccedil;&otilde;es cab&iacute;veis na Lei n&ordm; 9.610/1998, que protege os direitos autorais no Brasil.</p>\r\n\r\n<p>Os visitantes e usu&aacute;rios assumem toda e qualquer responsabilidade, de car&aacute;ter civil e/ou criminal, pela utiliza&ccedil;&atilde;o indevida das informa&ccedil;&otilde;es, textos, gr&aacute;ficos, marcas, imagens, enfim, todo e qualquer direito de propriedade intelectual ou industrial do servi&ccedil;o.</p>\r\n\r\n<p>Em nenhuma hip&oacute;tese, a Imprensa Nacional ser&aacute; respons&aacute;vel pela instala&ccedil;&atilde;o no equipamento do Usu&aacute;rio ou de terceiros, de c&oacute;digos maliciosos (v&iacute;rus, trojans, malware, worm, bot, backdoor, spyware, rootkit, ou de quaisquer outros que venham a ser criados), em decorr&ecirc;ncia da navega&ccedil;&atilde;o na Internet pelo usu&aacute;rio.</p>\r\n\r\n<h4>7. Quais s&atilde;o as responsabilidades da Imprensa Nacional?</h4>\r\n\r\n<p>Publicar e informar ao Usu&aacute;rio as futuras altera&ccedil;&otilde;es a estes Termos de Uso e Pol&iacute;tica de Privacidade conforme o princ&iacute;pio da publicidade estabelecido no artigo 37, caput, da Constitui&ccedil;&atilde;o Federal.</p>\r\n\r\n<p>Em nenhuma hip&oacute;tese, a Imprensa Nacional ser&aacute; respons&aacute;vel pela instala&ccedil;&atilde;o no equipamento do Usu&aacute;rio ou de terceiros, de c&oacute;digos maliciosos (v&iacute;rus, trojans, malware, worm, bot, backdoor, spyware, rootkit, ou de quaisquer outros que venham a ser criados), em decorr&ecirc;ncia da navega&ccedil;&atilde;o na Internet pelo usu&aacute;rio.</p>\r\n\r\n<p>Em hip&oacute;tese alguma, o servi&ccedil;o e seus colaboradores responsabilizam-se por eventuais danos diretos, indiretos, emergentes, especiais, imprevistos ou multas causadas, em qualquer mat&eacute;ria de responsabilidade, seja contratual, objetiva ou civil (inclusive neglig&ecirc;ncia ou outras), decorrentes de qualquer forma de uso do servi&ccedil;o, mesmo que advertida a possibilidade de tais danos.</p>\r\n\r\n<p>O usu&aacute;rio concorda que n&atilde;o usar&aacute; rob&ocirc;s, sistemas de varredura e armazenamento de dados (como &ldquo;spiders&rdquo; ou &ldquo;scrapers&rdquo;), links escondidos ou qualquer outro recurso escuso, ferramenta, programa, algoritmo ou m&eacute;todo coletor/extrator de dados autom&aacute;tico para acessar, adquirir, copiar ou monitorar o servi&ccedil;o, sem permiss&atilde;o expressa por escrito do &oacute;rg&atilde;o.</p>\r\n\r\n<p>Em se tratando de aplicativos em dispositivos m&oacute;veis sua comercializa&ccedil;&atilde;o &eacute; expressamente proibida. Ao concordar com este Termo de Uso e utilizar o aplicativo m&oacute;vel, o usu&aacute;rio receber&aacute; uma permiss&atilde;o do &oacute;rg&atilde;o para uso n&atilde;o comercial dos servi&ccedil;os oferecidos pelo aplicativo, o que, em nenhuma hip&oacute;tese, far&aacute; dele propriet&aacute;rio do aplicativo m&oacute;vel.</p>\r\n\r\n<p>Caso o usu&aacute;rio descumpra o Termo de Uso ou a Pol&iacute;tica de Privacidade, ou seja investigado em raz&atilde;o de m&aacute; conduta, este dever&aacute; responder legalmente por essa conduta.</p>\r\n\r\n<p>A Imprensa Nacional poder&aacute;, quanto &agrave;s ordens judiciais de pedido de informa&ccedil;&otilde;es, compartilhar informa&ccedil;&otilde;es necess&aacute;rias para investiga&ccedil;&otilde;es ou tomar medidas relacionadas a atividades ilegais, suspeitas de fraude ou amea&ccedil;as potenciais contra pessoas, bens ou sistemas que sustentam o servi&ccedil;o ou de outra forma necess&aacute;ria para cumprir com as obriga&ccedil;&otilde;es legais.</p>\r\n\r\n<p>A Imprensa Nacional se compromete a preservar a funcionalidade do servi&ccedil;o ou aplicativo, utilizando um layout que respeite a usabilidade e navegabilidade, facilitando a navega&ccedil;&atilde;o sempre que poss&iacute;vel, e exibir as funcionalidades de maneira completa, precisa e suficiente, de modo que as opera&ccedil;&otilde;es realizadas no servi&ccedil;o sejam claras.</p>\r\n\r\n<h4>8. Qual o contato pelo qual o usu&aacute;rio do servi&ccedil;o pode tirar suas d&uacute;vidas?</h4>\r\n\r\n<p>Caso o usu&aacute;rio tenha alguma d&uacute;vida sobre este Termo de Uso, ele poder&aacute; entrar em contato pelo e-mail&nbsp;<a href='mailto:caci@in.gov.br.>caci@in.gov.br.</a></p>", 1, null, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ClientId",
                table: "AspNetUsers",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_ProfessionalClient_ClientId",
                table: "TB_ProfessionalClient",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ProfessionalClient_ProfessionalProfileId",
                table: "TB_ProfessionalClient",
                column: "ProfessionalProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ProfessionalEspeciality_IdProfessionalProfile",
                table: "TB_ProfessionalEspeciality",
                column: "IdProfessionalProfile");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ProfessionalEspeciality_IdWorker",
                table: "TB_ProfessionalEspeciality",
                column: "IdWorker");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ProfessionalPayment_ProfessionalId",
                table: "TB_ProfessionalPayment",
                column: "ProfessionalId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ProfessionalProfile_AddressId",
                table: "TB_ProfessionalProfile",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ProfessionalProfile_ClientId",
                table: "TB_ProfessionalProfile",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ProfessionalProfile_ProfessionalAreaId",
                table: "TB_ProfessionalProfile",
                column: "ProfessionalAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ThreeAvaliation_ProfessionalProfileId",
                table: "TB_ThreeAvaliation",
                column: "ProfessionalProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Worker_AddressId",
                table: "TB_Worker",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Worker_ClientId",
                table: "TB_Worker",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Worker_ProfessionalAreaId",
                table: "TB_Worker",
                column: "ProfessionalAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Worker_ProfessionalProfileConcludedId",
                table: "TB_Worker",
                column: "ProfessionalProfileConcludedId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_WorkerDone_WorkerDoneProfessionalId",
                table: "TB_WorkerDone",
                column: "WorkerDoneProfessionalId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_WorkerDoneProfessional_ProfessionalProfileId",
                table: "TB_WorkerDoneProfessional",
                column: "ProfessionalProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_WorkerProfessional_ProfessionalProfileId",
                table: "TB_WorkerProfessional",
                column: "ProfessionalProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_WorkerProfessional_WorkerId",
                table: "TB_WorkerProfessional",
                column: "WorkerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "TB_ProfessionalClient");

            migrationBuilder.DropTable(
                name: "TB_ProfessionalEspeciality");

            migrationBuilder.DropTable(
                name: "TB_ProfessionalPayment");

            migrationBuilder.DropTable(
                name: "TB_Template");

            migrationBuilder.DropTable(
                name: "TB_TermUse");

            migrationBuilder.DropTable(
                name: "TB_ThreeAvaliation");

            migrationBuilder.DropTable(
                name: "TB_WorkerDone");

            migrationBuilder.DropTable(
                name: "TB_WorkerProfessional");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "TB_WorkerDoneProfessional");

            migrationBuilder.DropTable(
                name: "TB_Worker");

            migrationBuilder.DropTable(
                name: "TB_ProfessionalProfile");

            migrationBuilder.DropTable(
                name: "TB_Address");

            migrationBuilder.DropTable(
                name: "TB_Client");

            migrationBuilder.DropTable(
                name: "TB_ProfessionalArea");
        }
    }
}
