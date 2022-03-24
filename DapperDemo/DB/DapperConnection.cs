using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DapperDemo.DB
{
    public class DapperConnection
    {
        private readonly IConfiguration configuration;

        public DapperConnection(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
