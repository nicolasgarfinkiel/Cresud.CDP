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
                    EstablecimientoOrigen = dto.EstablecimientoOrigen,
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
                entity.EstablecimientoOrigen = dto.EstablecimientoOrigen;
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

            return result;
        }

        #endregion
    }
}
