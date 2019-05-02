using GTL.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace GTL.Application.Interfaces.UnitOfWork
{
    public interface IGTLContext
    {
        //IMemberRepository Members { get; }

        IDbCommand CreateCommand();

        IUnitOfWork CreateUnitOfWork();
    }
}
