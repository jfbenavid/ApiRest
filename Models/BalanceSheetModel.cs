namespace Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Model to manage the balance sheets information.
    /// </summary>
    public class BalanceSheetModel
    {
        /// <summary>
        /// Gets or sets the Created date of the balance sheet.
        /// </summary>
        [Required]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the Amount of the sum for the balance sheets.
        /// </summary>
        [Required]
        public double Amount { get; set; }
    }
}