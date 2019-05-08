using GTL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GTL.Application.Interfaces.Repositories
{
    public interface IBookRepository
    {
        Task<ICollection<Material>> GetBooksAsync();

        Task<ICollection<Material>> GetBooksByUserId(int userId);

        Task DeleteBookAsync(int id);
    }
}

