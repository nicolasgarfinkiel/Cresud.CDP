using System;
using System.Collections.Generic;
using System.Web;
using AutoMapper;
using Cresud.CDP.Dtos.Common;
using Cresud.CDP.EFRepositories;
using System.Linq;

namespace Cresud.CDP.Admin
{
    public abstract class BaseAdmin<TID, TE, TD, TF> where TF: FilterBase
    {
        public readonly CDPContext CdpContext;
        public string UsuarioLogged { get; set; }

        public BaseAdmin()
        {
            CdpContext = new CDPContext();

            try
            {
                if (HttpContext.Current != null && HttpContext.Current.Request.IsAuthenticated)
                {
                    UsuarioLogged = HttpContext.Current.User.Identity.Name;
                }
            }
            catch (Exception)
            {
            }

        }

        public virtual TD GetById(TID id)
        {
            var entity = (TE)CdpContext.Set(typeof(TE)).Find(id);
            return Mapper.Map<TE, TD>(entity);
        }

        public virtual IList<TD> GetAll()
        {
             var entities = (IList<TE>)CdpContext.Set(typeof(TE)).AsQueryable().OfType<TE>().ToList();
             return Mapper.Map<IList<TE>, IList<TD>>(entities);
        }

        public virtual PagedListResponse<TD> GetByFilter(TF filter)
        {          
            var query = GetQuery(filter).OfType<TE>();
            
            return new PagedListResponse<TD>
            {
                Count = query.Count(),
                Data = Mapper.Map<IList<TE>, IList<TD>>(query.Skip(filter.PageSize * (filter.CurrentPage - 1)).Take(filter.PageSize).ToList())
            };
        }

        public virtual TD Create(TD dto)
        {
            Validate(dto);
            var entity = ToEntity(dto);

            CdpContext.Set(typeof(TE)).Add(entity);
            CdpContext.SaveChanges();

            return Mapper.Map<TE, TD>(entity);
        }

        public virtual TD Update(TD dto)
        {
            Validate(dto);
            var entity = ToEntity(dto);
            CdpContext.SaveChanges();

            return Mapper.Map<TE, TD>(entity);
        }

        #region Abstract Methods

        public abstract TE ToEntity(TD dto);
        public abstract void Validate(TD dto);
        public abstract IQueryable GetQuery(TF filter);

        #endregion
    }
}
