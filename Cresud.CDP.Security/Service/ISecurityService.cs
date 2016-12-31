using System.ServiceModel;
using Cresud.CDP.Security.Service.Dtos;

namespace Cresud.CDP.Security.Service
{
        
    [ServiceContract(Namespace = "http://framework.irsa.com.ar/WebServices/Security/"), XmlSerializerFormat]
    public interface ISecurityService
    {
        [OperationContract(Action = "http://framework.irsa.com.ar/WebServices/Security/UserLogonByName", ReplyAction = "http://framework.irsa.com.ar/WebServices/Security/UserLogonByName")] 
        UserLogonByNameResult UserLogonByName(string ntUserName, int idApplication);
    }
}
