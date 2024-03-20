using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Sistema.Bico.Domain.Interface;
using System.Threading.Tasks;

namespace Sistema.Bico.Infra.Dapper.Repository
{
    public class PlanDapperRepository : IPlanDapperRepository
    {
        private readonly IConfiguration _configuration;
        private SqlConnection connection;

        public PlanDapperRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
        }

        public async Task DeletePlansVenciment()
        {
            string commandText = @"DELETE FROM [TB_ProfessionalPayment] WHERE [Created] <= DATEADD(day, -31, GETDATE())";
            await connection.ExecuteAsync(commandText);
        }
    }
}
