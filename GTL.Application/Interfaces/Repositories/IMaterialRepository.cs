namespace GTL.Application.Interfaces.Repositories
{
    public interface IMaterialRepository
    {
        void CreateMaterial(string isbn, string title, string description, int edition);
    }
}