namespace Models
{
    using System.ComponentModel.DataAnnotations;

    public class CreateUserModel : UserModel
    {
        [Required]
        public override string Email { get; set; }
    }
}