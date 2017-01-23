using System;
using System.Linq;
using Cresud.CDP.Dtos.Common;
using Cresud.CDP.Entities;

namespace Cresud.CDP.Admin
{
    public class ReportesAdmin : BaseAdmin<int, Entities.SolicitudReport, Dtos.SolicitudReport, FilterBase>
    {
        #region Base

        public override SolicitudReport ToEntity(Dtos.SolicitudReport dto)
        {
            throw  new NotImplementedException();
        }

        public override void Validate(Dtos.SolicitudReport dto)
        {            
        }

        public override IQueryable GetQuery(FilterBase filter)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
