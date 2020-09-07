namespace Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Models;
    using Repository.Entities;

    public interface IJwtUtils
    {
        string GenerateJwt(User user);
    }
}