namespace Domain.Interfaces
{
    using Repository.Entities;

    public interface IJwtUtils
    {
        string GenerateJwt(User user);
    }
}