namespace Repository.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Entity for roles in the database.
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Gets or sets the Role id.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }

        /// <summary>
        /// Gets or sets the Name of the role.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the users related to the roles.
        /// </summary>
        public List<User> Users { get; set; }
    }
}