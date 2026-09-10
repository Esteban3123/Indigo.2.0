using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Autentication
{
    public class JwtSettings
    {
        public string SecretName { get; set; }
        public string AudienceSecretName { get; set; }
        public string IssuerSecretName { get; set; }
        public string ExpiresInMinutes { get; set; }
    }
}
