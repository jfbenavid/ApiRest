namespace Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class BalanceSheetModel
    {
        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public double Amount { get; set; }
    }
}