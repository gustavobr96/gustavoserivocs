using Dapper;
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

        public UserDapperRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task AtualizarTokenPhone(Guid id, string tokenPhone)
        {
            try
            {
                await using var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                await connection.OpenAsync();

                string commandText = @"UPDATE ""TB_Client"" SET ""TokenPhone"" = @TokenPhone WHERE ""Id"" = @Id";
                await connection.ExecuteAsync(commandText, new { Id = id, TokenPhone = tokenPhone });
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
