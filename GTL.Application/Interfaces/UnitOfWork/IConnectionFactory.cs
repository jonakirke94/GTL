using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace GTL.Application.Interfaces.UnitOfWork
{
    public interface IConnectionFactory
    {
        IDbConnection Create();
    }
}
