using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using GTL.Persistence.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Dapper;

namespace Application.Tests.US_5.Setup
{
    public class DatabaseFixture : IDisposable
    {

        public SqlConnection Db { get; private set; }

        public SqlCommand Cmd { get; private set; }


        public DatabaseFixture()
        {
            var conn = GetConnectionString();
            Db = new SqlConnection(connectionString: conn);



            Db.Open();

            Cmd = Db.CreateCommand();

            // ... initialize data in the test database ...
        }

        public void Dispose()
        {
            var delMembers = "DELETE FROM MEMBER";
            Cmd.Connection.Execute(delMembers);
            


            Cmd.Dispose();
            Db.Close();
        }

        private static string GetConnectionString()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("testconfig.json")
                .Build();

            return config["ConnectionStrings:TestConnection"];
        }

    }
}
