using System;
using System.Linq;
using AutoMapper;
using Cresud.CDP.Dtos.Common;
using Cresud.CDP.Entities;

namespace Cresud.CDP.Admin
{
    public class SolicitudesAdmin : BaseAdmin<int, Entities.Solicitud, Dtos.SolicitudEdit, FilterBase>
    {
        #region Base

        public override Dtos.SolicitudEdit GetById(int id)
        {
            var entity = CdpContext.Solicitudes.Single(s => s.Id == id);
            var dto = Mapper.Map<Entities.Solicitud, Dtos.SolicitudEdit>(entity);

            if (entity.ProveedorTitularCartaDePorteId.HasValue)
            {
                dto.ProveedorTitularCartaDePorte = Mapper.Map<Entities.Proveedor, Dtos.Proveedor>(
                CdpContext.Proveedores.FirstOrDefault(e => e.Id == entity.ProveedorTitularCartaDePorteId.Value));
            }

            if (entity.ClienteIntermediarioId.HasValue)
            {
                var clienteId = entity.ClienteIntermediarioId.Value.ToString();
                dto.ClienteIntermediario = Mapper.Map<Entities.Cliente, Dtos.Cliente>(
                CdpContext.Clientes.FirstOrDefault(e => string.Equals(e.Id, clienteId)));
            }

            if (entity.ClienteRemitenteComercialId.HasValue)
            {
                var clienteId = entity.ClienteRemitenteComercialId.Value.ToString();
                dto.ClienteRemitenteComercial = Mapper.Map<Entities.Cliente, Dtos.Cliente>(
                CdpContext.Clientes.FirstOrDefault(e => string.Equals(e.Id, clienteId)));
            }

            if (entity.ClienteCorredorId.HasValue )
            {
                var clienteId = entity.ClienteCorredorId.Value.ToString();
                dto.ClienteCorredor = Mapper.Map<Entities.Cliente, Dtos.Cliente>(
                CdpContext.Clientes.FirstOrDefault(e => string.Equals(e.Id, clienteId)));
            }

            if (entity.ClienteEntregadorId.HasValue)
            {
                var clienteId = entity.ClienteEntregadorId.Value.ToString();
                dto.ClienteEntregador = Mapper.Map<Entities.Cliente, Dtos.Cliente>(
                CdpContext.Clientes.FirstOrDefault(e => string.Equals(e.Id, clienteId)));
            }

            if (entity.ClienteDestinatarioId.HasValue)
            {
                var clienteId = entity.ClienteDestinatarioId.Value.ToString();
                dto.ClienteDestinatario = Mapper.Map<Entities.Cliente, Dtos.Cliente>(
                CdpContext.Clientes.FirstOrDefault(e => string.Equals(e.Id, clienteId)));
            }

            if (entity.ClienteDestinoId.HasValue)
            {
                var clienteId = entity.ClienteDestinoId.Value.ToString();
                dto.ClienteDestino = Mapper.Map<Entities.Cliente, Dtos.Cliente>(
                CdpContext.Clientes.FirstOrDefault(e => string.Equals(e.Id, clienteId)));
            }

            if (entity.ProveedorTransportistaId.HasValue)
            {
                dto.ProveedorTransportista = Mapper.Map<Entities.Proveedor, Dtos.Proveedor>(
                CdpContext.Proveedores.FirstOrDefault(e => e.Id == entity.ProveedorTransportistaId.Value));
            }

            if (entity.ChoferTransportistaId.HasValue)
            {
                dto.ChoferTransportista = Mapper.Map<Entities.Chofer, Dtos.Chofer>(
                CdpContext.Choferes.FirstOrDefault(e => e.Id == entity.ChoferTransportistaId.Value));
            }

            if (entity.ChoferId.HasValue)
            {
                dto.Chofer = Mapper.Map<Entities.Chofer, Dtos.Chofer>(
                CdpContext.Choferes.FirstOrDefault(e => e.Id == entity.ChoferId.Value));
            }

            if (entity.GranoId.HasValue)
            {
                dto.Grano = Mapper.Map<Entities.Grano, Dtos.Grano>(
                CdpContext.Granos.FirstOrDefault(e => e.Id == entity.GranoId.Value));
            }

            if (entity.EstablecimientoProcedenciaId.HasValue)
            {
                dto.EstablecimientoProcedencia = Mapper.Map<Entities.Establecimiento, Dtos.Establecimiento>(
                CdpContext.Establecimientos.FirstOrDefault(e => e.Id == entity.EstablecimientoProcedenciaId.Value));
                dto.EstablecimientoProcedencia.LocalidadDescripcion = Mapper.Map<Entities.Localidad, Dtos.Localidad>(CdpContext.Localidades.Single(e => e.Id == dto.EstablecimientoProcedencia.LocalidadId)).Descripcion;
            }

            if (entity.EstablecimientoDestinoId.HasValue)
            {
                dto.EstablecimientoDestino = Mapper.Map<Entities.Establecimiento, Dtos.Establecimiento>(
                CdpContext.Establecimientos.FirstOrDefault(e => e.Id == entity.EstablecimientoDestinoId.Value));
                dto.EstablecimientoDestino.LocalidadDescripcion = Mapper.Map<Entities.Localidad, Dtos.Localidad>(CdpContext.Localidades.Single(e => e.Id == dto.EstablecimientoDestino.LocalidadId)).Descripcion;
            }

            if (entity.ClientePagadorDelFleteId.HasValue)
            {
                var clienteId = entity.ClientePagadorDelFleteId.Value.ToString();
                dto.ClientePagadorDelFlete = Mapper.Map<Entities.Cliente, Dtos.Cliente>(
                CdpContext.Clientes.FirstOrDefault(e => string.Equals(e.Id, clienteId)));
            }

            return dto;
        }

        public override Dtos.SolicitudEdit Create(Dtos.SolicitudEdit dto)
        {
            dto.SetIds();
            var entity = Mapper.Map<Dtos.SolicitudEdit, Solicitud>(dto);
            entity.CreateDate = DateTime.Now;
            entity.CreatedBy = UsuarioLogged;                        

            if (!dto.Id.HasValue && (dto.TipoDeCartaId != 4 && dto.TipoDeCartaId != 2 && dto.TipoDeCartaId != 7))
            {
                var establecimiento = CdpContext.Establecimientos.Single(e => e.Id == dto.EstablecimientoProcedenciaId);
                var establecimientoId = establecimiento.Id.ToString();
                var empresa = CdpContext.Empresas.Single(e => e.Id == dto.EmpresaId);

                var cartaDePorte = (
                    from lote in CdpContext.LotesCartaPorte
                    join cdp in CdpContext.CartaDePortes on lote.Id equals cdp.Lote.Id
                    where cdp.GrupoEmpresaId == empresa.GrupoEmpresa.Id &&
                          cdp.Estado == 0 &&
                          lote.FechaVencimiento >= DateTime.Now &&
                          (!establecimiento.AsociaCartaDePorte || lote.EstablecimientoOrigenId == establecimientoId)
                    orderby lote.FechaVencimiento, cdp.NumeroCartaDePorte
                    select cdp
                    ).FirstOrDefault();

                if (cartaDePorte == null)
                    throw new Exception("No Hay Cartas de porte disponibles para el establecimiento seleccionado");

                cartaDePorte.Estado = 1;
                cartaDePorte.FechaReserva = DateTime.Now;
                cartaDePorte.UsuarioReserva = UsuarioLogged;

                entity.NumeroCartaDePorte = cartaDePorte.NumeroCartaDePorte;
                entity.Cee = cartaDePorte.NumeroCee;
            }

            //TODO: Afip. Responsabilidad de sposzalksi.
            if (dto.Enviar)
            {
                
            }
         
            CdpContext.Solicitudes.Add(entity);
            CdpContext.SaveChanges();

            return Mapper.Map<Solicitud, Dtos.SolicitudEdit>(entity);
        }

        public override Solicitud ToEntity(Dtos.SolicitudEdit dto)
        {
            return null;
        }

        public override void Validate(Dtos.SolicitudEdit dto)
        {
            if (!dto.Id.HasValue && (dto.TipoDeCartaId != 4 && dto.TipoDeCartaId != 2 && dto.TipoDeCartaId != 7))
            {
                
            }
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
