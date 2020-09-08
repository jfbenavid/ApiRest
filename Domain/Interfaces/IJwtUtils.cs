namespace Domain.Interfaces
{
    using Repository.Entities;

    /// <summary>
    /// Interface to manage all related to JWT.
    /// </summary>
    public interface IJwtUtils
    {
        /// <summary>
        /// Creates and returns a Json Web Token.
        /// </summary>
        string GenerateJwt(User user);
    }
}