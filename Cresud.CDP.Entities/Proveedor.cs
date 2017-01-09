using System;

namespace Cresud.CDP.Entities
{
    public class Proveedor: EntityBase
    {     
        public string SapId { get; set; }
        public string Nombre { get; set; }
        public virtual TipoDocumentoSap TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Calle { get; set; }
        public string Piso { get; set; }
        public string Departamento { get; set; }
        public string Numero { get; set; }
        public string Cp { get; set; }
        public string Ciudad { get; set; }
        public string Pais { get; set; }
        public string Domicilio { get; set; }
        public bool EsProspecto { get; set; }
        public int IdSapOrganizacionDeVenta { get; set; }
           
    }
}


