using GTL.Application.Users.Queries.GetUserList;
using GTL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTL.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();

        User GetUser(int ind);

        int AddUser(User user);

        void DeleteUser(int id);

        void Update(User user);
    }
}

