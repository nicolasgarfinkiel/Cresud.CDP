﻿using System;

namespace Cresud.CDP.Dtos
{	
    public class SolicitudRecibida 
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public int? TipoCartaId { get; set; }
        public string NumeroCartaDePorte { get; set; }
        public string Cee { get; set; }
        public string Ctg { get; set; }
        public DateTime? FechaDeCarga { get; set; }
        public string ProveedorTitularCartaDePorteCuit { get; set; }
        public string ClienteIntermediarioCuit { get; set; }
        public string ClienteRemitenteComercialCuit { get; set; }
        public string ClienteCorredorCuit { get; set; }
        public string ClienteEntregadorCuit { get; set; }
        public string ClienteDestinatarioCuit { get; set; }
        public string ClienteDestinoCuit { get; set; }
        public string ProveedorTransportistaCuit { get; set; }
        public string ChoferCuit { get; set; }
        public Grano Grano { get; set; }
        public string NumeroContrato { get; set; }
        public bool? CargaPesadaDestino { get; set; }
        public decimal?  KilogramosEstimados { get; set; }
        public decimal? PesoBruto { get; set; }
        public decimal? PesoTara { get; set; }
        public string EstablecimientoProcedenciaCodigo { get; set; }
        public int? EstablecimientoProcedenciaLocalidadId { get; set; }
        public string EstablecimientoDestinoCodigo { get; set; }
        public int? EstablecimientoDestinoLocalidadId { get; set; }
        public decimal? KmRecorridos { get; set; }
        public string PatenteCamion { get; set; }
        public string PatenteAcoplado { get; set; }
         public decimal? TarifaReal { get; set; }
        public DateTime? FchaDescarga { get; set; }
        public DateTime? FechaDeEmision { get; set; }
        public DateTime? FchaArribo { get; set; }
        public decimal? PesoNetoDescarga { get; set; }
        public string EstablecimientoDestinoCambioCuit { get; set; }
        public int? EstablecimientoDestinoCambioLocalidadId { get; set; }
        public decimal? TarifaReferencia { get; set; }

        public decimal? PesoNeto
        {
            get { return PesoBruto - PesoTara; }
        }
    }
}


