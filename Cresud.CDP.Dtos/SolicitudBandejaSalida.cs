using System;

namespace Cresud.CDP.Dtos
{
    public class SolicitudBandejaSalida
    {
        public int Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int EmpresaId { get; set; }
        public string ObservacionAfip { get; set; }
        public string NumeroCartaDePorte { get; set; }
        public DateTime? FechaDeEmision { get; set; }
        public DateTime? FechaDeVencimiento { get; set; }
        public int? EstadoEnSAP { get; set; }
        public int? EstadoEnAFIP { get; set; }
        public decimal? CodigoAnulacionAfip { get; set; }
        public string CodigoRespuestaEnvioSAP { get; set; }        
        public string TipoCarta { get; set; }
        public string Ctg { get; set; }
        public string EstProcedencia { get; set; }
        public string TitularCDP { get; set; }
        public string MensajeRespuestaEnvioSap { get; set; }        
    }
}

 