using System;

namespace Cresud.CDP.Dtos
{
    public class Chofer 
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }        
        public string Apellido { get; set; }        
        public string Cuit { get; set; }        
        public string Camion { get; set; }        
        public string Acoplado { get; set; }        
        public DateTime CreateDate { get; set; }        
        public string CreatedBy { get; set; }                        
        public bool Enabled { get; set; }
        public bool EsChoferTransportista { get; set; }
        public int GrupoEmpresaId{ get; set; }
        public string Domicilio { get; set; }
        public string Marca { get; set; }
    }
}


