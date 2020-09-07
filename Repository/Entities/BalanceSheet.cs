namespace Repository.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class BalanceSheet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BalanceSheetId { get; set; }

        [Required]
        public int AccountId { get; set; }

        public Account Account { get; set; }

        [Required]
        public double Amount { get; set; }
    }
}