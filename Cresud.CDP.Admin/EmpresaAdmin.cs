using System;
using System.Linq;
using Cresud.CDP.Dtos.Common;
using Cresud.CDP.Entities;
using Empresa = Cresud.CDP.Entities.Empresa;

namespace Cresud.CDP.Admin
{
    public class EmpresaAdmin : BaseAdmin<int, Empresa, Dtos.Empresa, FilterBase>
    {
        #region Base

        public override Empresa ToEntity(Dtos.Empresa dto)
        {
            var entity = default(Empresa);
            var organizacion = (OrganizacionVenta) Enum.Parse(typeof (OrganizacionVenta), dto.IdSapOrganizacionDeVenta);
            var idSapOrganizacionDeVenta = (int)organizacion;
            var idCliente = dto.IdCliente.ToString();
            var grupoEmpresa = CdpContext.GrupoEmpresas.Single(g => g.Id == dto.GrupoEmpresaId);
            var cliente = CdpContext.Clientes.Single(g => string.Equals(g.Id, idCliente) && g.IdSapOrganizacionDeVenta == idSapOrganizacionDeVenta);
            var proveedor = CdpContext.Proveedores.Single(g => string.Equals(g.NumeroDocumento, cliente.Cuit) && g.IdSapOrganizacionDeVenta == idSapOrganizacionDeVenta);

            if (!dto.Id.HasValue)
            {                                              
                entity = new Empresa
                {                  
                    GrupoEmpresa = grupoEmpresa,
                    Descripcion = dto.Descripcion,
                    IdCliente = dto.IdCliente,
                    IdSapCanalExpor = dto.IdSapCanalExpor,
                    IdSapCanalLocal = dto.IdSapCanalLocal,
                    IdSapMoneda = dto.IdSapMoneda,
                    IdSapOrganizacionDeVenta = idSapOrganizacionDeVenta.ToString(),
                    IdSapSector = dto.IdSapSector,
                    SapId = proveedor.SapId
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
                entity.IdSapOrganizacionDeVenta = idSapOrganizacionDeVenta.ToString();
                entity.IdSapSector = dto.IdSapSector;
                entity.SapId = proveedor.SapId;
            }

            return entity;
        }

        public override void Validate(Dtos.Empresa dto)
        {          
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
