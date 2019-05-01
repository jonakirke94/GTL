using GTL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using GTL.Application.UseCases.Members.Commands.CreateMember;

namespace GTL.Application.Interfaces.Repositories
{
    public interface IMemberRepository
    {
        Member GetMemberWithLoanerCards(string ssn);

        Member GetMemberBySsn(string ssn);

        void CreateMember(Member member);
    }
}

