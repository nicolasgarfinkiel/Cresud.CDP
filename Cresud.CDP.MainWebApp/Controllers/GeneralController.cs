using System;
using System.Web.Mvc;
using Cresud.CDP.Admin;
using Cresud.CDP.Dtos;
using Cresud.CDP.Dtos.Common;
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