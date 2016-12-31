using System;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Security;
using Cresud.CDP.Admin;
using Cresud.CDP.Dtos.Common;
using Cresud.CDP.Entities;

namespace Cresud.CDP.MainWebApp.Controllers
{

    public class HomeController : BaseController<EmpresaAdmin, int, Empresa, Dtos.Empresa, FilterBase>
    {
        public ActionResult Index()
        {
            try
            {
                Membership.ValidateUser(WindowsIdentity.GetCurrent().Name, null);
                var empresa = _admin.GetById(22);
            }
            catch (Exception ex)
            {
                var i = ex.Message;
            }            

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