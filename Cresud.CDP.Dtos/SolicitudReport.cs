using System;

namespace Cresud.CDP.Dtos
{
    public class SolicitudReport
    {
        public int Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public int EmpresaId { get; set; }
        public string ObservacionAfip { get; set; }
        public string NumeroCartaDePorte { get; set; }
        public DateTime? FechaDeEmision { get; set; }
        public DateTime? FechaDeCarga { get; set; }
        public DateTime? FechaDeVencimiento { get; set; }
        public int? ClienteIntermediarioId { get; set; }
        public int? ClienteRemitenteComercialId { get; set; }
        public int? ClienteCorredorId { get; set; }
        public int? ClienteEntregadorId { get; set; }
        public int? ClienteDestinatarioId { get; set; }
        public int? ProveedorTitularCartaDePorteId { get; set; }
        public int? ProveedorTransportistaId { get; set; }
        public int? ChoferId { get; set; }
        public int? CosechaId { get; set; }
        public int? NumeroContrato { get; set; }
        public bool? CargaPesadaDestino { get; set; }
        public decimal? KilogramosEstimados { get; set; }
        public decimal? PesoBruto { get; set; }
        public decimal? PesoTara { get; set; }
        public string Observaciones { get; set; }
        public int? EstablecimientoProcedenciaId { get; set; }
        public int? EstablecimientoDestinoId { get; set; }
        public string PatenteCamion { get; set; }
        public string PatenteAcoplado { get; set; }
        public decimal? KmRecorridos { get; set; }
        public int? EstadoFlete { get; set; }
        public decimal? CantHoras { get; set; }
        public decimal? TarifaReferencia { get; set; }
        public decimal? TarifaReal { get; set; }
        public int? EstadoEnSAP { get; set; }
        public int? EstadoEnAFIP { get; set; }
        public int? GranoId { get; set; }
        public decimal? CodigoAnulacionAfip { get; set; }
        public DateTime? FechaAnulacionAfip { get; set; }
        public string CodigoRespuestaEnvioSAP { get; set; }
        public string CodigoRespuestaAnulacionSAP { get; set; }
        public decimal? PHumedad { get; set; }
        public decimal? POtros { get; set; }

        public string TipoCarta { get; set; }
        public string Cee { get; set; }
        public string Ctg { get; set; }
        public string Intermediario { get; set; }
        public string CteRemitenteComecial { get; set; }
        public bool? EsCanjeador { get; set; }
        public string CteCorredor { get; set; }
        public string Entregador { get; set; }
        public string Destinatario { get; set; }
        public string Destino { get; set; }
        public string Transportista { get; set; }
        public string CTransportista { get; set; }
        public string Chofer { get; set; }
        public string Grano { get; set; }
        public int? ConformeCondicional { get; set; }
        public string EstProcedencia { get; set; }
        public string EstDestino { get; set; }
        public string CtePagador { get; set; }
        public string EstDestinoCambio { get; set; }
        public string CteDestinatarioCambio { get; set; }
        public string CosechaDescripcion { get; set; }
        public string TitularCDP { get; set; }

        public bool? Asociacartadeporte { get; set; }
        public string ProvTitularCDPNumeroDocumento { get; set; }
        public string CteIntermediarioCuit { get; set; }
        public string CteRemitenteComecialCuit { get; set; }
        public string CteCorredorCuit { get; set; }
        public string CteEntregadorCuit { get; set; }
        public string CteDestinatarioCuit { get; set; }
        public string CteDestinoCuit { get; set; }
        public string TransportistaNumeroDocumento { get; set; }
        public string CTransportistaCuit { get; set; }
        public string ChoferCuit { get; set; }
        public int? EspecieCodigo { get; set; }
        public int? IdTipoGrano { get; set; }
        public int EstProcedenciaLocalidad { get; set; }
        public string EstProcedenciaEstablecimientoAfip { get; set; }
        public int? EstDestinoLocalidad { get; set; }
        public string EstDestinoEstablecimientoAfip { get; set; }
        public decimal? PesoNeto { get; set; }
    }
}


