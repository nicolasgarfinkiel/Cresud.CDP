using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Cresud.CDP.Dtos.Common;
using Cresud.CDP.Entities;

namespace Cresud.CDP.Admin
{
    public class GeneralAdmin : BaseAdmin<int, object, object, FilterBase>
    {
        #region Base

        public override object ToEntity(object dto)
        {
            throw  new NotImplementedException();
        }

        public override void Validate(object dto)
        {
            throw new NotImplementedException();
        }

        public override IQueryable GetQuery(FilterBase filter)
        {
            throw new NotImplementedException();
        }

        #endregion

        public object GetPaises()
        {
            var data = CdpContext.Paises.OrderBy(c => c.Descripcion).ToList();

            return Mapper.Map<IList<Pais>, IList<Dtos.Pais>>(data);
        }

        public object GetProvincias(int paisId)
        {
            var data = CdpContext.Provincias.Where(p => p.PaisId == paisId).OrderBy(c => c.Descripcion).ToList();

            return Mapper.Map<IList<Provincia>, IList<Dtos.Provincia>>(data);
        }

        public object GetLocalidades()
        {
            var data = CdpContext.Localidades.OrderBy(c => c.Descripcion).ToList();

            return Mapper.Map<IList<Localidad>, IList<Dtos.Localidad>>(data);
        }
        
        public IEnumerable<string> GetRecorridoEstablecimientoList()
        {
            return Enum.GetNames(typeof(RecorridoEstablecimiento)).OrderBy(t => t);
        }

        public IEnumerable<string> GetOrganizacionVentaList()
        {
            return Enum.GetNames(typeof(OrganizacionVenta)).OrderBy(t => t);
        }

        public IList<Dtos.TipoCarta> GetTipoCartaList()
        {
            var data = CdpContext.TipoCartas.OrderBy(t => t.Descripcion).ToList();

            return Mapper.Map<IList<TipoCarta>, IList<Dtos.TipoCarta>>(data);
        }

        public object GetEstadosAfip()
        {
             return  Cresud.CDP.Infrastructure.Enum<EstadoAfip>.ToKeyValue();
        }

        public IEnumerable<string> GetEstadosSap()
        {
            return Cresud.CDP.Infrastructure.Enum<EstadoSap>.ToKeyValue();
        }

        #region Clientes

        public PagedListResponse<Dtos.Cliente> GetClientesByFilter(FilterBase filter)
        {
            var empresa = CdpContext.Empresas.Single(e => e.Id == filter.EmpresaId);
            var idSapOrganizacionDeVenta = int.Parse(empresa.IdSapOrganizacionDeVenta);

            var query = CdpContext.Clientes.Where(c => c.IdSapOrganizacionDeVenta == idSapOrganizacionDeVenta &&
                                                      c.Id != "2000151" && c.Id != "3000352").AsQueryable();

            if (!string.IsNullOrEmpty(filter.MultiColumnSearchText))
            {
                filter.MultiColumnSearchText = filter.MultiColumnSearchText.ToLower();

                query = query.Where(r =>
                    (r.RazonSocial != null && r.RazonSocial.ToLower().Contains(filter.MultiColumnSearchText)) ||
                    (r.Cuit != null && r.Cuit.ToLower().Contains(filter.MultiColumnSearchText)) ||
                    (r.Id != null && r.Id.ToLower().Contains(filter.MultiColumnSearchText)) 
                    ).AsQueryable();
            }

            query = query.OrderBy(c => c.RazonSocial).AsQueryable();

            return new PagedListResponse<Dtos.Cliente>
            {
                Count = query.Count(),
                Data = Mapper.Map<IList<Cliente>, IList<Dtos.Cliente>>(query.Skip(filter.PageSize * (filter.CurrentPage - 1)).Take(filter.PageSize).ToList())
            };                                   
        }

        public PagedListResponse<Dtos.Cliente> GetClientesConProveedorByFilter(Dtos.Filters.FilterClientesConProveedor filter)
        {
            var organizacion = (OrganizacionVenta)Enum.Parse(typeof(OrganizacionVenta), filter.IdSapOrganizacionDeVenta);
            var idSapOrganizacionDeVenta = (int)organizacion;

            var query = CdpContext.Clientes
                        .Join(CdpContext.Proveedores, 
                        c => new {c.Cuit, c.IdSapOrganizacionDeVenta }, 
                        p => new {Cuit = p.NumeroDocumento, p.IdSapOrganizacionDeVenta}, 
                        (c, p) => c)
                        .Where(c => c.IdSapOrganizacionDeVenta == idSapOrganizacionDeVenta && !string.IsNullOrEmpty(c.Cuit)).AsQueryable();

            if (!string.IsNullOrEmpty(filter.MultiColumnSearchText))
            {
                filter.MultiColumnSearchText = filter.MultiColumnSearchText.ToLower();

                query = query.Where(r =>
                    (r.RazonSocial != null && r.RazonSocial.ToLower().Contains(filter.MultiColumnSearchText)) ||
                    (r.Cuit != null && r.Cuit.ToLower().Contains(filter.MultiColumnSearchText)) ||
                    (r.Id != null && r.Id.ToLower().Contains(filter.MultiColumnSearchText))
                    ).AsQueryable();
            }

            query = query.OrderBy(c => c.RazonSocial).AsQueryable();

            return new PagedListResponse<Dtos.Cliente>
            {
                Count = query.Count(),
                Data = Mapper.Map<IList<Cliente>, IList<Dtos.Cliente>>(query.Skip(filter.PageSize * (filter.CurrentPage - 1)).Take(filter.PageSize).ToList())
            };                              
        }

        public IList<Dtos.ClienteRemitenteComercial> GetClientesRemitenteComercial(int empresaId)
        {
            var data = CdpContext.ClientesRemitenteComercial
                        .Where(c => c.EmpresaId == empresaId)
                        .OrderBy(c => c.RazonSocial)
                        .ToList();

            return Mapper.Map<IList<ClienteRemitenteComercial>, IList<Dtos.ClienteRemitenteComercial>>(data);
        }

        public IList<Dtos.ClienteCorredor> GetClientesCorredor(int empresaId)
        {
            var data = CdpContext.ClientesCorredor
                        .Where(c => c.EmpresaId == empresaId)
                        .OrderBy(c => c.RazonSocial)
                        .ToList();

            return Mapper.Map<IList<ClienteCorredor>, IList<Dtos.ClienteCorredor>>(data);
        }

        public IList<Dtos.ClienteEntregador> GetClientesEntregador(int empresaId)
        {
            var data = CdpContext.ClientesEntregador
                        .Where(c => c.EmpresaId == empresaId)
                        .OrderBy(c => c.RazonSocial)
                        .ToList();

            return Mapper.Map<IList<ClienteEntregador>, IList<Dtos.ClienteEntregador>>(data);
        }

        public IList<Dtos.ClienteDestinatario> GetClientesDestinatario(int empresaId)
        {
            var data = CdpContext.ClientesDestinatario
                        .Where(c => c.EmpresaId == empresaId)
                        .OrderBy(c => c.RazonSocial)
                        .ToList();

            return Mapper.Map<IList<ClienteDestinatario>, IList<Dtos.ClienteDestinatario>>(data);
        }

        public IList<Dtos.ClienteIntermediario> GetClientesIntermediarios(int empresaId)
        {
            var data = CdpContext.ClientesIntermediarios
                        .Where(c => c.EmpresaId == empresaId)
                        .OrderBy(c => c.RazonSocial)
                        .ToList();

            return Mapper.Map<IList<ClienteIntermediario>, IList<Dtos.ClienteIntermediario>>(data);
        }

        #endregion

        #region Proveedores

        public PagedListResponse<Dtos.Proveedor> GetProveedoresByFilter(FilterBase filter)
        {
            var empresa = CdpContext.Empresas.Single(e => e.Id == filter.EmpresaId);
            var idSapOrganizacionDeVenta = int.Parse(empresa.IdSapOrganizacionDeVenta);

            var query = CdpContext.Proveedores.Where(c => c.IdSapOrganizacionDeVenta == idSapOrganizacionDeVenta).AsQueryable();

            if (!string.IsNullOrEmpty(filter.MultiColumnSearchText))
            {
                filter.MultiColumnSearchText = filter.MultiColumnSearchText.ToLower();

                query = query.Where(r =>
                    (r.Nombre != null && r.Nombre.ToLower().Contains(filter.MultiColumnSearchText)) ||
                    (r.NumeroDocumento != null && r.NumeroDocumento.ToLower().Contains(filter.MultiColumnSearchText))                     
                    ).AsQueryable();
            }

            query = query.OrderBy(c => c.Nombre).AsQueryable();

            return new PagedListResponse<Dtos.Proveedor>
            {
                Count = query.Count(),
                Data = Mapper.Map<IList<Proveedor>, IList<Dtos.Proveedor>>(query.Skip(filter.PageSize * (filter.CurrentPage - 1)).Take(filter.PageSize).ToList())
            };
        }

        #endregion


      
    }
}
