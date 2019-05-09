using System;
using System.IO;
using Dapper;
using GTL.Application.Interfaces.UnitOfWork;
using GTL.Web;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace IntegrationTests
{
    public class IntegrationBase : IDisposable
    {
        private readonly IGTLContext _context;

        protected IMediator _mediator;

        public IntegrationBase()
        {
            var server = new TestServer(new WebHostBuilder()
                .ConfigureAppConfiguration(config =>
                    config.AddJsonFile("appsettings.test.json", optional: true, reloadOnChange: true))
                .UseStartup<Startup>());

            _mediator = server.Host.Services.GetRequiredService<IMediator>();

            _context = server.Host.Services.GetRequiredService<IGTLContext>();
        }

        public void Dispose()
        {
            ResetDatabase();
        }

        public void ResetDatabase()
        {
            RunScript("ResetDatabase");        
        }

        public void SeedDatabase()
        {
            RunScript("SeedDatabase");
        }

        public void RunScript(string fileName)
        {
            var script = File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "/" + fileName + ".sql");

            using (var cmd = _context.CreateCommand())
            {
                foreach (var sqlBatch in script.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    cmd.Connection.Execute(sqlBatch);
                }
            }
        }
    }
}
