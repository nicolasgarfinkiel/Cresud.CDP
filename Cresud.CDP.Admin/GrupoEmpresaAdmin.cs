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
            var pais = CdpContext.Paises.Single(g => g.Id == dto.PaisId);

            if (!dto.Id.HasValue)
            {
                entity = new Entities.GrupoEmpresa
                {                                      
                    Descripcion = dto.Descripcion,
                    Activo = true,
                    IdApp = 0,
                    Pais = pais
                };
            }
            else
            {
                entity = CdpContext.GrupoEmpresas.Single(c => c.Id == dto.Id.Value);                
                entity.Descripcion = dto.Descripcion;
                entity.Pais = pais;
            }

            return entity;
        }

        public override void Validate(Dtos.GrupoEmpresa dto)
        {
            var entity = CdpContext.GrupoEmpresas.FirstOrDefault(c => string.Equals(c.Descripcion.ToLower(), dto.Descripcion.ToLower()));

            if (entity != null && entity.Id != dto.Id)
                throw new Exception("Ya existe otro grupo empresa con la misma descripción");
        }

        public override IQueryable GetQuery(FilterBase filter)
        {
            var result = CdpContext.GrupoEmpresas.OrderBy(c => c.Descripcion).AsQueryable();

            if (!string.IsNullOrEmpty(filter.MultiColumnSearchText))
            {
                filter.MultiColumnSearchText = filter.MultiColumnSearchText.ToLower();

                result = result.Where(r =>
                    (r.Descripcion != null && r.Descripcion.ToLower().Contains(filter.MultiColumnSearchText)) ||
                    (r.Pais != null && r.Pais.Descripcion.ToLower().Contains(filter.MultiColumnSearchText))                    
                    ).AsQueryable();
            }

            return result;
        }

        #endregion
    }
}
