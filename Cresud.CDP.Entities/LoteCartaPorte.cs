using System;
using System.Collections.Generic;
using System.Linq;

namespace Cresud.CDP.Entities
{
    public class LoteCartaPorte: EntityBase
    {
        public int? Desde { get; set; }
        public int? Hasta { get; set; }
        public string Cee { get; set; }
        public string EstablecimientoOrigenId { get; set; }        
        public string Sucursal { get; set; }
        public string PuntoEmision { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public decimal? HabilitacionNumero { get; set; }
        public virtual GrupoEmpresa GrupoEmpresa { get; set; }
        public virtual IList<CartaDePorte> CartasDePorte { get; set; }       

    }
}
