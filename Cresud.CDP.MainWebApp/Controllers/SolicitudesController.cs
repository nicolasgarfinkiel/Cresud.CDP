using System.Linq;
using System.Web.Mvc;
using Cresud.CDP.Admin;
using Cresud.CDP.Dtos;
using Cresud.CDP.Dtos.Common;
using Solicitud = Cresud.CDP.Entities.Solicitud;

namespace Cresud.CDP.MainWebApp.Controllers
{
    [Authorize]
    public class SolicitudesController : BaseController<SolicitudesAdmin, int, Solicitud, Dtos.Solicitud, FilterBase>
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
            var generalAdmin = new GeneralAdmin();
            var granosAdmin = new GranosAdmin();

            return new
            {
                TipoCartaList = generalAdmin.GetTipoCartaList(),
                Granos = granosAdmin.GetAll().OrderBy(g => g.Descripcion),
                ClienteDefault = generalAdmin.GetClienteById(CDPSession.Current.Usuario.CurrentEmpresa.IdCliente.ToString())
            };
        }
        #endregion        
    }
}