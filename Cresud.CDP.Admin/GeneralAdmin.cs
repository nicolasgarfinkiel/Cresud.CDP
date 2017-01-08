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
    }
}
