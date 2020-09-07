namespace Models
{
    using System.ComponentModel.DataAnnotations;
    using Models.Enums;

    public class UserModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [EnumDataType(typeof(Roles))]
        public int RoleId { get; set; } = (int)Roles.User;

        public virtual string Email { get; set; }

        public string RoleName { get; set; }
    }
}