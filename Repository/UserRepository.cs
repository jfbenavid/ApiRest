namespace Repository
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Repository.Entities;
    using Repository.Interfaces;

    /// <summary>
    /// Contains all the methods related to the <see cref="User"/> entity.
    /// </summary>
    public class UserRepository : AppRepository, IUserRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Creates a new instance of <see cref="UserRepository"/>.
        /// </summary>
        public UserRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task ChangeRoleAsync(string username, int newRole)
        {
            var authUser = await _context
                .AuthUsers
                .FirstAsync(user => user.Username.Equals(username));

            authUser.RoleId = newRole;
        }

        /// <inheritdoc />
        public async Task<User> GetUserAsync(string username, string password)
        {
            return await _context
                .AuthUsers
                .Include(c => c.Role)
                .AsNoTracking()
                .FirstOrDefaultAsync(user =>
                    user.Username.Equals(username) &&
                    user.Password.Equals(password));
        }

        /// <inheritdoc />
        public async Task<User> GetUserInfoAsync(string username)
        {
            return await _context
                .AuthUsers
                .AsNoTracking()
                .FirstOrDefaultAsync(user =>
                    user.Username.Equals(username));
        }

        /// <inheritdoc />
        public async Task<User[]> GetAllUsersAsync(bool includeRole = false, bool includeBalances = false)
        {
            var query = _context.AuthUsers
                .AsNoTracking();

            if (includeRole)
            {
                query = query.Include(user => user.Role);
            }

            if (includeBalances)
            {
                query = query.Include(user => user.BalanceSheets);
            }

            return await query.ToArrayAsync();
        }

        /// <inheritdoc />
        public async Task<BalanceSheet[]> GetBalanceSheetsByUsernameAsync(string username)
        {
            return await _context.BalanceSheets
                .Include(balance => balance.User)
                .AsNoTracking()
                .Where(balance => balance.User.Username.Equals(username))
                .ToArrayAsync();
        }

        /// <inheritdoc />
        public async Task<Role[]> GetRolesAsync()
        {
            return await _context.Roles.ToArrayAsync();
        }

        /// <inheritdoc />
        public async Task<Role> GetRoleByIdAsync(int roleId)
        {
            return await _context
                .Roles
                .FirstOrDefaultAsync(role => role.RoleId == roleId);
        }

        /// <inheritdoc />
        public async Task<BalanceSheet> GetBalanceSheetByIdAsync(int balanceId)
        {
            return await _context
                .BalanceSheets
                .FirstOrDefaultAsync(balance => balance.BalanceSheetId == balanceId);
        }
    }
}