namespace Repository.Interfaces
{
    using System.Threading.Tasks;
    using Repository.Entities;

    /// <summary>
    /// Contains methods to manage the user information in the repository.
    /// </summary>
    public interface IUserRepository : IAppRepository
    {
        /// <summary>
        /// Get an user by username and password.
        /// </summary>
        Task<User> GetUserAsync(string username, string password);

        /// <summary>
        /// Gets all the information for users, including roles and balance sheets if you want.
        /// </summary>
        Task<User[]> GetAllUsersAsync(bool includeRole = false, bool includeBalances = false);

        /// <summary>
        /// Changes the role for an user.
        /// </summary>
        Task ChangeRoleAsync(string username, int newRole);

        /// <summary>
        /// Gets all the roles in the database.
        /// </summary>
        Task<Role[]> GetRolesAsync();

        /// <summary>
        /// Gets a role in the database.
        /// </summary>
        Task<Role> GetRoleByIdAsync(int roleId);

        /// <summary>
        /// Gets the information for an user with its username.
        /// </summary>
        Task<User> GetUserInfoAsync(string username);

        /// <summary>
        /// Gets all the balances for an user with its username.
        /// </summary>
        Task<BalanceSheet[]> GetBalanceSheetsByUsernameAsync(string username);

        /// <summary>
        /// Gets a balance by its id.
        /// </summary>
        Task<BalanceSheet> GetBalanceSheetByIdAsync(int balanceId);
    }
}