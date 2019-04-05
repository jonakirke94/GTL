using GTL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GTL.Application.Interfaces.Repositories
{
    public interface IBookRepository
    {
        Task<ICollection<Book>> GetBooksAsync();

        Task<ICollection<Book>> GetBooksByUserId(int userId);

        Task DeleteBookAsync(int id);
    }
}

