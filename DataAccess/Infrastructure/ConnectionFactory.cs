using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.Common;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace DataAccess.Infrastructure
{
    public class ConnectionFactory : IConnectionFactory
    {
        private string connectionString;

        private readonly IConfiguration configuration;
        public ConnectionFactory(IConfiguration _configuration)
        {
            configuration = _configuration;
            connectionString = configuration.GetConnectionString("DefaultConnection");
            DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
        }
        public IDbConnection GetConnection
        {
            get
            {
                var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                var conn = factory.CreateConnection();
                conn.ConnectionString = connectionString;
                conn.Open();
                return conn;
            }
        }
    }
}
