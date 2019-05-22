using System;
using System.IO;
using Dapper;
using GTL.Application.Interfaces.UnitOfWork;
using GTL.Domain.Entities;
using GTL.Persistence.Configurations;
using GTL.Web;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TestDatabaseManager;

namespace IntegrationTests
{
    public class IntegrationBase : IDisposable
    {
        protected readonly IGTLContext _context;

        protected IMediator _mediator;

        public IntegrationBase()
        {
            var server = new TestServer(new WebHostBuilder()
                .ConfigureAppConfiguration(config =>
                    config.AddJsonFile("appsettings.test.json", optional: true, reloadOnChange: true))
                .UseStartup<Startup>());

            _mediator = server.Host.Services.GetRequiredService<IMediator>();

            _context = server.Host.Services.GetRequiredService<IGTLContext>();

            ScriptRunner.ResetDatabase();
        }

        public void Dispose()
        {
            ScriptRunner.ResetDatabase();
        }

    }
}
