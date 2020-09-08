namespace Repository.Interfaces
{
    using System.Threading.Tasks;

    /// <summary>
    /// Contains generic methods to use in all the repository.
    /// </summary>
    public interface IAppRepository
    {
        /// <summary>
        /// Save the changes done to one or more entities.
        /// </summary>
        Task<bool> SaveChangesAsync();

        /// <summary>
        /// Add a new object to the database.
        /// </summary>
        void Add<T>(T entity) where T : class;

        /// <summary>
        /// Delete an object from the database.
        /// </summary>
        void Delete<T>(T entity) where T : class;
    }
}