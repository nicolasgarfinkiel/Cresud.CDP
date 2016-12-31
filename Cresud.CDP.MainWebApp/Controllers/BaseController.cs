using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Cresud.CDP.Admin;
using Cresud.CDP.Dtos.Common;
using Cresud.CDP.Infrastructure;

namespace Cresud.CDP.MainWebApp.Controllers
{
    public abstract class BaseController<TA, TID,  TE, TD, TF> : Controller where TA : BaseAdmin<TID, TE, TD, TF>, new() where TF : FilterBase
    {
        #region Properties

        protected TA _admin;

        #endregion

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            _admin = new TA();
        }       

        [HttpPost]
        public ActionResult GetDataListInit()
        {
            var response = new Response<object> { Result = new Result() { HasErrors = false, Messages = new List<string>() } };

            try
            {
                response.Data = GetDataList();
            }
            catch (Exception ex)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(ex.Message);
            }

            return this.JsonNet(response);
        }

        [HttpPost]
        public ActionResult GetDataEditInit()
        {
            var response = new Response<object> { Result = new Result() { HasErrors = false, Messages = new List<string>() } };

            try
            {
                response.Data = GetDataEdit();
            }
            catch (Exception ex)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(ex.Message);
            }

            return this.JsonNet(response);
        }

        [HttpPost]
        public ActionResult GetById(TID id)
        {
            var response = new Response<object> { Result = new Result() { HasErrors = false, Messages = new List<string>() } };

            try
            {
                response.Data = _admin.GetById(id);
            }
            catch (Exception ex)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(ex.Message);
            }

            return this.JsonNet(response);
        }

        [HttpPost]
        public ActionResult GetByFilter(TF filter)
        {
            var response = new Response<object> { Result = new Result() { HasErrors = false, Messages = new List<string>() } };

            try
            {
                response.Data = _admin.GetByFilter(filter);
            }
            catch (Exception ex)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(ex.Message);
            }

            return this.JsonNet(response);
        }

        [HttpPost]
        public ActionResult CreateEntity(TD dto)
        {
            var response = new Response<object> { Result = new Result() { HasErrors = false, Messages = new List<string>() } };

            try
            {
                _admin.Create(dto);
            }
            catch (Exception ex)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(ex.Message);
            }

            return this.JsonNet(response);
        }

        [HttpPost]
        public ActionResult UpdateEntity(TD dto)
        {
            var response = new Response<object> { Result = new Result() { HasErrors = false, Messages = new List<string>() } };

            try
            {
                _admin.Update(dto);
            }
            catch (Exception ex)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(ex.Message);
            }

            return this.JsonNet(response);
        }
       
        #region Abstract Methods

        public abstract object GetDataList();
        public abstract object GetDataEdit();

        #endregion

    }
}