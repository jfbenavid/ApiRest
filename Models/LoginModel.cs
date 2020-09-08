namespace Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Model to manage the login to the web app.
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Gets or sets the username to login.
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password to login.
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}