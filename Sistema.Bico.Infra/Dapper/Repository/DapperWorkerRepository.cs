using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using Sistema.Bico.Domain.Command.Filters;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Domain.Response;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Bico.Infra.Dapper.Repository
{
    public class DapperWorkerRepository : IDapperWorkerRepository
    {
        private readonly IConfiguration _configuration;
        private NpgsqlConnection connection;
        private readonly ILogger<DapperWorkerRepository> _logger;

        public DapperWorkerRepository(IConfiguration configuration,
         ILogger<DapperWorkerRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<WorkerPaginationResponse> GetMyPublishWorkerClient(FilterWorkerCommand filter)
        {
            try
            {
                Slapper.AutoMapper.Cache.ClearInstanceCache();
                Slapper.AutoMapper.Configuration.AddIdentifiers(typeof(WorkerResponse), new[] { "Id" });


                var sqlCount = @"
                    SELECT COUNT(*)
                    FROM ""TB_Worker"" w
                    LEFT JOIN ""TB_Address"" a ON a.""Id"" = w.""AddressId""
                    LEFT JOIN ""TB_ProfessionalArea"" pa ON pa.""Id"" = w.""ProfessionalAreaId""
                    WHERE (@City IS NULL OR a.""City"" ILIKE '%' || @City || '%')
                      AND (@Area IS NULL OR @Area = 0 OR pa.""Codigo"" = @Area)
                      AND (@Profession IS NULL OR w.""Profession"" ILIKE '%' || @Profession || '%')
                      AND w.""ClientId"" = @ClientId;";

                var sqlData = @"
                    SELECT
                      w.""Id"" as ""Id"",
                      to_char(w.""Created"", 'DD/MM/YYYY') AS ""Created"",
                      w.""Price"" as ""Price"",
                      w.""Title"" as ""Titulo"",
                      w.""Phone"" as ""Phone"",
                      pa.""Codigo"" as ""Area"",
                      pa.""Description"" as ""AreaName"",
                      w.""Remote"" as ""Remote"",
                      w.""About"" as ""Sobre"",
                      w.""Profession"" as ""Profession"",
                      a.""ZipCode"" as ""CEP"",
                      a.""City"" as ""City"",
                      a.""State"" as ""State"",
                      (
                        SELECT COUNT(*) 
                        FROM ""TB_WorkerProfessional"" wp 
                        WHERE wp.""WorkerId"" = w.""Id""
                      ) AS ""Interessados""
                    FROM ""TB_Worker"" w
                    LEFT JOIN ""TB_Address"" a ON a.""Id"" = w.""AddressId""
                    LEFT JOIN ""TB_ProfessionalArea"" pa ON pa.""Id"" = w.""ProfessionalAreaId""
                    WHERE (@City IS NULL OR a.""City"" ILIKE '%' || @City || '%')
                      AND (@Area IS NULL OR @Area = 0 OR pa.""Codigo"" = @Area)
                      AND (@Profession IS NULL OR w.""Profession"" ILIKE '%' || @Profession || '%')
                      AND w.""ClientId"" = @ClientId

                    
                    ORDER BY w.""Created"" DESC
                    LIMIT @Take OFFSET @Skip;";

                var parameters = new
                {
                    City = string.IsNullOrEmpty(filter.City) ? null : filter.City,
                    Area = filter.Area,
                    Profession = string.IsNullOrEmpty(filter.Profession) ? null : filter.Profession,
                    ClientId = filter.ClientId,
                    Take = filter.Take,
                    Skip = filter.Take * (filter.Page - 1)
                };

                await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                await conn.OpenAsync();

                var count = await conn.ExecuteScalarAsync<int>(sqlCount, parameters);

                var rows = await conn.QueryAsync<dynamic>(sqlData, parameters);

                var workers = Slapper.AutoMapper.MapDynamic<WorkerResponse>(rows);

                return new WorkerPaginationResponse { CountRegister = count, Worker = workers.ToList() };
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Erro em GetMyPublishWorkerClient");
                return null;
            }
        }
    }
}
