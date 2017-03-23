using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cresud.CDP.Admin;
using Cresud.CDP.Dtos;
using Cresud.CDP.Dtos.Common;
using Cresud.CDP.Infrastructure;
using Solicitud = Cresud.CDP.Entities.Solicitud;

namespace Cresud.CDP.MainWebApp.Controllers
{
    [Authorize(Roles = "Alta Solicitud, Visualizacion Solicitud")]
    public class SolicitudesController : BaseController<SolicitudesAdmin, int, Solicitud, Dtos.SolicitudEdit, FilterBase>
    {
        #region Base
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
            var granosAdmin = new GranosAdmin();

            return new
            {
                TipoCartaList = generalAdmin.GetTipoCartaList().Where(t => t.Activo),
                Granos = granosAdmin.GetAll().OrderBy(g => g.Descripcion),
                ClienteDefault = generalAdmin.GetClienteById(CDPSession.Current.Usuario.CurrentEmpresa.IdCliente.ToString())
            };
        }

        [HttpPost]
        public ActionResult UpdateSimple(Solicitud solicitud)
        {
            var response = new Response<object> { Result = new Result() { HasErrors = false, Messages = new List<string>() } };

            try
            {
                _admin.UpdateSimple(solicitud);
            }
            catch (Exception ex)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(ex.Message);
            }

            return this.JsonNet(response);
        }

        [HttpPost]
        public ActionResult ReenviarSap(int id)
        {
            var response = new Response<object> { Result = new Result() { HasErrors = false, Messages = new List<string>() } };

            try
            {
                _admin.ReenviarSap(id);
            }
            catch (Exception ex)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(ex.Message);
            }

            return this.JsonNet(response);
        }

        [HttpPost]
        public ActionResult ReenviarAfip(int id)
        {
            var response = new Result() {HasErrors = false, Messages = new List<string>()};

            try
            {
              response = _admin.ReenviarAfip(id);
            }
            catch (Exception ex)
            {
                response.HasErrors = true;
                response.Messages.Add(ex.Message);
            }

            return this.JsonNet(response);
        }

        #endregion        
    }
}