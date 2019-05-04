using GTL.Domain.Entities;

namespace GTL.Application.Interfaces.Repositories
{
    public interface IMaterialRepository
    {
        void Add(Material material);
        Material GetByIsbn(string isbn);
        Material GetById(int id);
        Material GetByTitle(string title);
        void Update(Material material);
    }
}