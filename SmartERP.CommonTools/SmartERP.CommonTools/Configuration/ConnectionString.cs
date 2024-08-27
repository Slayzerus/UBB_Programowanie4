using Microsoft.Extensions.Configuration;
using SmartERP.CommonTools.Enums;
using SmartERP.CommonTools.Extensions;

namespace SmartERP.CommonTools.Configuration
{
    public class ConnectionString
    {
        private string? host;

        private string? port;

        private string? db;

        private string? user;

        private string? pass;

        private DatabaseType dbType = DatabaseType.Unknown;

        private readonly string _connectionStringName;

        private readonly IConfiguration _configuration;

        public ConnectionString(IConfiguration configuration, string connectionStringName = "DefaultConnection")
        {
            _configuration = configuration;
            _connectionStringName = connectionStringName;
            ReadEnvironmentVariables();
        }

        public DatabaseType DbType => dbType;

        private void ReadEnvironmentVariables()
        {
            string dbTypeStr = Environment.GetEnvironmentVariable("DBTYPE") ?? string.Empty;
            if (!string.IsNullOrEmpty(dbTypeStr))
            {
                dbType = dbTypeStr.GetValueFromDescription<DatabaseType>();
            }
            else
            {
                dbType = DatabaseType.Unknown;
            }

            host = Environment.GetEnvironmentVariable("DBHOST");
            port = Environment.GetEnvironmentVariable("DBPORT");

            if (dbType == DatabaseType.Unknown)
            {
                if (ReadPostgreVariables())
                {
                    dbType = DatabaseType.PostgreSQL;
                }
                else if (ReadSqlServerVariables())
                {
                    dbType = DatabaseType.SQLServer;
                }
            }
            else if (dbType == DatabaseType.PostgreSQL)
            {
                ReadPostgreVariables();
            }
            else if (dbType == DatabaseType.SQLServer)
            {
                ReadSqlServerVariables();
            }
        }

        private bool ReadSqlServerVariables()
        {
            db = Environment.GetEnvironmentVariable("DBNAME");
            user = Environment.GetEnvironmentVariable("DBUSER");
            pass = Environment.GetEnvironmentVariable("DBPASS");
            return !string.IsNullOrEmpty(db) && !string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(pass);
        }

        private bool ReadPostgreVariables()
        {
            db = Environment.GetEnvironmentVariable("POSTGRES_DB");
            user = Environment.GetEnvironmentVariable("POSTGRES_USER");
            if (string.IsNullOrEmpty(user))
            {
                user = "root";
            }
            pass = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
            return !string.IsNullOrEmpty(db) && !string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(pass);
        }

        private string GenerateFromParameters()
        {
            if (dbType == DatabaseType.SQLServer)
            {
                host += !string.IsNullOrEmpty(port) ? $"{host}, {port}" : "";
                return $"Server={host};Database={db};Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=false";
            }
            else if (dbType == DatabaseType.PostgreSQL)
            {
                return $"Host={host}; User ID={user};Password={pass};Port={port};Database={db};Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;";
            }

            return string.Empty;
        }

        public string Get()
        {
            ReadEnvironmentVariables();

            if (!string.IsNullOrEmpty(host) && !string.IsNullOrEmpty(db) && !string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(pass))
            {
                return GenerateFromParameters();
            }

            dbType = DatabaseType.SQLServer;
            return _configuration.GetConnectionString(_connectionStringName) ?? string.Empty;
        }
    }
}
