using System.Web.Mvc;
using Contabilidad.Infrastructure;

namespace Cresud.CDP.Infrastructure
{
    public static class ControllerExtensions
    {
        public static JsonNetResult JsonNet(this Controller controller, object data)
        {
            return new JsonNetResult() { Data = data };
        }
    }
}
