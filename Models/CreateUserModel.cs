namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Models.Enums;

    public class CreateAuthModel : AuthModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [EnumDataType(typeof(Roles))]
        public int RoleId { get; set; } = (int)Roles.User;
    }
}