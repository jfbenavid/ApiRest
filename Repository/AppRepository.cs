namespace Repository
{
    using System.Threading.Tasks;
    using Repository.Interfaces;

    /// <summary>
    /// Contains generic methods to use in all the repository.
    /// </summary>
    public abstract class AppRepository : IAppRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Creates a new instance of <see cref="AppRepository"/>.
        /// </summary>
        public AppRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        /// <inheritdoc />
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        /// <inheritdoc />
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
    }
}