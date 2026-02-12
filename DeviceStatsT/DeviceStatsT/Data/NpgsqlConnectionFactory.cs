using System.Data;
using Npgsql;
using DeviceStats.Data;
using Microsoft.Extensions.Configuration;
using System.Data.Common;

namespace DeviceStatsT.Data
{
    public class NpgsqlConnectionFactory: IDbConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public NpgsqlConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbConnection Create()
        {
            var conString = _configuration.GetConnectionString("DevicestatsDb");
            if (string.IsNullOrWhiteSpace(conString))
                throw new InvalidOperationException("Connection string 'DevicestatsDb' not found.");

            return new NpgsqlConnection(conString);
        }
        
    }
}

