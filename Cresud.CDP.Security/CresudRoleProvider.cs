using System;

namespace Cresud.CDP.Security
{
    public class CresudRoleProvider : System.Web.Security.RoleProvider
    {
        #region Properties
        
        #endregion 

        public CresudRoleProvider()
        {
        
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();      
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();          
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            bool result;

            throw new NotImplementedException();       

            return result;
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            var result = new string[]{};

            throw new NotImplementedException();      

            return result;
        }

        public override string[] GetAllRoles()
        {
            return null;
        }

        public override string[] GetRolesForUser(string username)
        {
            return null;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            var result = new string[]{};

            throw new NotImplementedException();        

            return result;
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            bool result;

            throw new NotImplementedException();        

            return result;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();    
        }

        public override bool RoleExists(string roleName)
        {
            bool result;

            throw new NotImplementedException();     

            return result;
        }
    }
}