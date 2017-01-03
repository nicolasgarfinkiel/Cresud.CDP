using System.Web.Mvc;
using Cresud.CDP.Admin;
using Cresud.CDP.Dtos;
using Cresud.CDP.Dtos.Common;

namespace Cresud.CDP.MainWebApp.Controllers
{
    [Authorize]
    public class ChoferesController : BaseController<ChoferesAdmin, int, Entities.Chofer, Dtos.Chofer, FilterBase>
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