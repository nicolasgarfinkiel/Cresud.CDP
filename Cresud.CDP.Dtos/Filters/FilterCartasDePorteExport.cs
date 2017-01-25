using System;
using Cresud.CDP.Dtos.Common;

namespace Cresud.CDP.Dtos.Filters
{
    public class FilterCartasDePorteExport : FilterBase
    {     
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public int? ClienteRemitenteComercialId { get; set; }
        public int? ClienteCorredorId { get; set; }
        public int? ClienteEntregadorId { get; set; }
        public int? ClienteDestinatarioId { get; set; }
        public int? CosechaId { get; set; }
        public int? GranoId { get; set; }
        public int? IntermediarioId { get; set; }
        public int? ProveedorTitularCartaDePorteId { get; set; }
        public int? ProveedorTransportistaId { get; set; }
        public int? ChoferId { get; set; }
        public int? EstablecimientoProcedenciaId { get; set; }
        public int? EstablecimientoDestinoId { get; set; }         
    }
}
