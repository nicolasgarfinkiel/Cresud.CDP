using System;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Security;
using Cresud.CDP.Admin;
using Cresud.CDP.Dtos.Common;
using Cresud.CDP.Entities;

namespace Cresud.CDP.MainWebApp.Controllers
{
    [Authorize]
    public class HomeController : BaseController<EmpresaAdmin, int, Empresa, Dtos.Empresa, FilterBase>
    {
        public ActionResult Index()
        {
        //    Membership.ValidateUser(WindowsIdentity.GetCurrent().Name, null);             

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