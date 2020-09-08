namespace Models
{
    /// <summary>
    /// Model to transfer a balance sheet to another user.
    /// </summary>
    public class BalanceSheetTransferModel
    {
        /// <summary>
        /// Gets or sets the username to transfer the balance sheet.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the amount to transfer the balance sheet.
        /// </summary>
        public double Amount { get; set; }
    }
}