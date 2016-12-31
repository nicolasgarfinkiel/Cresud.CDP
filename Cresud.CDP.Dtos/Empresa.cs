using System.Collections.Generic;

namespace Cresud.CDP.Dtos
{
    public class Empresa
    {
        public GrupoEmpresa GrupoEmpresa { get; set; }
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public string Descripcion { get; set; }
        public string IdSapOrganizacionDeVenta { get; set; }
        public string IdSapSector { get; set; }
        public string IdSapCanalLocal { get; set; }
        public string IdSapCanalExpor { get; set; }
        public string SapId { get; set; }
        public string IdSapMoneda { get; set; }      
        public IList<string> Roles { get; set; }
        
    }
}
