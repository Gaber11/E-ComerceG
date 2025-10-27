using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
      
   // "audience": "Link of Frontend"
        public string Audience { get; set; }

        public string SecretKey { get; set; }
  
        public double ExpirationInDays { get; set; }
   
    }
}
