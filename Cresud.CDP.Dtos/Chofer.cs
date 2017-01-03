using System;

namespace Cresud.CDP.Dtos
{
    public class Chofer 
    {
        public int Id { get; set; }
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
        public int GrupoEmpresaId{ get; set; }
        public string Domicilio { get; set; }
        public string Marca { get; set; }
    }
}


