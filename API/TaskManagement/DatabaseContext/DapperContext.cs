using Microsoft.Data.SqlClient;
using System.Data;

namespace TaskManagement.DatabaseContext
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            // Connection string aapke appsettings.json mein hogi
            _connectionString = _configuration.GetConnectionString("Conn");
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
