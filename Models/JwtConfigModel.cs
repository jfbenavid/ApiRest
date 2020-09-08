namespace Models
{
    /// <summary>
    /// Model to use the information in appSettings.json for jwt.
    /// </summary>
    public class JwtConfigModel
    {
        /// <summary>
        /// Gets or sets the secret key of the jwt.
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// Gets or sets the Issuer of the jwt.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Gets or sets the minutes to the jwt alive.
        /// </summary>
        public int MinutesAlive { get; set; }
    }
}