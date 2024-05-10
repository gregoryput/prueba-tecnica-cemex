using Microsoft.Data.SqlClient;

namespace Api_7._0.Repositories
{
    public class ConexionDB
    {
        public SqlConnection GetConnection(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Db");
            var connection = new SqlConnection(connectionString);
            return connection;
        }

    }
}
