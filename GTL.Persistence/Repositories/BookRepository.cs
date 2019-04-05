using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTL.Persistence.Repositories
{
    public class BookRepository : IBookRepository
    {
        public Task DeleteBookAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Book>> GetBooksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Book>> GetBooksByUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
