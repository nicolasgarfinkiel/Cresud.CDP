using System;
using System.Data.Entity;
using System.Web;
using AutoMapper;
using Cresud.CDP.EFRepositories;
using System.Linq;

namespace Cresud.CDP.Admin
{
    public abstract class BaseAdmin<TD, TE>
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

        public virtual TD GetById(int id)
        {

                   var dbSet = CdpContext.Set(typeof (TE)).;

        }

        public virtual TD Create(TD dto)
        {
            Validate();
            var entity = ToEntity(dto);

            CdpContext.Set(typeof(TE)).Add(entity);
            CdpContext.SaveChanges();

            return Mapper.Map<TE, TD>(entity);
        }

        public virtual TD Update(TD dto)
        {
            Validate();
            var entity = ToEntity(dto);            
            CdpContext.SaveChanges();

            return Mapper.Map<TE, TD>(entity);
        }


        public abstract TE ToEntity(TD dto);
        public abstract void Validate();
    }
}
