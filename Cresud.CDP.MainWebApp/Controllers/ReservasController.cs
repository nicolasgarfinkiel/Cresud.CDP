using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cresud.CDP.Admin;
using Cresud.CDP.Dtos;
using Cresud.CDP.Dtos.Common;
using Cresud.CDP.Dtos.Filters;
using Cresud.CDP.Infrastructure;
using Solicitud = Cresud.CDP.Entities.Solicitud;

namespace Cresud.CDP.MainWebApp.Controllers
{
     [Authorize(Roles = "Alta Solicitud, Reservas")]
    public class ReservasController : BaseController<ReservasAdmin, int, Solicitud, Dtos.Solicitud, FilterReservas>
    {
        #region Base
        public ActionResult Index()
        {
            return View();
        }    
       
        public override object GetDataList()
        {
            return new
            {
                CartasDePorteDisponibles =  new LotesAdmin().GetCartasDePorteDisponibles(CDPSession.Current.Usuario.CurrentEmpresa.GrupoEmpresa.Id.Value),               
            };
        }

        public override object GetDataEdit()
        {
            return new
            {
                TipoCartaList = new GeneralAdmin().GetTipoCartaList().Where(t => t.Activo && (string.Equals(t.Descripcion, "Venta de granos propios") || string.Equals(t.Descripcion, "Traslado de granos") || string.Equals(t.Descripcion, "Canje") ))
            };
        }
        #endregion        

        [HttpPost]
        public ActionResult Cancelar(int solicitudId)
        {
            var response = new Response<object> { Result = new Result() { HasErrors = false, Messages = new List<string>() } };

            try
            {
                _admin.Cancelar(solicitudId);
            }
            catch (Exception ex)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(ex.Message);
            }

            return this.JsonNet(response);
        }

        [HttpPost]
        public ActionResult Anular(int solicitudId)
        {
            var response = new Response<object> { Result = new Result() { HasErrors = false, Messages = new List<string>() } };

            try
            {
                _admin.Anular(solicitudId);
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