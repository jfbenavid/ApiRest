namespace Repository.Interfaces
{
    using System.Threading.Tasks;

    public interface IAppRepository
    {
        Task<bool> SaveChangesAsync();

        void Add<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;
    }
}