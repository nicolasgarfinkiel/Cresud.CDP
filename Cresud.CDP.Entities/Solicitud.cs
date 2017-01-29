using System;

namespace Cresud.CDP.Entities
{
    public class Solicitud : EntityBase
    {
        public int EmpresaId { get; set; }
        public string ObservacionAfip { get; set; }
        public string NumeroCartaDePorte { get; set; }
        public string Cee { get; set; }
        public string Ctg { get; set; }
        public DateTime? FechaDeEmision { get; set; }
        public DateTime? FechaDeCarga { get; set; }
        public DateTime? FechaDeVencimiento { get; set; }
        public int? ClienteIntermediarioId { get; set; }
        public int? ClienteRemitenteComercialId { get; set; }
        public int? ClienteCorredorId { get; set; }
        public int? ClienteEntregadorId { get; set; }
        public int? ClienteDestinatarioId { get; set; }
        public int? ClienteDestinoId { get; set; }
        public bool? RemitenteComercialComoCanjeador { get; set; }
        public int? ProveedorTitularCartaDePorteId { get; set; }
        public int? ProveedorTransportistaId { get; set; }
        public int? ChoferId { get; set; }
        public int? CosechaId { get; set; }
        public int? EspecieId { get; set; }
        public int? NumeroContrato { get; set; }
        public int? SapContrato { get; set; }
        public bool? SinContrato { get; set; }
        public bool? CargaPesadaDestino { get; set; }
        public decimal? KilogramosEstimados { get; set; }
        public string DeclaracionDeCalidad { get; set; }
        public int? ConformeCondicionalId { get; set; }
        public decimal? PesoBruto { get; set; }
        public decimal? PesoTara { get; set; }
        public string Observaciones { get; set; }
        public string LoteDeMaterial { get; set; }
        public int? EstablecimientoProcedenciaId { get; set; }
        public int? EstablecimientoDestinoId { get; set; }
        public string PatenteCamion { get; set; }
        public string PatenteAcoplado { get; set; }
        public decimal? KmRecorridos { get; set; }
        public int? EstadoFlete { get; set; }
        public decimal? CantHoras { get; set; }
        public decimal? TarifaReferencia { get; set; }
        public decimal? TarifaReal { get; set; }
        public int? ClientePagadorDelFleteId { get; set; }
        public int? EstadoEnSAP { get; set; }
        public int? EstadoEnAFIP { get; set; }
        public int? GranoId { get; set; }
        public decimal? CodigoAnulacionAfip { get; set; }
        public DateTime? FechaAnulacionAfip { get; set; }
        public string CodigoRespuestaEnvioSAP { get; set; }
        public string MensajeRespuestaEnvioSAP { get; set; }
        public string CodigoRespuestaAnulacionSAP { get; set; }
        public string MensajeRespuestaAnulacionSAP { get; set; }
        public int? EstablecimientoDestinoCambioId { get; set; }
        public int? ClienteDestinatarioCambioId { get; set; }
        public int? ChoferTransportistaId { get; set; }
        public decimal? PHumedad { get; set; }
        public decimal? POtros { get; set; }
        public int? TipoDeCartaId { get; set; }

        public decimal? PesoNeto
        {
            get { return PesoBruto - PesoTara; }
        }
    }
}

