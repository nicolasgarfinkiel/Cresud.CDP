using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Cresud.SecurityServiceTest
{
    /// <summary>
    /// Summary description for Security
    /// </summary>
    [WebService(Namespace = "http://framework.irsa.com.ar/WebServices/Security/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Security : System.Web.Services.WebService
    {

        [WebMethod]
        public UserLogonByNameResult UserLogonByName(string ntUserName, int idApplication)
        {
            return new UserLogonByNameResult();
        }

        [WebMethod]
        public List<Group> GroupsListPerUser(UserLogonByNameResult user, int idApplication)
        {
            return new List<Group>
            {
                new Group
                {
                    
                }
            };
        }

        [WebMethod]
        public List<Permission> PermissionListPerGroup(Group group)
        {
            return new List<Permission>
            {
                new Permission
                {
                    Description = "Admin"
                }
            };
        }
    }
}
