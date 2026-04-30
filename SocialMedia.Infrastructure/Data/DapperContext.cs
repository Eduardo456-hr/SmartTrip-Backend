using Microsoft.Extensions.Configuration;
using MySqlConnector;
using SocialMedia.Core.Interfaces;
using System.Data;

namespace SocialMedia.Infrastructure.Data
{
    public class DapperContext : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionMySql")
                ?? throw new Exception("No se encontró la cadena de conexión ConnectionMySql.");
        }

        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}