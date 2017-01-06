using System;

namespace Cresud.CDP.Entities
{
    public class Grano : EntityBase
    {        
        public string Descripcion { get; set; }        
        public string IdMaterialSap { get; set; }
        public string SujetoALote { get; set; }
        public virtual Especie EspecieAfip { get; set;  }
        public virtual TipoGrano TipoGranoAfip { get; set;  }
        public virtual Cosecha CosechaAfip { get; set; }
        public virtual GrupoEmpresa GrupoEmpresa { get; set;  }                
    }
}





