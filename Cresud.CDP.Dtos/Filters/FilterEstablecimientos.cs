using Cresud.CDP.Dtos.Common;

namespace Cresud.CDP.Dtos.Filters
{
    public class FilterEstablecimientos: FilterBase
    {        
        public bool Origen { get; set; }

        public bool Destino { get; set; }
    }
}
