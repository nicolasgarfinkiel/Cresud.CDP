using Cresud.CDP.Dtos.Common;

namespace Cresud.CDP.Dtos.Filters
{
    public class FilterSolicitudes : FilterBase
    {     
        public int? EstadoAfip { get; set; }
        public int? EstadoSap { get; set; }
    }
}
