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
    public static class ScriptRunner
    {
        private static readonly string connectionString = Settings.Default.GTL_TEST;

        public static void ResetDatabase()
        {
            ClearDatabase();
            SeedDatabase();
        }

        private static void ClearDatabase()
        {
            RunScript("ClearDatabase");
        }

        private static void SeedDatabase()
        {
            RunScript("SeedDatabase");
        }

        private static void RunScript(string fileName)
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
