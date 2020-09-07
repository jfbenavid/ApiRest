namespace Repository.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AuthUser
    {
        public int AuthUserId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string EmailAddress { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}