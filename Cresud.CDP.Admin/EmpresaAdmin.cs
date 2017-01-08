using System;
using System.Linq;
using Cresud.CDP.Dtos.Common;
using Empresa = Cresud.CDP.Entities.Empresa;

namespace Cresud.CDP.Admin
{
    public class EmpresaAdmin : BaseAdmin<int, Empresa, Dtos.Empresa, FilterBase>
    {
        #region Base

        public override Empresa ToEntity(Dtos.Empresa dto)
        {
            throw new NotImplementedException();
        }

        public override void Validate(Dtos.Empresa dto)
        {
            throw new NotImplementedException();            
        }

        public override IQueryable GetQuery(FilterBase filter)
        {
            throw new NotImplementedException();

        }

        #endregion
    }
}
