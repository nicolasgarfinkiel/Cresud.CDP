﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cresud.CDP.Entities
{
    public class Especie: EntityBase
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public virtual GrupoEmpresa GrupoEmpresa { get; set; }
    }
}

