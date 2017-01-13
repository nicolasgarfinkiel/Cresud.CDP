using System.Web.Mvc;
using Cresud.CDP.Admin;
using Cresud.CDP.Dtos;
using Cresud.CDP.Dtos.Common;
using Cresud.CDP.Dtos.Filters;

namespace Cresud.CDP.MainWebApp.Controllers
{
    [Authorize]
    public class EstablecimientosController : BaseController<EstablecimientosAdmin, int, Entities.Establecimiento, Dtos.Establecimiento, FilterEstablecimientos>
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
            var generalAdmin = new GeneralAdmin();

            return new
            {
                Provincias = generalAdmin.GetProvincias(CDPSession.Current.Usuario.CurrentEmpresa.GrupoEmpresa.PaisId),
                Localidades = generalAdmin.GetLocalidades(),
                RecorridoEstablecimientoList = generalAdmin.GetRecorridoEstablecimientoList()
            };
        }
    }
}