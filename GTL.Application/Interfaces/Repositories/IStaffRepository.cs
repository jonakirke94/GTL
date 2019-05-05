using System;
using System.Collections.Generic;
using System.Text;
using GTL.Domain.Entities;

namespace GTL.Application.Interfaces.Repositories
{
    public interface IStaffRepository
    {
        Staff GetById(int id);

        Staff GetByEmail(string email);
    }
}
