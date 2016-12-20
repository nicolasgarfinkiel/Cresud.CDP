using System;
using System.Web;
using Cresud.CDP.EFRepositories;

namespace Cresud.CDP.Admin
{
    public abstract class BaseAdmin
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
    }
}
