namespace Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Model to create a user.
    /// </summary>
    public class CreateUserModel : UserModel
    {
        /// <summary>
        /// <see cref="UserModel.Email"/>
        /// </summary>
        [Required]
        public override string Email { get; set; }
    }
}