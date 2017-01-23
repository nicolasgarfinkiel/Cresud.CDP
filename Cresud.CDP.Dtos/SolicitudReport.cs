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
    }
}


