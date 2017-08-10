using System;
using System.Linq;
using System.Web.Mvc;
using Cresud.CDP.Admin;
using Cresud.CDP.Dtos;
using Cresud.CDP.Dtos.Common;
using Cresud.CDP.Infrastructure;
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
        [AllowAnonymous]
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
            var grupoEmpresaAdmin = new GrupoEmpresaAdmin();

            return new
            {
               GrupoEmpresaList = grupoEmpresaAdmin.GetAll().OrderBy(g => g.Descripcion),
               OrganizacionVentaList = generalAdmin.GetOrganizacionVentaList()
            };
        }

        [HttpPost]
        public ActionResult GetByClienteId(int clienteId)
        {
            var response = new Response<Dtos.Empresa>();

            try
            {
                response.Data = _admin.GetByClienteId(clienteId);
            }
            catch (Exception ex)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(ex.Message);
            }

            return this.JsonNet(response);
        }

         [HttpPost]
        public ActionResult GetBySapId(string sapId)
        {
            var response = new Response<Dtos.Empresa>();

            try
            {
                response.Data = _admin.GetBySapId(sapId);
            }
            catch (Exception ex)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(ex.Message);
            }

            return this.JsonNet(response);
        }        
    }
}