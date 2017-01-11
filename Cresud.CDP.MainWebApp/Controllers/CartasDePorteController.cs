﻿using System.Web.Mvc;
using Cresud.CDP.Admin;
using Cresud.CDP.Dtos.Common;
using Cresud.CDP.Dtos.Filters;

namespace Cresud.CDP.MainWebApp.Controllers
{
    [Authorize]
    public class CartasDePorteController : BaseController<CartasDePorteAdmin, int, Entities.LoteCartaPorte, Dtos.LoteCartaPorte, FilterLotesCartaPorte>
    {        
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
    }
}