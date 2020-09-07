namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Repository.Entities;
    using Repository.Interfaces;

    public class AuthUserRepository : IAuthUserRepository
    {
        private readonly AppDbContext _context;

        public AuthUserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAuthUserAsync(AuthUser user)
        {
            await _context.AddAsync(user);
        }

        public async Task<AuthUser> GetAuthUserAsync(string username, string password)
        {
            return await _context
                .AuthUsers
                .FirstOrDefaultAsync(user =>
                    user.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase) &&
                    user.Password.Equals(password));
        }
    }
}