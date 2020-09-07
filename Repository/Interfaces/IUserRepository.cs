namespace Repository.Interfaces
{
    using System.Threading.Tasks;
    using Repository.Entities;

    public interface IUserRepository : IAppRepository
    {
        Task<User> GetAuthUserAsync(string username, string password);

        Task<User[]> GetAuthUsersAsync(bool includeRole = false, bool includeBalances = false);

        Task ChangeRoleAsync(string user, int newRole);

        Task<Role[]> GetRolesAsync();

        Task<User> GetAuthUserAsync(string user);

        Task<BalanceSheet[]> GetBalanceSheetsAsync(string user);
    }
}