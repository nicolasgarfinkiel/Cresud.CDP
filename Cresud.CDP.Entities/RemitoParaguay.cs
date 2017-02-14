using System;

namespace Cresud.CDP.Entities
{
    public class RemitoParaguay
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Cee { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public string NumeroRemision { get; set; }
        public DateTime? FechaDeEmision { get; set; }
        public string RazonSocial { get; set; }
        public string Cuit { get; set; }
        public string Direccion { get; set; }
        public string MotivoTraslado { get; set; }
        public string CteDeVta { get; set; }
        public string TranspRazonSocial { get; set; }
        public string TransportistaCuit { get; set; }
        public string EPDireccion { get; set; }
        public string LocPartida { get; set; }
        public string ProvPartida { get; set; }
        public string EDDireccion { get; set; }
        public string LocLlegada { get; set; }
        public string ProvLlegada { get; set; }
        public decimal? KmRecorridos { get; set; }
        public string PatenteCamion { get; set; }
        public string PatenteAcoplado { get; set; }
        public string ChoferRazonSocial { get; set; }
        public string ChoferCuit { get; set; }
        public string ChoferDomicilio { get; set; }
        public string MarcaVehiculo { get; set; }
        public decimal? Cantidad { get; set; }
        public string Kg { get; set; }
        public string DescripcionDetallada { get; set; }
        public decimal? HabilitacionNum { get; set; }
    }
}
