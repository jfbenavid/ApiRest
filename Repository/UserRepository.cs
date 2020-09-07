namespace Repository
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Repository.Entities;
    using Repository.Interfaces;

    public class UserRepository : AppRepository, IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task ChangeRoleAsync(string user, int newRole)
        {
            var authUser = await _context
                .AuthUsers
                .FirstAsync(user => user.Username.Equals(user));

            authUser.RoleId = newRole;
        }

        public async Task<User> GetAuthUserAsync(string username, string password)
        {
            return await _context
                .AuthUsers
                .Include(c => c.Role)
                .AsNoTracking()
                .FirstOrDefaultAsync(user =>
                    user.Username.Equals(username) &&
                    user.Password.Equals(password));
        }

        public async Task<User> GetAuthUserAsync(string user)
        {
            return await _context
                .AuthUsers
                .AsNoTracking()
                .FirstOrDefaultAsync(user =>
                    user.Username.Equals(user));
        }

        public async Task<User[]> GetAuthUsersAsync(bool includeRole = false, bool includeBalances = false)
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

        public async Task<BalanceSheet[]> GetBalanceSheetsAsync(string user)
        {
            return await _context.BalanceSheets
                .Include(balance => balance.User)
                .AsNoTracking()
                .Where(balance => balance.User.Username.Equals(user))
                .ToArrayAsync();
        }

        public async Task<Role[]> GetRolesAsync()
        {
            return await _context.Roles.ToArrayAsync();
        }
    }
}