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
            var entity = default(Empresa);

            if (!dto.Id.HasValue)
            {
                var grupoEmpresa = CdpContext.GrupoEmpresas.Single(g => g.Id == dto.GrupoEmpresaId);

                entity = new Empresa
                {                  
                    GrupoEmpresa = grupoEmpresa,
                    Descripcion = dto.Descripcion,
                    IdCliente = dto.IdCliente,
                    IdSapCanalExpor = dto.IdSapCanalExpor,
                    IdSapCanalLocal = dto.IdSapCanalLocal,
                    IdSapMoneda = dto.IdSapMoneda,
                    IdSapOrganizacionDeVenta = dto.IdSapOrganizacionDeVenta,
                    IdSapSector = dto.IdSapSector,
                    SapId = dto.SapId
                };
            }
            else
            {
                entity = CdpContext.Empresas.Single(c => c.Id == dto.Id.Value);                
                entity.Descripcion = dto.Descripcion;
                entity.IdCliente = dto.IdCliente;
                entity.IdSapCanalExpor = dto.IdSapCanalExpor;
                entity.IdSapCanalLocal = dto.IdSapCanalLocal;
                entity.IdSapMoneda = dto.IdSapMoneda;
                entity.IdSapOrganizacionDeVenta = dto.IdSapOrganizacionDeVenta;
                entity.IdSapSector = dto.IdSapSector;
                entity.SapId = dto.SapId;
            }

            return entity;
        }

        public override void Validate(Dtos.Empresa dto)
        {
            var entity = CdpContext.Empresas.FirstOrDefault(c => string.Equals(c.Descripcion.ToLower(), dto.Descripcion.ToLower()));

            if (entity != null && entity.Id != dto.Id)
                throw new Exception("Ya existe otra empresa con la misma descripción");
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
