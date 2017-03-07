using System;
using System.Linq;
using AutoMapper;
using Cresud.CDP.Dtos.Filters;
using Cresud.CDP.Entities;
using Establecimiento = Cresud.CDP.Dtos.Establecimiento;

namespace Cresud.CDP.Admin
{
    public class EstablecimientosAdmin : BaseAdmin<int, Entities.Establecimiento, Dtos.Establecimiento, FilterEstablecimientos>
    {
        #region Base

        public override Establecimiento GetById(int id)
        {
            var entity = CdpContext.Establecimientos.Single(e => e.Id == id);
            var result = Mapper.Map<Entities.Establecimiento, Establecimiento>(entity);

            if (result.InterlocutorDestinatarioId > 0)
            {
                var cid = result.InterlocutorDestinatarioId.ToString();
                result.InterlocutorDestinatario = Mapper.Map<Cliente, Dtos.Cliente>(CdpContext.Clientes.Single(c => string.Equals(c.Id, cid)));
            }
            
            return result;
        }

        public override Entities.Establecimiento ToEntity(Dtos.Establecimiento dto)
        {
            var entity = default(Entities.Establecimiento);
            var provincia = CdpContext.Provincias.Single(g => g.Id == dto.ProvinciaId);
            var recorridoEstablecimiento = !string.IsNullOrEmpty(dto.RecorridoEstablecimiento) ?
                                           (RecorridoEstablecimiento)Enum.Parse(typeof(RecorridoEstablecimiento), dto.RecorridoEstablecimiento) :
                                           default(RecorridoEstablecimiento?);

            if (!dto.Id.HasValue)
            {
                entity = new Entities.Establecimiento
                {
                    CreateDate = DateTime.Now,
                    CreatedBy = UsuarioLogged,
                    Enabled = true,
                    EmpresaId = dto.EmpresaId,
                    Descripcion = dto.Descripcion,
                    AsociaCartaDePorte = dto.AsociaCartaDePorte,
                    Direccion = dto.Direccion,
                    EstablecimientoAfip = dto.EstablecimientoAfip,
                    IdAlmacenSap = dto.IdAlmacenSap,
                    IdCEBE = dto.IdCEBE,
                    IdCentroSap = dto.IdCentroSap,
                    IdExpedicion = dto.IdExpedicion,
                    InterlocutorDestinatarioId = dto.InterlocutorDestinatarioId,
                    LocalidadId = dto.LocalidadId,
                    Provincia = provincia,
                    RecorridoEstablecimiento = recorridoEstablecimiento
                };
            }
            else
            {
                entity = CdpContext.Establecimientos.Single(c => c.Id == dto.Id.Value);

                entity.UpdateDate = DateTime.Now;
                entity.UpdatedBy = UsuarioLogged;
                entity.Descripcion = dto.Descripcion;
                entity.Descripcion = dto.Descripcion;
                entity.AsociaCartaDePorte = dto.AsociaCartaDePorte;
                entity.Direccion = dto.Direccion;
                entity.EstablecimientoAfip = dto.EstablecimientoAfip;
                entity.IdAlmacenSap = dto.IdAlmacenSap;
                entity.IdCEBE = dto.IdCEBE;
                entity.IdCentroSap = dto.IdCentroSap;
                entity.IdExpedicion = dto.IdExpedicion;
                entity.InterlocutorDestinatarioId = dto.InterlocutorDestinatarioId;
                entity.LocalidadId = dto.LocalidadId;
                entity.Provincia = provincia;
                entity.RecorridoEstablecimiento = recorridoEstablecimiento;
            }

            return entity;
        }

        public override void Validate(Dtos.Establecimiento dto)
        {
        }

        public override IQueryable GetQuery(FilterEstablecimientos filter)
        {
            var result = CdpContext.Establecimientos.Where(c => c.EmpresaId == filter.EmpresaId).OrderBy(c => c.Descripcion).AsQueryable();

            if (filter.Origen)
            {
                result = result.Where(e => e.RecorridoEstablecimiento.HasValue &&
                                           (e.RecorridoEstablecimiento.Value == RecorridoEstablecimiento.SoloOrigen ||
                                            e.RecorridoEstablecimiento.Value == RecorridoEstablecimiento.OrigenYDestino)).AsQueryable();
            }

            if (filter.Destino)
            {
                result = result.Where(e => e.RecorridoEstablecimiento.HasValue &&
                                           (e.RecorridoEstablecimiento.Value == RecorridoEstablecimiento.SoloDestino ||
                                            e.RecorridoEstablecimiento.Value == RecorridoEstablecimiento.OrigenYDestino)).AsQueryable();
            }

            if (filter.Enabled.HasValue)
            {
                result = result.Where(e => e.Enabled == filter.Enabled.Value).AsQueryable();
            }

            if (!string.IsNullOrEmpty(filter.MultiColumnSearchText))
            {
                filter.MultiColumnSearchText = filter.MultiColumnSearchText.ToLower();

                result = result.Where(r =>
                    (r.Descripcion != null && r.Descripcion.ToLower().Contains(filter.MultiColumnSearchText)) ||
                    (r.Direccion != null && r.Direccion.ToLower().Contains(filter.MultiColumnSearchText)) ||
                    (r.CreatedBy != null && r.CreatedBy.ToLower().Contains(filter.MultiColumnSearchText)) ||
                    (r.Provincia != null && r.Provincia.Descripcion.ToLower().Contains(filter.MultiColumnSearchText))
                    ).AsQueryable();
            }

            return result;
        }

        #endregion
    }
}
