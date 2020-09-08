namespace Models
{
    using System.ComponentModel.DataAnnotations;
    using Models.Enums;

    /// <summary>
    /// Model to manage the roles information.
    /// </summary>
    public class RoleModel
    {
        /// <summary>
        /// Gets or sets the Id for the Role.
        /// </summary>
        [EnumDataType(typeof(Roles))]
        public int RoleId { get; set; }

        /// <summary>
        /// Gets or sets the Name for the role.
        /// </summary>
        public string Name { get; set; }
    }
}