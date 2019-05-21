using System;
using System.Data.SqlClient;
using System.IO;
using Dapper;
using GTL.Application.Interfaces.UnitOfWork;
using GTL.Domain.Entities;
using GTL.Web;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace TestDatabaseManager
{
    public class ScriptRunner
    {
        private readonly string connectionString = "Data Source=gtl.cebo28xw992s.eu-west-1.rds.amazonaws.com,1433;Initial Catalog=GTL_TEST; User ID=master;Password=hejhej123;";

        public void ResetDatabase()
        {
            ClearDatabase();
            SeedDatabase();
        }

        private void ClearDatabase()
        {
            RunScript("ClearDatabase");
        }

        private void SeedDatabase()
        {
            RunScript("SeedDatabase");
        }

        private void RunScript(string fileName)
        {
            var script = File.ReadAllText(Directory.GetCurrentDirectory() + "/scripts/" + fileName + ".sql");

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                foreach (var sqlBatch in script.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    using (var cmd = new SqlCommand("", connection))
                    {
                        cmd.Connection.Execute(sqlBatch);
                    }
                }
                connection.Close();
            }
        }
    }
}
