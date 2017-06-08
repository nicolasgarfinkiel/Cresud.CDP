using System;
using System.Collections.Generic;
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
        #region Clientes

        [HttpPost]
        public virtual ActionResult CreateCliente(Cliente dto)
        {
            var response = new Response<object> { Result = new Result() { HasErrors = false, Messages = new List<string>() } };

            try
            {
                response.Data = _admin.CreateCliente(dto);
            }
            catch (Exception ex)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(ex.Message);
            }

            return this.JsonNet(response);
        }

        [HttpPost]
        public virtual ActionResult UpdateCliente(Cliente dto)
        {
            var response = new Response<object> { Result = new Result() { HasErrors = false, Messages = new List<string>() } };

            try
            {
                response.Data = _admin.UpdateCliente(dto);
            }
            catch (Exception ex)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(ex.Message);
            }

            return this.JsonNet(response);
        }


        [HttpPost]
        public ActionResult GetClienteById(string id)
        {
            var response = new Response<Dtos.Cliente>();

            try
            {
                response.Data = _admin.GetClienteById(id);
            }
            catch (Exception ex)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(ex.Message);
            }

            return this.JsonNet(response);
        }

        [HttpPost]
        public ActionResult GetClientesByFilter(FilterClientes filter)
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

        #endregion

        #region Proveedores

        [HttpPost]
        public virtual ActionResult CreateProveedor(Proveedor dto)
        {
            var response = new Response<object> { Result = new Result() { HasErrors = false, Messages = new List<string>() } };

            try
            {
                response.Data = _admin.CreateProveedor(dto);
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

        #endregion
        
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