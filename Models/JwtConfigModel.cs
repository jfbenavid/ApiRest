namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class JwtConfigModel
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
    }
}