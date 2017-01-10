using System.Web.Mvc;
using Cresud.CDP.Admin;
using Cresud.CDP.Dtos.Common;

namespace Cresud.CDP.MainWebApp.Controllers
{
    [Authorize]
    public class GruposEmpresaController : BaseController<GrupoEmpresaAdmin, int, Entities.GrupoEmpresa, Dtos.GrupoEmpresa, FilterBase>
    {        
        public ActionResult Index()
        {
            return View();
        }       
        
        public override object GetDataList()
        {
            return new {};
        }

        public override object GetDataEdit()
        {
            var generalAdmin = new GeneralAdmin();

            return new
            {
                Paises = generalAdmin.GetPaises()                
            };
        }
    }
}