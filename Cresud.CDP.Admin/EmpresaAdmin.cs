using System;
using System.Collections.Generic;
using System.Linq;
using Cresud.CDP.Dtos;
using Cresud.CDP.Dtos.Common;
using Empresa = Cresud.CDP.Entities.Empresa;

namespace Cresud.CDP.Admin
{
    public class EmpresaAdmin : BaseAdmin<int, Empresa, Dtos.Empresa, FilterBase>
    {
        public override Empresa ToEntity(Dtos.Empresa dto)
        {
            throw new NotImplementedException();
        }

        public override IList<Dtos.Empresa> GetAll()
        {
            return new List<Dtos.Empresa>
            {
                new Dtos.Empresa
                {
                    Descripcion = "Empresa 1",
                    Id = 1,
                    GrupoEmpresa = new GrupoEmpresa
                    {
                        Descripcion = "G1",
                        IdApp = 1,
                        PaisDescripcion = "Arg",
                        PaisId = 1
                    }
                    
                },
                 new Dtos.Empresa
                {
                    Descripcion = "Empresa 2",
                    Id = 2,
                    GrupoEmpresa = new GrupoEmpresa
                    {
                        Descripcion = "G1",
                        IdApp = 1,
                        PaisDescripcion = "Arg",
                        PaisId = 1
                    }
                }
            };
        }

        public override void Validate()
        {
            throw new NotImplementedException();            
        }

        public override IQueryable GetQuery(FilterBase filter)
        {
            throw new NotImplementedException();
            
        }
        
    }
}
