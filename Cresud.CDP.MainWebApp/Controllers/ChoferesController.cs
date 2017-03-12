using System.Web.Mvc;
using Cresud.CDP.Admin;
using Cresud.CDP.Dtos;
using Cresud.CDP.Dtos.Common;
using Cresud.CDP.Dtos.Filters;

namespace Cresud.CDP.MainWebApp.Controllers
{
    [Authorize(Roles = "Administracion")]
    public class ChoferesController : BaseController<ChoferesAdmin, int, Entities.Chofer, Dtos.Chofer, FilterChoferes>
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
            return new { };
        }
    }
}