using System;

namespace Cresud.CDP.Entities
{
    public class Chofer : EntityBase
    {        
        public string Nombre { get; set; }        
        public string Apellido { get; set; }        
        public string Cuit { get; set; }        
        public string Camion { get; set; }        
        public string Acoplado { get; set; }        
        public DateTime FechaCreacion { get; set; }        
        public string UsuarioCreacion { get; set; }        
        public DateTime FechaModificacion { get; set; }        
        public string UsuarioModificacion { get; set; }
        public bool Activo { get; set; }
        public bool EsChoferTransportista { get; set; }
        public virtual GrupoEmpresa GrupoEmpresa{ get; set; }
        public string Domicilio { get; set; }
        public string Marca { get; set; }
    }
}


