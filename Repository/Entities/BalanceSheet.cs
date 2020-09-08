namespace Repository.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Entity for Balance sheets of the user.
    /// </summary>
    public class BalanceSheet
    {
        /// <summary>
        /// Gets or sets the Balance sheet id.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BalanceSheetId { get; set; }

        /// <summary>
        /// Gets or sets the User id related to the balance sheet.
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the User object related to the balance sheet.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets the Amount used in the balance sheet.
        /// </summary>
        [Required]
        public double Amount { get; set; }

        /// <summary>
        /// Gets or sets the Created date of the balance sheet.
        /// </summary>
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}