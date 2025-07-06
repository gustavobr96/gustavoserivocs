using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Npgsql;
using Sistema.Bico.Domain.Command.Filters;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Domain.Response;
using Sistema.Bico.Domain.UseCases.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sistema.Bico.Infra.Dapper.Repository
{

    public class DapperProfessionalClientRepository : IDapperProfessionalClientRepository
    {
        private readonly IConfiguration _configuration;
        private NpgsqlConnection connection;
        private readonly ILogger<DapperProfessionalClientRepository> _logger;

        public DapperProfessionalClientRepository(IConfiguration configuration,
             ILogger<DapperProfessionalClientRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
            connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
        }

        public async Task<(int Total, List<ProfessionalProfile> List)> GetProfessionalPaginationWithSlapper(FilterProfessionalCommand filter)
        {
            try
            {

                // Slapper config
                Slapper.AutoMapper.Cache.ClearInstanceCache();


                Slapper.AutoMapper.Configuration.AddIdentifiers(typeof(ProfessionalProfile), new[] { "Id" });
                Slapper.AutoMapper.Configuration.AddIdentifiers(typeof(Address), new[] { "Id" });
                Slapper.AutoMapper.Configuration.AddIdentifiers(typeof(ProfessionalArea), new[] { "Id" });
                Slapper.AutoMapper.Configuration.AddIdentifiers(typeof(ProfessionalEspeciality), new[] { "Id" });


                var offset = (filter.Page - 1) * filter.Take;

                var sql = @"
                        SELECT 
                            pp.""Id"" as ""Id"",
                            pp.""Name"" as ""Name"",
                            pp.""LastName"" as ""LastName"",
                            pp.""Phone"" as ""Phone"",
                            pp.""Perfil"" as ""Perfil"",
                            pp.""Profession"" as ""Profession"",
                            pp.""Avaliation"" as ""Avaliation"",
                            pp.""Ativo"" as ""Ativo"",
                            pp.""Premium"" as ""Premium"",
                            pp.""ClientId"" as ""ClientId"",
                            pp.""About"" as ""About"",
                            pp.""PerfilPicture"" as ""PerfilPicture"",
                            pp.""ProfessionalAreaId"" as ""ProfessionalAreaId"",
                            pp.""AddressId"" as ""AddressId"",

                            pa.""Id"" as ""ProfessionalArea_Id"",
                            pa.""Codigo"" as ""ProfessionalArea_Codigo"",
                            pa.""Description"" as ""ProfessionalArea_Description"",

                            a.""Id"" as ""Address_Id"",
                            a.""City"" as ""Address_City"",
                            a.""State"" as ""Address_State"",
                            a.""Bairro"" as ""Address_Bairro"",
                            a.""Number"" as ""Address_Number"",
                            a.""Logradouro"" as ""Address_Logradouro"",
                            a.""Complemento"" as ""Address_Complemento"",
                            a.""ZipCode"" as ""Address_ZipCode"",

                            e.""Id"" as ""Especiality_Id"",
                            e.""Description"" as ""Especiality_Description"",
                            e.""IdProfessionalProfile"" as ""Especiality_IdProfessionalProfile""

                        FROM ""TB_ProfessionalProfile"" pp
                        INNER JOIN ""TB_Address"" a ON a.""Id"" = pp.""AddressId""
                        LEFT JOIN ""TB_ProfessionalArea"" pa ON pa.""Id"" = pp.""ProfessionalAreaId""
                        LEFT JOIN ""TB_ProfessionalEspeciality"" e ON e.""IdProfessionalProfile"" = pp.""Id""

                        WHERE 
                            (
                                @City IS NULL OR TRIM(@City) = '' OR LOWER(a.""City"") LIKE LOWER('%' || @City || '%')
                            )
                            AND (
                                @Area IS NULL OR pa.""Codigo"" = @Area
                            )
                            AND (
                                @Profession IS NULL OR TRIM(@Profession) = '' OR LOWER(pp.""Profession"") LIKE LOWER('%' || @Profession || '%')
                            )
                            AND pp.""ClientId"" != @ClientId
                            AND pp.""Ativo"" = true

                        ORDER BY pp.""Name""
                        OFFSET @Offset ROWS FETCH NEXT @Take ROWS ONLY;
                    ";

                var countSql = @"
                    SELECT COUNT(DISTINCT pp.""Id"")
                    FROM ""TB_ProfessionalProfile"" pp
                    LEFT JOIN ""TB_Address"" a ON a.""Id"" = pp.""AddressId""
                    LEFT JOIN ""TB_ProfessionalArea"" pa ON pa.""Id"" = pp.""ProfessionalAreaId""
                    WHERE 
                        (
                            @City IS NULL OR TRIM(@City) = '' OR LOWER(a.""City"") LIKE LOWER('%' || @City || '%')
                        )
                        AND (
                            @Area IS NULL OR @Area = 0 OR pa.""Codigo"" = @Area
                        )
                        AND (
                            @Profession IS NULL OR TRIM(@Profession) = '' OR LOWER(pp.""Profession"") LIKE LOWER('%' || @Profession || '%')
                        )
                        AND pp.""ClientId"" != @ClientId
                        AND pp.""Ativo"" = true;
                ";

                var parameters = new
                {
                    City = filter.City,
                    Area = filter.Area,
                    Profession = filter.Profession,
                    ClientId = filter.ClientId,
                    Offset = offset,
                    Take = filter.Take
                };

                await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                await conn.OpenAsync();

                var total = await conn.ExecuteScalarAsync<int>(countSql, parameters);
                var rows = await conn.QueryAsync<dynamic>(sql, parameters);



                var mapped = Slapper.AutoMapper.MapDynamic<ProfessionalProfile>(rows).ToList();

                return (total, mapped);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro em GetProfessionalPaginationWithSlapper");
                throw; // relança para não esconder erro
            }
        }


        public async Task AtualizarStatus(Guid id, StatusWorker status)
        {
            try
            {
                string commandText = @"UPDATE ""TB_ProfessionalClient"" SET ""StatusWorker"" = @StatusWorker WHERE ""Id"" = @Id";
                await connection.ExecuteAsync(commandText, new { Id = id, StatusWorker = status });
                connection.Close();

            }
            catch (Exception e)
            {

            }

        }
    }
}
