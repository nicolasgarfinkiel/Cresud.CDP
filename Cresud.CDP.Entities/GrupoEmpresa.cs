using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cresud.CDP.Entities
{
    public class GrupoEmpresa: EntityBase
    {
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public int IdApp { get; set; }
        public virtual Pais Pais { get; set; }   
    }    				
}
