using Microsoft.Data.SqlClient;
using System.Data;

namespace Hpss.Data
{
    public class DapperDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionstring = null;

        public DapperDbContext(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connectionstring = _configuration?.GetConnectionString("sqlserver");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionstring);
        }
    }

