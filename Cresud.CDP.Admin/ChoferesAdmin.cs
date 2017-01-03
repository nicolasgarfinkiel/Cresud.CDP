using System;
using System.Linq;
using Cresud.CDP.Dtos.Common;
using Cresud.CDP.Entities;

namespace Cresud.CDP.Admin
{
    public class ChoferesAdmin : BaseAdmin<int, Entities.Chofer, Dtos.Chofer, FilterBase>
    {
        public override Chofer ToEntity(Dtos.Chofer dto)
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
