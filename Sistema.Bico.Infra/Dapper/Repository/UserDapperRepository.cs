using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Sistema.Bico.Domain.Interface;
using System;
using System.Threading.Tasks;

namespace Sistema.Bico.Infra.Dapper.Repository
{
    public class UserDapperRepository : IUserDapperRepository
    {
        private readonly IConfiguration _configuration;
        private NpgsqlConnection connection;

        public UserDapperRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
        }

        public async Task AtualizarTokenPhone(Guid id, string tokenPhone)
        {
            try {

                string commandText = @"UPDATE ""TB_Client"" SET ""TokenPhone"" = @TokenPhone WHERE ""Id"" = @Id";
                await connection.ExecuteAsync(commandText, new { Id = id, TokenPhone = tokenPhone });
                connection.Close();

            } catch(Exception e)
            {

            }
    
        }
    }
}
