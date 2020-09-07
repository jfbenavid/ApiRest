namespace Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Repository.Entities;

    public interface IAuthUserRepository
    {
        Task<AuthUser> GetAuthUserAsync(string username, string password);

        Task AddAuthUserAsync(AuthUser user);
    }
}