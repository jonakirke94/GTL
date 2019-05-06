using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace GTL.IntegrationTests
{
    public static class DatabaseSetup
    {
        public static string GetConnectionString()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("testconfig.json")
                .Build();

            return config["ConnectionStrings:TestConnection"];
        }

        public static DbCommand GetReadyCommand()
        {
            var conn = GetConnectionString();
            var db = new SqlConnection(connectionString: conn);
            db.Open();

            return db.CreateCommand();
        }
    }
}
