using System;
using System.Linq;
using AutoMapper;
using Cresud.CDP.Dtos.Filters;
using Solicitud = Cresud.CDP.Entities.Solicitud;

namespace Cresud.CDP.Admin
{
    public class ReservasAdmin : BaseAdmin<int, Entities.Solicitud, Dtos.Solicitud, FilterReservas>
    {
        #region Base

        public override Dtos.Solicitud Create(Dtos.Solicitud dto)
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

            var entity = new Solicitud
            {
                EstablecimientoProcedenciaId = establecimiento.Id,
                ObservacionAfip = "Reserva de Carta de Porte",
                NumeroCartaDePorte = cartaDePorte.NumeroCartaDePorte,
                Cee = cartaDePorte.NumeroCee,
                CreateDate = DateTime.Now,
                CreatedBy = UsuarioLogged,
                TipoDeCartaId = dto.TipoDeCartaId,
                EmpresaId = empresa.Id
            };

            entity.SetDefaultValues();
            CdpContext.Solicitudes.Add(entity);
            CdpContext.SaveChanges();

            return Mapper.Map<Solicitud, Dtos.Solicitud>(entity);
        }

        public override Solicitud ToEntity(Dtos.Solicitud dto)
        {
            return null;
        }

        public override void Validate(Dtos.Solicitud dto)
        {
        }

        public override IQueryable GetQuery(FilterReservas filter)
        {
            var result = (from solicitud in CdpContext.Solicitudes
                          join cdp in CdpContext.CartaDePortes on solicitud.NumeroCartaDePorte equals cdp.NumeroCartaDePorte
                          where string.IsNullOrEmpty(solicitud.Ctg) &&
                          solicitud.EmpresaId == filter.EmpresaId &&
                          cdp.GrupoEmpresaId == filter.IdGrupoEmpresa &&
                          ((filter.AsignadasAMi && string.Equals(cdp.UsuarioReserva, UsuarioLogged)) || (!filter.AsignadasAMi && !string.IsNullOrEmpty(cdp.UsuarioReserva)))
                          select solicitud).OrderBy(s => s.NumeroCartaDePorte).AsQueryable();

            return result;
        }

        #endregion

        public void Cancelar(int solicitudId)
        {
            var solicitud = CdpContext.Solicitudes.Single(c => c.Id == solicitudId);
            var cartaDePorte = CdpContext.CartaDePortes.Single(c => string.Equals(c.NumeroCartaDePorte, solicitud.NumeroCartaDePorte));

            cartaDePorte.Estado = 0;
            cartaDePorte.FechaReserva = null;
            cartaDePorte.UsuarioReserva = null;

            CdpContext.Solicitudes.Remove(solicitud);
            CdpContext.SaveChanges();
        }

        public void Anular(int solicitudId)
        {
            var solicitud = CdpContext.Solicitudes.Single(c => c.Id == solicitudId);
            var cartaDePorte = CdpContext.CartaDePortes.Single(c => string.Equals(c.NumeroCartaDePorte, solicitud.NumeroCartaDePorte));

            solicitud.EstadoEnSAP = 4;
            solicitud.EstadoEnAFIP = 3;
            solicitud.ObservacionAfip = "Reserva de Carta de Porte ANULADA";

            cartaDePorte.FechaReserva = null;
            cartaDePorte.UsuarioReserva = null;

            CdpContext.SaveChanges();
        }
    }
}


