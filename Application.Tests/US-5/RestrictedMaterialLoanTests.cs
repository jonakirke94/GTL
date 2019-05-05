using System;
using System.Collections.Generic;
using System.Text;
using Application.Tests.Infrastructure;
using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Entities;
using GTL.Domain.Enums;
using Xunit;

namespace Application.Tests
{
    public class RestrictedMaterialLoanTests
    {
        private readonly IMemberRepository _memberRepo;

        public RestrictedMaterialLoanTests(IMemberRepository memberRepo)
        {
            _memberRepo = memberRepo;
        }

        [Fact]
        [AutoRollback]
        public void InsertTest()
        {
            var member = new Member
            {
                Ssn = "1234567891",
                Email = "fakemail@fake.dk",
                Type = MemberType.PROFESSOR,
                Name = "FAKETESTNAME"
            };

            _memberRepo.Add(member);

            Assert.True(true);
        }

    }
}
