namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BalanceSheetModel
    {
        [Required]
        public int AccountId { get; set; }

        [Required]
        public double Amount { get; set; }
    }
}