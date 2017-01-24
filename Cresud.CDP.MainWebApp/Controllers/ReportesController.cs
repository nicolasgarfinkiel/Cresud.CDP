using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Cresud.CDP.Admin;
using Cresud.CDP.Dtos;
using Cresud.CDP.Dtos.Common;
using Cresud.CDP.Infrastructure;
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

        public ActionResult GetDataListExport()
        {
            var response = new Response<object> { Result = new Result() { HasErrors = false, Messages = new List<string>() } };
            var empresaId = CDPSession.Current.Usuario.CurrentEmpresa.Id.Value;

            try
            {
                response.Data = new
                {
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
    }
}