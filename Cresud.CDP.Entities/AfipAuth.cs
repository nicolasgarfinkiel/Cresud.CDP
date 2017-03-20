using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cresud.CDP.Entities
{
    public class AfipAuth
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Sign { get; set; }
        public string CuitRepresentado { get; set; }
        public DateTime? GenerationTime { get; set; }
        public DateTime? ExpirationTime { get; set; }
          public string Service { get; set; }
        public string UniqueId { get; set; }
    }
}


