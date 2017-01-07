using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Cresud.CDP.Dtos;
using Cresud.CDP.Dtos.Common;
using Cosecha = Cresud.CDP.Entities.Cosecha;
using Grano = Cresud.CDP.Entities.Grano;

namespace Cresud.CDP.Admin
{
    public class GranosAdmin : BaseAdmin<int, Entities.Grano, Dtos.Grano, FilterBase>
    {
        #region Base

        public override Grano ToEntity(Dtos.Grano dto)
        {
            var entity = default(Grano);

            if (!dto.Id.HasValue)
            {
                var grupoEmpresa = CdpContext.GrupoEmpresas.Single(g => g.Id == dto.GrupoEmpresaId);
                var cosecha = CdpContext.Cosechas.Single(g => g.Id == dto.CosechaAfipId);
                var especie = CdpContext.Especies.Single(g => g.Id == dto.EspecieAfipId);
                var tipoGrano = CdpContext.TiposGrano.Single(g => g.Id == dto.TipoGranoAfipId);

                entity = new Grano
                {                    
                    CreateDate = DateTime.Now,
                    CreatedBy = UsuarioLogged,                 
                    Enabled = true,                    
                    GrupoEmpresa = grupoEmpresa,                    
                    CosechaAfip = cosecha,
                    Descripcion = dto.Descripcion,
                    EspecieAfip = especie,
                    IdMaterialSap = dto.IdMaterialSap,
                    SujetoALote = dto.SujetoALote,
                    TipoGranoAfip = tipoGrano
                };
            }
            else
            {
                entity = CdpContext.Granos.Single(c => c.Id == dto.Id.Value);
                var cosecha = CdpContext.Cosechas.Single(g => g.Id == dto.CosechaAfipId);
                var especie = CdpContext.Especies.Single(g => g.Id == dto.EspecieAfipId);
                var tipoGrano = CdpContext.TiposGrano.Single(g => g.Id == dto.TipoGranoAfipId);

                entity.UpdateDate = DateTime.Now;
                entity.UpdatedBy = UsuarioLogged;
                entity.UpdatedBy = UsuarioLogged;
                entity.CosechaAfip = cosecha;
                entity.Descripcion = dto.Descripcion;
                entity.EspecieAfip = especie;
                entity.IdMaterialSap = dto.IdMaterialSap;
                entity.SujetoALote = dto.SujetoALote;
                entity.TipoGranoAfip = tipoGrano;
            }

            return entity;
        }

        public override void Validate(Dtos.Grano dto)
        {          
        }

        public override IQueryable GetQuery(FilterBase filter)
        {
            var result = CdpContext.Granos.Where(c => c.GrupoEmpresa.Id == filter.IdGrupoEmpresa).OrderBy(c => c.Descripcion).AsQueryable();
            var cresud = filter.IdGrupoEmpresa == App.IdGrupoCresud;            

            if (!string.IsNullOrEmpty(filter.MultiColumnSearchText))
            {
                filter.MultiColumnSearchText = filter.MultiColumnSearchText.ToLower();

                result = result.Where(r =>
                    (r.Descripcion != null && r.Descripcion.ToLower().Contains(filter.MultiColumnSearchText)) ||
                    (r.SujetoALote != null && r.SujetoALote.ToLower().Contains(filter.MultiColumnSearchText)) ||
                    (r.CreatedBy != null && r.CreatedBy.ToLower().Contains(filter.MultiColumnSearchText)) ||
                    (cresud && r.EspecieAfip != null && r.EspecieAfip.Descripcion.ToLower().Contains(filter.MultiColumnSearchText)) ||
                    (cresud && r.CosechaAfip != null && r.CosechaAfip.Descripcion.ToLower().Contains(filter.MultiColumnSearchText)) ||
                    (cresud && r.TipoGranoAfip != null && r.TipoGranoAfip.Descripcion.ToLower().Contains(filter.MultiColumnSearchText))                     
                    ).AsQueryable();                
            }

            return result;
        }

        #endregion

        public object GetCosechas()
        {
            var data = CdpContext.Cosechas.OrderBy(c => c.Descripcion).ToList();

            return Mapper.Map<IList<Cosecha>, IList<Dtos.Cosecha>>(data);
        }

        public object GetEspecies()
        {
            var data = CdpContext.Especies.OrderBy(c => c.Descripcion).ToList();

            return Mapper.Map<IList<Entities.Especie>, IList<Dtos.Especie>>(data);
        }

        public object GetTiposGrano()
        {
            var data = CdpContext.TiposGrano.OrderBy(c => c.Descripcion).ToList();

            return Mapper.Map<IList<Entities.TipoGrano>, IList<Dtos.TipoGrano>>(data);
        }
    }
}
