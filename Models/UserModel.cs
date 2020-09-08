namespace Models
{
    using System.ComponentModel.DataAnnotations;
    using Models.Enums;

    /// <summary>
    /// Model to manage the user information.
    /// </summary>
    public class UserModel : LoginModel
    {
        /// <summary>
        /// Gets or sets the roleId for the user.
        /// </summary>
        [EnumDataType(typeof(Roles))]
        public int RoleId { get; set; } = (int)Roles.User;

        /// <summary>
        /// Gets or sets the Email address for the user.
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// Gets or sets the Role name for the user.
        /// </summary>
        public string RoleName { get; set; }
    }
}