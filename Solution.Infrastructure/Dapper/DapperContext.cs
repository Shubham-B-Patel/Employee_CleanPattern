
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Solution.Core.Interfaces.Dapper;
using System.Data;

namespace Solution.Infrastructure.Dapper
{
    public class DapperContext : IDapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ConnectionString")!;
        }

        public IDbConnection getDbConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
