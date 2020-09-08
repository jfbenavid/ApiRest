namespace Models
{
    /// <summary>
    /// Model to manage the user information.
    /// </summary>
    public class UserModel : LoginModel
    {
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