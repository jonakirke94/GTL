using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.UseCases.Login.Commands;
using Xunit;

namespace IntegrationTests
{
    public class MediatorIntegrationTests : IntegrationBase
    {
        public MediatorIntegrationTests()
        {
            SeedDatabase();
        }

        [Fact(DisplayName = "Test Login handler")]
        public async Task TD2_TC2()
        {
            // Arrange
            var command = new LoginCommand
            {
                Email = "thanos@gmail.com",
                Password = "12345678"
            };

            var response = await _mediator.Send(command);


            Assert.False(response);

            //var mediator = testServer.Host.Services.GetRequiredService<IMediator>();


            // Assert
        }

    }
}
