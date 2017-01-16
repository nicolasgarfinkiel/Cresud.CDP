using System;
using System.Linq;
using Cresud.CDP.Dtos.Common;
using Cresud.CDP.Entities;

namespace Cresud.CDP.Admin
{
    public class SolicitudesAdmin : BaseAdmin<int, Entities.Solicitud, Dtos.Solicitud, FilterBase>
    {
        #region Base

        public override Solicitud ToEntity(Dtos.Solicitud dto)
        {
            var entity = default(Solicitud);

            //if (!dto.Id.HasValue)
            //{
            //    var grupoEmpresa = CdpContext.GrupoEmpresas.Single(g => g.Id == dto.GrupoEmpresaId);

            //    entity = new Chofer
            //    {
            //        Acoplado = dto.Acoplado,
            //        Apellido = dto.Apellido,
            //        Camion = dto.Camion,
            //        CreateDate = DateTime.Now,
            //        CreatedBy = UsuarioLogged,
            //        Cuit = dto.Cuit,
            //        Domicilio = dto.Domicilio,
            //        Enabled = true,
            //        EsChoferTransportista = dto.EsChoferTransportista,
            //        GrupoEmpresa = grupoEmpresa,
            //        Marca = dto.Marca,
            //        Nombre = dto.Nombre
            //    };
            //}
            //else
            //{
            //    entity = CdpContext.Choferes.Single(c => c.Id == dto.Id.Value);
            //    entity.Acoplado = dto.Acoplado;
            //    entity.Apellido = dto.Apellido;
            //    entity.Camion = dto.Camion;
            //    entity.Cuit = dto.Cuit;
            //    entity.Domicilio = dto.Domicilio;
            //    entity.EsChoferTransportista = dto.EsChoferTransportista;
            //    entity.Marca = dto.Marca;
            //    entity.Nombre = dto.Nombre;
            //    entity.UpdateDate = DateTime.Now;
            //    entity.UpdatedBy = UsuarioLogged;
            //}

            return entity;
        }

        public override void Validate(Dtos.Solicitud dto)
        {            
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
