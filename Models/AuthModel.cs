namespace Models
{
    using System.ComponentModel.DataAnnotations;

    public class AuthModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string EmailAddress { get; set; }
    }
}