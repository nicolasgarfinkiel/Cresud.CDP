using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.Mvc;
using Cresud.CDP.Admin;
using Cresud.CDP.Dtos;
using Cresud.CDP.Dtos.Common;
using Cresud.CDP.Dtos.Filters;
using Cresud.CDP.Infrastructure;
using Cresud.CDP.Infrastructure.ActionResults;

namespace Cresud.CDP.MainWebApp.Controllers
{
    [Authorize(Roles = "Alta PDF Lotes Cartas de Porte")]
    public class LotesController : BaseController<LotesAdmin, int, Entities.LoteCartaPorte, Dtos.LoteCartaPorte, FilterLotesCartaPorte>
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
            return new
            {
                CartasDePorteDisponibles = _admin.GetCartasDePorteDisponibles(CDPSession.Current.Usuario.CurrentEmpresa.GrupoEmpresa.Id.Value)
            };
        }

        #endregion

        public ActionResult Export(FilterLotesCartaPorte filter)
        {
            var excelPackage = _admin.Export(filter);

            return new ExcelResult
            {
                ExcelPackage = excelPackage,
                FileName = string.Format("RangosCartaDePorte_{0}.xlsx", DateTime.Now.ToString("ddMMyyyy"))
            };
        }

        [HttpPost]
        public ActionResult UploadPdf()
        {
            var response = new Result { HasErrors = false, Messages = new List<string>() };

            try
            {
                var folder = ConfigurationManager.AppSettings["RutaOriginalCartaDePorte"];
                var fileName = Request.Files[0].FileName;
                var fullPath = string.Format("{0}{1}", folder, fileName);

                if (!string.Equals(Path.GetExtension(fileName), ".pdf"))
                {
                    throw new Exception("Solo se permiten archivos de tipo .pdf");
                }

                if (System.IO.File.Exists(fullPath))
                {
                    throw new Exception("Ya existe otro archivo con el mismo nombre");
                }

                CDPSession.Current.File = Request.Files[0];          
            }
            catch (Exception ex)
            {
                response.HasErrors = true;
                response.Messages.Add(ex.Message);
            }

            return this.JsonNet(response);
        }
    }
}