using System.Collections.Generic;

namespace Cresud.CDP.Dtos.Common
{
    public class Result
    {
        public bool HasErrors { get; set; }
        public IList<string> Messages { get; set; }
    }
}
