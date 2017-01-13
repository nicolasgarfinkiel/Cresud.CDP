using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Cresud.CDP.Dtos.Common;
using Cresud.CDP.Dtos.Filters;
using Cresud.CDP.Entities;

namespace Cresud.CDP.Admin
{
    public class CartasDePorteAdmin : BaseAdmin<int, Entities.LoteCartaPorte, Dtos.LoteCartaPorte, FilterLotesCartaPorte>
    {
        #region Base

        public override LoteCartaPorte ToEntity(Dtos.LoteCartaPorte dto)
        {
            var entity = default(LoteCartaPorte);                        

            if (!dto.Id.HasValue)
            {
                var grupoEmpresa = CdpContext.GrupoEmpresas.Single(g => g.Id == dto.GrupoEmpresaId);
                
                entity = new LoteCartaPorte
                {                   
                    CreateDate = DateTime.Now,
                    CreatedBy = UsuarioLogged,                 
                    Enabled = true,                   
                    GrupoEmpresa = grupoEmpresa,
                    Cee = dto.Cee,
                    Desde = dto.Desde,
                    EstablecimientoOrigenId = dto.EstablecimientoOrigenId,
                    FechaDesde = dto.FechaDesde,
                    FechaHasta = dto.FechaHasta,
                    FechaVencimiento = dto.FechaVencimiento,
                    HabilitacionNumero = dto.HabilitacionNumero,
                    Hasta = dto.Hasta,
                    PuntoEmision = dto.PuntoEmision,
                    Sucursal = dto.Sucursal,                     
                };
            }
            else
            {
                entity = CdpContext.LotesCartaPorte.Single(c => c.Id == dto.Id.Value);              
                entity.UpdateDate = DateTime.Now;
                entity.UpdatedBy = UsuarioLogged;
                entity.Cee = dto.Cee;
                entity.Desde = dto.Desde;
                entity.EstablecimientoOrigenId = dto.EstablecimientoOrigenId;
                entity.FechaDesde = dto.FechaDesde;
                entity.FechaHasta = dto.FechaHasta;
                entity.FechaVencimiento = dto.FechaVencimiento;
                entity.HabilitacionNumero = dto.HabilitacionNumero;
                entity.Hasta = dto.Hasta;
                entity.PuntoEmision = dto.PuntoEmision;
                entity.Sucursal = dto.Sucursal;
            }

            return entity;
        }

        public override void Validate(Dtos.LoteCartaPorte dto)
        {            
        }

        public override PagedListResponse<Dtos.LoteCartaPorte> GetByFilter(FilterLotesCartaPorte filter)
        {
            var query = GetQuery(filter).OfType<LoteCartaPorte>();            
            var dtos = Mapper.Map<IList<LoteCartaPorte>, IList<Dtos.LoteCartaPorte>>(query.Skip(filter.PageSize*(filter.CurrentPage - 1)).Take(filter.PageSize).ToList());

            dtos.ToList().ForEach(d => d.Disponibles = CdpContext.CartaDePortes.Count(c => c.Estado == 0 && c.Lote.Id == d.Id) );

            return new PagedListResponse<Dtos.LoteCartaPorte>
            {
                Count = query.Count(),
                Data = dtos
            };
        }

        public override IQueryable GetQuery(FilterLotesCartaPorte filter)
        {
            var result = CdpContext.LotesCartaPorte
                        .Where(c => c.GrupoEmpresa.Id == filter.IdGrupoEmpresa && c.Desde >= filter.Desde)
                        .OrderBy(c => c.Desde).AsQueryable();

            if (filter.TieneDisponibilidad)
            {
                result = result.Where(r => r.CartasDePorte.Any(c => c.Estado == 0)).AsQueryable();
            }

            if (filter.Vigente)
            {
                result = result.Where(r =>r.FechaVencimiento >= DateTime.Now ).AsQueryable();
            }

            return result;
        }

        public override void Delete(int id)
        {
            var dto = GetById(id);
            var entity = CdpContext.LotesCartaPorte.Single(l => l.Id == id);
            dto.Disponibles = CdpContext.CartaDePortes.Count(c => c.Estado == 0 && c.Lote.Id == id);

            if (dto.Cantidad == dto.Disponibles)
            {
                var cdpIds = entity.CartasDePorte.Select(c => c.Id).ToList();

                cdpIds.ForEach(c =>
                {
                    var cdp = entity.CartasDePorte.Single(cc => cc.Id == c);
                    CdpContext.CartaDePortes.Remove(cdp);
                });

                CdpContext.LotesCartaPorte.Remove(entity);
            }
            else
            {
                var maxNroCartaPorte = entity.CartasDePorte.Where(c => c.Estado == 1).Select(c => int.Parse(c.NumeroCartaDePorte)).Max();
                var cdpIds = entity.CartasDePorte.Where(c => c.Estado == 0).Select(c => c.Id).ToList();

                cdpIds.ForEach(c =>
                {
                    var cdp = entity.CartasDePorte.Single(cc => cc.Id == c);
                    CdpContext.CartaDePortes.Remove(cdp);
                });

                entity.Hasta = maxNroCartaPorte;

                var log1 = new LogOperacion
                {
                   Tabla = "CartasDePorte",
                   Accion = "DELETE DISP",
                   ReferenciaId = id,
                   CreateDate = DateTime.Now,
                   CreatedBy = UsuarioLogged
                };

                var log2 = new LogOperacion
                {
                   Tabla = "LoteCartasDePorte",
                   Accion = "DELETE DISP",
                   ReferenciaId = id,
                   CreateDate = DateTime.Now,
                   CreatedBy = UsuarioLogged
                };

                CdpContext.LogOperaciones.Add(log1);
                CdpContext.LogOperaciones.Add(log2);
            }

            CdpContext.SaveChanges();
        }

        public override Dtos.LoteCartaPorte GetById(int id)
        {
            var entity = CdpContext.LotesCartaPorte.Single(d => d.Id == id);
            var dto = Mapper.Map<LoteCartaPorte, Dtos.LoteCartaPorte>(entity);

            if (!string.IsNullOrEmpty(dto.EstablecimientoOrigenId))
            {
                var establecimientoId = int.Parse(dto.EstablecimientoOrigenId);
                var establecimiento = CdpContext.Establecimientos.Single(e => e.Id == establecimientoId);
                dto.EstablecimientoOrigenDescripcion = establecimiento.Descripcion;
            }
                        
            return dto;
        }

        #endregion
    }
}
