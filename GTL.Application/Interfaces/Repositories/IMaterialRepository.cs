using GTL.Domain.Entities;

namespace GTL.Application.Interfaces.Repositories
{
    public interface IMaterialRepository
    {
        void Add(string isbn, string title, string description, int edition);
        void Update(string isbn, string title, string description, int edition);
        Material GetByIsbn(string isbn);
        Material GetById(int id);
    }
}