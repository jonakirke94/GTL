using GTL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Application.Interfaces.Repositories
{
    public interface IMemberRepository
    {
        Member GetMemberBySsn(string ssn);
    }
}

