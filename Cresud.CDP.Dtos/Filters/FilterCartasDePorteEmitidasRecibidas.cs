using System;
using Cresud.CDP.Dtos.Common;

namespace Cresud.CDP.Dtos.Filters
{
    public class FilterCartasDePorteEmitidasRecibidas : FilterBase
    {     
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }        
    }
}
