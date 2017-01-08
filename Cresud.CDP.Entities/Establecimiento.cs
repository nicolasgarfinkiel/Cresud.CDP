using System.ComponentModel.DataAnnotations.Schema;

namespace Cresud.CDP.Entities
{
    public class Establecimiento : EntityBase
    {
        public string Descripcion { get; set; }
        public string Direccion { get; set; }            
        public int LocalidadId { get; set; }
        public virtual Provincia Provincia { get; set; }
        public string IdAlmacenSap { get; set; }
        public string IdCentroSap { get; set; }
        public RecorridoEstablecimiento? RecorridoEstablecimiento { get; set; }
        public string IdCEBE { get; set; }
        public string IdExpedicion { get; set; }
        public string EstablecimientoAfip { get; set; }
        public int EmpresaId { get; set; }
        public int InterlocutorDestinatarioId { get; set; }
        public bool AsociaCartaDePorte { get; set; }
    }
}

