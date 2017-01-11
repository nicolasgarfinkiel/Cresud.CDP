﻿using System;

namespace Cresud.CDP.Dtos
{
    public class LoteCartaPorte
    {
        public int? Id { get; set; }
        public int? Desde { get; set; }
        public int? Hasta { get; set; }
        public string Cee { get; set; }
        public string EstablecimientoOrigen { get; set; }
        public int GrupoEmpresaId { get; set; }
        public string Sucursal { get; set; }
        public string PuntoEmision { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public long HabilitacionNumero { get; set; }
    }
}