using Cresud.CDP.Dtos.Common;

namespace Cresud.CDP.Dtos.Filters
{
    public class FilterLotesCartaPorte: FilterBase
    {
        public int Desde { get; set; }
        public bool TieneDisponibilidad { get; set; }
    }
}
