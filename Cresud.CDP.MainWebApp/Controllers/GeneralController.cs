using System;
using System.Web.Mvc;
using Cresud.CDP.Admin;
using Cresud.CDP.Dtos;
using Cresud.CDP.Dtos.Common;
using Cresud.CDP.Dtos.Filters;
using Cresud.CDP.Infrastructure;

namespace Cresud.CDP.MainWebApp.Controllers
{
    [Authorize]
    public class GeneralController : BaseController<GeneralAdmin, int, object, object, FilterBase>
    {
        [HttpPost]
        public ActionResult GetClientesByFilter(FilterBase filter)
        {
            var response = new PagedListResponse<Cliente>();

            try
            {
                response = _admin.GetClientesByFilter(filter);
            }
            catch (Exception ex)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(ex.Message);
            }

            return this.JsonNet(response);
        }

        [HttpPost]
        public ActionResult GetClientesConProveedorByFilter(FilterClientesConProveedor filter)
        {
            var response = new PagedListResponse<Cliente>();

            try
            {
                response = _admin.GetClientesConProveedorByFilter(filter);
            }
            catch (Exception ex)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(ex.Message);
            }

            return this.JsonNet(response);
        }

        [HttpPost]
        public ActionResult GetProveedoresByFilter(FilterBase filter)
        {
            var response = new PagedListResponse<Proveedor>();

            try
            {
                response = _admin.GetProveedoresByFilter(filter);
            }
            catch (Exception ex)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(ex.Message);
            }

            return this.JsonNet(response);
        }
        
        public override object GetDataList()
        {
            throw new NotImplementedException();
        }

        public override object GetDataEdit()
        {
            throw new NotImplementedException();
        }
    }
}