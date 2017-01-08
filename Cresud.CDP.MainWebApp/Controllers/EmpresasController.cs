using System.Linq;
using System.Web.Mvc;
using Cresud.CDP.Admin;
using Cresud.CDP.Dtos;
using Cresud.CDP.Dtos.Common;
using Empresa = Cresud.CDP.Entities.Empresa;

namespace Cresud.CDP.MainWebApp.Controllers
{
    [Authorize]
    public class EmpresasController : BaseController<EmpresaAdmin, int, Empresa, Dtos.Empresa, FilterBase>
    {
        public ActionResult Index()
        {
            return View();
        }    

        [ChildActionOnly]
        public ActionResult EmpresasCurrentUser()
        {
            return PartialView();
        }

        public ActionResult SelectEmpresa(int empresaId)
        {
            CDPSession.Current.Usuario.CurrentEmpresa = CDPSession.Current.Usuario.Empresas.Single(e => e.Id == empresaId);

            return RedirectToAction("Index", "Home");
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
               OrganizacionVentaList = generalAdmin.GetOrganizacionVentaList()
            };
        }
    }
}