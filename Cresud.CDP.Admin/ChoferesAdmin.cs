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
            var result = CdpContext.Choferes.OrderBy(c => c.Nombre).ThenBy(c => c.Apellido).AsQueryable();

            if (!string.IsNullOrEmpty(filter.MultiColumnSearchText))
            {
                filter.MultiColumnSearchText = filter.MultiColumnSearchText.ToLower();

                result = result.Where(r => 
                    (r.Nombre != null && r.Nombre.ToLower().Contains(filter.MultiColumnSearchText)) ||
                    (r.Apellido != null && r.Apellido.ToLower().Contains(filter.MultiColumnSearchText)) ||
                    (r.Cuit != null && r.Cuit.ToLower().Contains(filter.MultiColumnSearchText))).AsQueryable();
            }

            return result;
        }        
    }
}
