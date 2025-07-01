using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Bico.Infra.Dapper.Repository
{

    public class DapperProfessionalClientRepository : IDapperProfessionalClientRepository
    {
        private readonly IConfiguration _configuration;
        private NpgsqlConnection connection;

        public DapperProfessionalClientRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
        }

        public async Task<List<ProfessionalClient>> GetMyProfessionalClient(Guid clientId)
        {
            try
            {
                var query = @"
                                SELECT 
                                    pc.""Id"", 
                                    pc.""StatusWorker"", 
                                    pc.""Created"",

                                    pp.""Id"",
                                    pp.""Name"", 
                                    pp.""LastName"", 
                                    pp.""Phone"", 
                                    pp.""Perfil"", 
                                    pp.""Profession"", 
                                    pp.""Ativo"", 
                                    pp.""ClientId"", 

                                    addr.""Id"", 
                                    addr.""ZipCode"", 
                                    addr.""City"", 
                                    addr.""State"",

                                    pe.""Id"", 
                                    pe.""Description""
        
                                FROM ""TB_ProfessionalClient"" pc
                                INNER JOIN ""TB_ProfessionalProfile"" pp ON pc.""ProfessionalProfileId"" = pp.""Id""
                                LEFT JOIN ""TB_Address"" addr ON pp.""AddressId"" = addr.""Id""
                                LEFT JOIN ""TB_ProfessionalEspeciality"" pe ON pp.""Id"" = pe.""IdProfessionalProfile""
                                WHERE pc.""ClientId"" = @ClientId";

                var professionalClients = new Dictionary<Guid, ProfessionalClient>();

                var result = await connection.QueryAsync<ProfessionalClient,
                    ProfessionalProfile, Address, ProfessionalEspeciality,
                    ProfessionalClient>(
                    query,
                    (pc, pp, addr, pe) =>
                    {
                        if (!professionalClients.TryGetValue(pc.Id, out var professionalClient))
                        {
                            professionalClient = pc;
                            professionalClient.ProfessionalProfile = pp;
                            professionalClient.ProfessionalProfile.Especiality = new HashSet<ProfessionalEspeciality>();
                            professionalClient.ProfessionalProfile.Address = addr;

                            professionalClients.Add(pc.Id, professionalClient);
                        }

                        if (pe != null && !professionalClient.ProfessionalProfile.Especiality.Any(e => e.Id == pe.Id))
                        {
                            professionalClient.ProfessionalProfile.Especiality.Add(pe);
                        }

                        return professionalClient;
                    },
                    new { ClientId = clientId },
                    splitOn: "Id,Id,Id, Id"
                );

                return professionalClients.Values.ToList();

            }
            catch(Exception e)
            {
                return null;
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
