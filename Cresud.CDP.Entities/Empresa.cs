using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cresud.CDP.Entities
{
    public class Empresa : EntityBase
    {
        public int IdCliente { get; set; }
        public string Descripcion { get; set; }
        public string IdSapOrganizacionDeVenta { get; set; }
        public string IdSapSector { get; set; }
        public string IdSapCanalLocal { get; set; }
        public string IdSapCanalExpor { get; set; }
        public string SapId { get; set; }
        public string IdSapMoneda { get; set; }
        public virtual GrupoEmpresa GrupoEmpresa { get; set; }
    }
}
