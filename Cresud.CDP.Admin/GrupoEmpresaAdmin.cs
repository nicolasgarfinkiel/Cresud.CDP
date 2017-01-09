using System;
using System.Linq;
using Cresud.CDP.Dtos.Common;

namespace Cresud.CDP.Admin
{
    public class GrupoEmpresaAdmin : BaseAdmin<int, Entities.GrupoEmpresa, Dtos.GrupoEmpresa, FilterBase>
    {
        #region Base

        public override Entities.GrupoEmpresa ToEntity(Dtos.GrupoEmpresa dto)
        {
            var entity = default(Entities.GrupoEmpresa);

            if (!dto.Id.HasValue)
            {
                entity = new Entities.GrupoEmpresa
                {                                      
                    Descripcion = dto.Descripcion,                    
                };
            }
            else
            {
                entity = CdpContext.GrupoEmpresas.Single(c => c.Id == dto.Id.Value);                
                entity.Descripcion = dto.Descripcion;                
            }

            return entity;
        }

        public override void Validate(Dtos.GrupoEmpresa dto)
        {
            var entity = CdpContext.Empresas.FirstOrDefault(c => string.Equals(c.Descripcion.ToLower(), dto.Descripcion.ToLower()));

            if (entity != null && entity.Id != dto.Id)
                throw new Exception("Ya existe otro grupo empresa con la misma descripción");
        }

        public override IQueryable GetQuery(FilterBase filter)
        {
            var result = CdpContext.Empresas.OrderBy(c => c.Descripcion).AsQueryable();

            if (!string.IsNullOrEmpty(filter.MultiColumnSearchText))
            {
                filter.MultiColumnSearchText = filter.MultiColumnSearchText.ToLower();

                result = result.Where(r =>
                    (r.Descripcion != null && r.Descripcion.ToLower().Contains(filter.MultiColumnSearchText)) ||
                    (r.IdSapMoneda != null && r.IdSapMoneda.ToLower().Contains(filter.MultiColumnSearchText)) ||
                    (r.GrupoEmpresa != null && r.GrupoEmpresa.Descripcion.ToLower().Contains(filter.MultiColumnSearchText)) ||
                    (r.GrupoEmpresa != null && r.GrupoEmpresa.Pais.Descripcion.ToLower().Contains(filter.MultiColumnSearchText))                    
                    ).AsQueryable();
            }

            return result;
        }

        #endregion
    }
}
