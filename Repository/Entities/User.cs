namespace Repository.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Models.Enums;

    /// <summary>
    /// Entity for the users in the database.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the id of the user.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the id for the role to the user.
        /// </summary>
        [Required]
        [ForeignKey("Role")]
        public int RoleId { get; set; } = (int)Roles.User;

        /// <summary>
        /// Gets or sets the related information for roles.
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// Gets or sets the related information of the balance sheets.
        /// </summary>
        public List<BalanceSheet> BalanceSheets { get; set; }
    }
}