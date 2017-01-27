namespace Cresud.CDP.Dtos
{
    public class SolicitudReport: Solicitud
    {
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

        public bool Asociacartadeporte { get; set; }
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
        public int EspecieCodigo { get; set; }
        public int IdTipoGrano { get; set; }
        public int EstProcedenciaLocalidad { get; set; }
        public string EstProcedenciaEstablecimientoAfip { get; set; }
        public int EstDestinoLocalidad { get; set; }
        public string EstDestinoEstablecimientoAfip { get; set; }
    }
}


