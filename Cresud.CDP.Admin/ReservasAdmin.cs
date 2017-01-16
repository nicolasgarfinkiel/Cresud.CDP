using System;
using System.Linq;
using Cresud.CDP.Dtos.Common;
using Cresud.CDP.Entities;

namespace Cresud.CDP.Admin
{
    public class ReservasAdmin : BaseAdmin<int, Entities.Solicitud, Dtos.Solicitud, FilterBase>
    {
        #region Base

        public override Solicitud ToEntity(Dtos.Solicitud dto)
        {
            return null;
        }

        public override void Validate(Dtos.Solicitud dto)
        {            
        }

        public override IQueryable GetQuery(FilterBase filter)
        {
            var result = CdpContext.Choferes.Where(c => c.GrupoEmpresa.Id == filter.IdGrupoEmpresa).OrderBy(c => c.Nombre).ThenBy(c => c.Apellido).AsQueryable();

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

        #endregion
    }
}
