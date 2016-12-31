using System;
using System.Linq;
using Cresud.CDP.Dtos.Common;
using Cresud.CDP.Entities;

namespace Cresud.CDP.Admin
{
    public class EmpresaAdmin : BaseAdmin<int, Empresa, Dtos.Empresa, FilterBase>
    {
        public override Empresa ToEntity(Dtos.Empresa dto)
        {
            throw new NotImplementedException();
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
