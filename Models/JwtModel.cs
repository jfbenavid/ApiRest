namespace Models
{
    using System;

    /// <summary>
    /// Model to register information about JWT.
    /// </summary>
    public class JwtModel
    {
        /// <summary>
        /// Gets or sets the Access token for an user.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the expiration time for the token.
        /// </summary>
        public TimeSpan ExpirationTime { get; set; }
    }
}