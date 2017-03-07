using System;

namespace Cresud.CDP.Dtos
{
    public class Establecimiento
    {
        public int? Id { get; set; }
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public int LocalidadId { get; set; }
        public string LocalidadDescripcion { get; set; }
        public int ProvinciaId { get; set; }
        public string ProvinciaDescripcion { get; set; }
        public string IdAlmacenSap { get; set; }
        public string IdCentroSap { get; set; }        
        public string RecorridoEstablecimiento { get; set; }
        public string IdCEBE { get; set; }
        public string IdExpedicion { get; set; }
        public string EstablecimientoAfip { get; set; }
        public int EmpresaId { get; set; }
        public int InterlocutorDestinatarioId { get; set; }
        public Cliente InterlocutorDestinatario { get; set; }
        public bool AsociaCartaDePorte { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }  
    }
}

