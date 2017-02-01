using System;
using System.Collections.Generic;
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
    public class ReportesController : BaseController<ReportesAdmin, int, SolicitudReport, Dtos.SolicitudReport, FilterBase>
    {
        private readonly GeneralAdmin _generalAdmin;
        private readonly GranosAdmin _granosAdmin;

        public ReportesController()
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
            return new {};
        }

        public override object GetDataEdit()
        {
            return new { };
        }

        #endregion 
       
        #region Export

        public ActionResult GetDataListExport()
        {
            var response = new Response<object> { Result = new Result() { HasErrors = false, Messages = new List<string>() } };
            var empresaId = CDPSession.Current.Usuario.CurrentEmpresa.Id.Value;

            try
            {
                response.Data = new
                {
                    Usuario = CDPSession.Current.Usuario,
                    RemitentesComerciales = _generalAdmin.GetClientesRemitenteComercial(empresaId),
                    ClientesCorredor = _generalAdmin.GetClientesCorredor(empresaId),
                    ClientesEntregador = _generalAdmin.GetClientesEntregador(empresaId),
                    ClientesDestinatario = _generalAdmin.GetClientesDestinatario(empresaId),
                    ClientesIntermediarios = _generalAdmin.GetClientesIntermediarios(empresaId),
                    Cosechas = _granosAdmin.GetCosechas(),
                    Granos = _granosAdmin.GetGranosByGrupoEmpresaId(CDPSession.Current.Usuario.CurrentEmpresa.GrupoEmpresa.Id.Value),
                };


            }
            catch (Exception ex)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(ex.Message);
            }

            return this.JsonNet(response);                           
        }

        public ActionResult Export(FilterCartasDePorteExport filter)
        {
            var excelPackage = _admin.Export(filter);
            var fileName = App.IdGrupoCresud == filter.IdGrupoEmpresa ? "Lote_CDP_{0}.xlsx" : "Lote_CDP_Cresca_{0}.xlsx";

            return new ExcelResult
            {
                ExcelPackage = excelPackage,
                FileName = string.Format(fileName, DateTime.Now.ToString("dd/MM/yyyy"))
            };
        }

        #endregion

        #region EmitidasRecibidas

        public ActionResult GetDataListCdp()
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
        public ActionResult GetCdpsEmitidasByFilter(FilterCartasDePorteEmitidasRecibidas filter)
        {
            var response = new PagedListResponse<Dtos.SolicitudReport>();

            try
            {
                response = _admin.GetCdpsEmitidasByFilter(filter);
            }
            catch (Exception ex)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(ex.Message);
            }

            return this.JsonNet(response);
        }


        [HttpPost]
        public ActionResult GetCdpsRecibidasByFilter(FilterCartasDePorteEmitidasRecibidas filter)
        {
            var response = new PagedListResponse<Dtos.SolicitudRecibida>();

            try
            {
                response = _admin.GetCdpsRecibidasByFilter(filter);
            }
            catch (Exception ex)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(ex.Message);
            }

            return this.JsonNet(response);
        }


        public ActionResult EmitidasExport(FilterCartasDePorteEmitidasRecibidas filter)
        {
            var txt = _admin.GetEmitidasExport(filter);
            var fileName = "CartasDePorteEmitidas_{0}.txt";

            return new TxtResult
            {
                Txt = txt,
                FileName = string.Format(fileName, DateTime.Now.ToString("dd/MM/yyyy"))
            };
        }

        public ActionResult RecibidasExport(FilterCartasDePorteEmitidasRecibidas filter)
        {
            var txt = _admin.GetRecibidasExport(filter);
            var fileName = "CartasDePorteRecibidas_{0}.txt";

            return new TxtResult
            {
                Txt = txt,
                FileName = string.Format(fileName, DateTime.Now.ToString("dd/MM/yyyy"))
            };
        }

        #endregion

        #region Actividad

        [HttpPost]
        public ActionResult GetReporteActividad(FilterCartasDePorteEmitidasRecibidas filter)
        {
            var response = new Response<IList<CartaDePorteGraficoItem>>();

            try
            {
                response.Data = _admin.GetReporteActividad(filter);
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