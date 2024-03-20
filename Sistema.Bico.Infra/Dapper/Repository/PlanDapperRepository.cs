using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Sistema.Bico.Domain.Interface;
using System.Threading.Tasks;

namespace Sistema.Bico.Infra.Dapper.Repository
{
    public class PlanDapperRepositoryPostgress /*: IPlanDapperRepository*/
    {
        private readonly IConfiguration _configuration;
        private NpgsqlConnection connection;

        public PlanDapperRepositoryPostgress(IConfiguration configuration)
        {
            _configuration = configuration;
            connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
        }

        public async Task DeletePlansVencimentPostgress()
        {
            string commandText = @"DELETE FROM ""TB_ProfessionalPayment"" WHERE ""Created"" + interval '31' day  <= Now()";
            await connection.ExecuteAsync(commandText);
        }
    }
}
