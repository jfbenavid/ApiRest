namespace Models
{
    using System.ComponentModel.DataAnnotations;
    using Models.Enums;

    /// <summary>
    /// Model to create a user.
    /// </summary>
    public class CreateUserModel : UserModel
    {
        /// <summary>
        /// Gets or sets the Email address for the user.
        /// </summary>
        [Required]
        public override string Email { get; set; }

        /// <summary>
        /// Gets or sets the roleId for the user.
        /// </summary>
        [EnumDataType(typeof(Roles))]
        public int RoleId { get; set; } = (int)Roles.User;
    }
}