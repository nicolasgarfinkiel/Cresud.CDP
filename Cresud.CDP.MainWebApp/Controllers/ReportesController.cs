﻿using System.Web.Mvc;
using Cresud.CDP.Admin;
using Cresud.CDP.Dtos.Common;
using Cresud.CDP.Entities;

namespace Cresud.CDP.MainWebApp.Controllers
{
    [Authorize]
    public class ReportesController : BaseController<ReportesAdmin, int, SolicitudReport, Dtos.SolicitudReport, FilterBase>
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
            return new { };
        }

        #endregion        
    }
}