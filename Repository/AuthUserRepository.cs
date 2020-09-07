namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Models.Enums;
    using Repository.Entities;
    using Repository.Interfaces;

    public class AuthUserRepository : AppRepository, IAuthUserRepository
    {
        private readonly AppDbContext _context;

        public AuthUserRepository(AppDbContext context)
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

        public async Task<AuthUser> GetAuthUserAsync(string username, string password)
        {
            return await _context
                .AuthUsers
                .Include(c => c.Role)
                .AsNoTracking()
                .FirstOrDefaultAsync(user =>
                    user.Username.Equals(username) &&
                    user.Password.Equals(password));
        }

        public async Task<AuthUser> GetAuthUserAsync(string user)
        {
            return await _context
                .AuthUsers
                .AsNoTracking()
                .FirstOrDefaultAsync(user =>
                    user.Username.Equals(user));
        }

        public async Task<AuthUser[]> GetAuthUsersAsync(bool includeRoles = false)
        {
            var query = _context.AuthUsers
                .AsNoTracking();

            if (includeRoles)
            {
                query = query.Include(user => user.Role);
            }

            return await query.ToArrayAsync();
        }

        public async Task<Role[]> GetRolesAsync()
        {
            return await _context.Roles.ToArrayAsync();
        }
    }
}