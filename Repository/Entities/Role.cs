namespace Repository.Entities
{
    using System.Collections.Generic;

    public class Role
    {
        public int RoleId { get; set; }

        public string Name { get; set; }

        public List<AuthUser> Auths { get; set; }
    }
}