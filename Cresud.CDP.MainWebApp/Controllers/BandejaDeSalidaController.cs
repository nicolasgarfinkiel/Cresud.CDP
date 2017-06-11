using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using Cresud.CDP.Admin;
using Cresud.CDP.Dtos;
using Cresud.CDP.Dtos.Common;
using Cresud.CDP.Dtos.Filters;
using Cresud.CDP.Infrastructure;
using Cresud.CDP.Infrastructure.ActionResults;
using SolicitudReport = Cresud.CDP.Entities.SolicitudReport;

namespace Cresud.CDP.MainWebApp.Controllers
{
    [Authorize]
    public class BandejaDeSalidaController : BaseController<ReportesAdmin, int, SolicitudReport, Dtos.SolicitudReport, FilterBase>
    {
        private readonly GeneralAdmin _generalAdmin;
        private readonly GranosAdmin _granosAdmin;
        private readonly SolicitudesAdmin _solicitudesAdmin;

        public BandejaDeSalidaController()
        {
            _generalAdmin = new GeneralAdmin();
            _granosAdmin = new GranosAdmin();
            _solicitudesAdmin = new SolicitudesAdmin();
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
            var response = new PagedListResponse<Dtos.SolicitudBandejaSalida>();

            try
            {
                response = _admin.GetSolicitadasByFilter(filter);
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                response.Result.HasErrors = true;

                while (ex != null)
                {
                    sb.AppendLine(ex.Message);
                    ex = ex.InnerException;
                }

                response.Result.Messages.Add(sb.ToString());
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

        public ActionResult ReportePdf(int solicitudId, string numeroCartaDePorte)
        {
            var pdf = _admin.ReportePdf(solicitudId);

            return new PdfResult
            {
                Content = pdf,
                FileName = string.Format("CartaDePorte_{0}.pdf", numeroCartaDePorte)
            };
        }

        public ActionResult ReporteSimplePdf(int solicitudId, string numeroCartaDePorte)
        {
            var pdf = _admin.ReporteSimplePdf(solicitudId);

            return new PdfResult
            {
                Content = pdf,
                FileName = string.Format("CartaDePorteSimple_{0}.pdf", numeroCartaDePorte)
            };
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
                    Usuario = CDPSession.Current.Usuario,
                    EstadosAfip = _generalAdmin.GetEstadosAfip()
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

         [HttpPost]
        public ActionResult ConfirmarArribo(int solicitudId, string consumoPropio)
        {
            var response = new Result() { HasErrors = false, Messages = new List<string>() };

            try
            {
                response = _solicitudesAdmin.ConfirmarArribo(solicitudId, consumoPropio);
            }
            catch (Exception ex)
            {
                response.HasErrors = true;
                response.Messages.Add(ex.Message);
            }

            return this.JsonNet(response);
        }        

        #endregion

        #region TrasladosRechazados

         [HttpPost]
        public ActionResult GetTrasladosRechazados(FilterBase filter)
        {
            var response = new PagedListResponse<Dtos.SolicitudReport>();

            try
            {
                response = _admin.GetTrasladosRechazados(filter);
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