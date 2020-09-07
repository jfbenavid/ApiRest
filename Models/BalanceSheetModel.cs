namespace Models
{
    using System.ComponentModel.DataAnnotations;

    public class BalanceSheetModel
    {
        [Required]
        public int AccountId { get; set; }

        [Required]
        public double Amount { get; set; }
    }
}