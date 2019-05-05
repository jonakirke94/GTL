using System;
using System.Collections.Generic;
using System.Text;
using Application.Tests.US_5.Setup;
using Dapper;
using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Entities;
using GTL.Domain.Enums;
using Xunit;

namespace Application.Tests
{
    public class RestrictedMaterialLoanTests : DatabaseFixture
    {

        [Fact]
        public void InsertTest()
        {
                var member =
                    $@"INSERT INTO [Member] ([Ssn], [Name], [Email], [Type]) VALUES('1234567891', 'FAKE', 'fake@fake.dk', 'PROFESSOR')";
                Cmd.Connection.Execute(member);
            


            Assert.True(true);
        }

    }
}
