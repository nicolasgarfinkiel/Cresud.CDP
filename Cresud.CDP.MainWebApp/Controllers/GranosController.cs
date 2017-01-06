using System.Web.Mvc;
using Cresud.CDP.Admin;
using Cresud.CDP.Dtos;
using Cresud.CDP.Dtos.Common;

namespace Cresud.CDP.MainWebApp.Controllers
{
    [Authorize]
    public class GranosController : BaseController<GranosAdmin, int, Entities.Grano, Dtos.Grano, FilterBase>
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
            return new
            {
                Cosechas = _admin.GetCosechas(),
                Especies = _admin.GetEspecies(),
                TiposGrano = _admin.GetTiposGrano(),                
            };
        }
    }
}