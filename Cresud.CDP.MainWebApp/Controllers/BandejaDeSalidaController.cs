using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Cresud.CDP.Admin;
using Cresud.CDP.Dtos;
using Cresud.CDP.Dtos.Common;
using Cresud.CDP.Dtos.Filters;
using Cresud.CDP.Infrastructure;
using SolicitudReport = Cresud.CDP.Entities.SolicitudReport;

namespace Cresud.CDP.MainWebApp.Controllers
{
    [Authorize]
    public class BandejaDeSalidaController : BaseController<ReportesAdmin, int, SolicitudReport, Dtos.SolicitudReport, FilterBase>
    {
        private readonly GeneralAdmin _generalAdmin;
        private readonly GranosAdmin _granosAdmin;

        public BandejaDeSalidaController()
        {
            _generalAdmin = new GeneralAdmin();
            _granosAdmin = new GranosAdmin();
        }

        #region Base

        public ActionResult Index()
        {
            return View();
        }

        public override object GetDataList()
        {
            return new { };
        }

        public override object GetDataEdit()
        {
            return new { };
        }

        #endregion

        #region Solicitadas

        public ActionResult GetDataListSolicitadas()
        {
            var response = new Response<object> { Result = new Result() { HasErrors = false, Messages = new List<string>() } };

            try
            {
                response.Data = new
                {
                    Usuario = CDPSession.Current.Usuario,
                    EstadosAfip = _generalAdmin.GetEstadosAfip(),
                    EstadosSap = _generalAdmin.GetEstadosSap(),
                };
            }
            catch (Exception ex)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(ex.Message);
            }

            return this.JsonNet(response);
        }

        [HttpPost]
        public ActionResult GetSolicitadasByFilter(FilterSolicitudes filter)
        {
            var response = new PagedListResponse<Dtos.SolicitudReport>();

            try
            {
                response = _admin.GetSolicitadasByFilter(filter);
            }
            catch (Exception ex)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(ex.Message);
            }

            return this.JsonNet(response);
        }

        [HttpPost]
        public ActionResult GetLogSapByFilter(FilterLogSap filter)
        {
            var response = new PagedListResponse<Dtos.LogSap>();

            try
            {
                response = _admin.GetLogSapByFilter(filter);
            }
            catch (Exception ex)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(ex.Message);
            }

            return this.JsonNet(response);
        }

        #endregion

        #region ConfirmacionArribo

        public ActionResult GetDataListConfirmacionArribo()
        {
            var response = new Response<object> { Result = new Result() { HasErrors = false, Messages = new List<string>() } };

            try
            {
                response.Data = new
                {
                    Usuario = CDPSession.Current.Usuario
                };
            }
            catch (Exception ex)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(ex.Message);
            }

            return this.JsonNet(response);
        }

        [HttpPost]
        public ActionResult GetConfirmacionesArriboByFilter(FilterBase filter)
        {
            var response = new PagedListResponse<Dtos.SolicitudReport>();

            try
            {
                response = _admin.GetConfirmacionesArriboByFilter(filter);
            }
            catch (Exception ex)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(ex.Message);
            }

            return this.JsonNet(response);
        }



        #endregion

    }
}