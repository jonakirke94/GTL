using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTL.Application.Interfaces
{
    public interface IAdoContext : IDisposable
    {
        void SaveChanges();

        IDbCommand CreateCommand();
    }
}
