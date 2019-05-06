using System;
using System.Data.SqlClient;
using Dapper;

namespace IntegrationTests
{
    public class DatabaseFixture : IDisposable
    {

        public SqlConnection Db { get; private set; }

        public SqlCommand Cmd { get; private set; }


        public DatabaseFixture()
        {
            var conn = DatabaseSetup.GetConnectionString();
            Db = new SqlConnection(connectionString: conn);
            Db.Open();
            Cmd = Db.CreateCommand();
        }

        public void Dispose()
        {
            const string clearQuery = "DELETE FROM LOAN; DELETE FROM MEMBER; DELETE FROM LIBRARY; DELETE FROM COPY; DELETE FROM MATERIAL";
            Cmd.Connection.Execute(clearQuery);

            Cmd.Dispose();
            Db.Close();
        }
    }
}
