namespace Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Models.Enums;
    using Repository.Entities;

    public interface IAuthUserRepository : IAppRepository
    {
        Task<AuthUser> GetAuthUserAsync(string username, string password);

        Task<AuthUser[]> GetAuthUsersAsync(bool includeRoles = false);

        Task ChangeRoleAsync(string user, int newRole);

        Task<Role[]> GetRolesAsync();

        Task<AuthUser> GetAuthUserAsync(string user);
    }
}