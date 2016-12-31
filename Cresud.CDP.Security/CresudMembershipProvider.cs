﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Web.Security;
using Cresud.CDP.Admin;
using Cresud.CDP.Dtos;
using Cresud.CDP.Infrastructure;
using Cresud.CDP.Infrastructure.Services;
using Cresud.CDP.Security.Service;

namespace Cresud.CDP.Security
{
    public class CresudMembershipProvider : MembershipProvider
    {
        #region Properties

        #endregion

        public CresudMembershipProvider()
        {

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

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            bool result = false;

            try
            {

            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { return true; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return false; }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            var result = 0;

            throw new NotImplementedException();

            return result;
        }

        public override string GetPassword(string username, string answer)
        {
            var result = String.Empty;

            throw new NotImplementedException();

            return result;
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();

        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return 3; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return 0; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 10; }
        }

        public override int PasswordAttemptWindow
        {
            get { return 30; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return ".{10,}"; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return true; }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        //public ResetPasswordProveedorResponse ResetPasswordProveedor(ResetPasswordProveedorRequest request)
        //{
        //    return _admin.ResetPasswordProveedor(request);
        //}

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            if (CDPSession.Current.Usuario == null)
            {
                Init(username);
            }

            return CDPSession.Current.Usuario != null && CDPSession.Current.Usuario.Empresas.Any(e => e.Roles != null && e.Roles.Any());
        }

        private void Init(string username)
        {
            var empresas = GetEmpresas(username);

            CDPSession.Current.Usuario = new Usuario
            {
                Nombre = username,
                Empresas = empresas
            };
        }

        private IList<Empresa> GetEmpresas(string username)
        {
            var admin = new EmpresaAdmin();
            var empresas = admin.GetAll().Where(e => e.GrupoEmpresa != null).ToList();
            var result = new List<Empresa>();

            empresas.ForEach(e =>
            {
                var usuario = GetUsuario(username, e.GrupoEmpresa.IdApp);

                if (usuario == null) return;


            });

            return result;
        }

        private object GetUsuario(string username, int idApp)
        {
            var client = new WebServiceClient<ISecurityService>(ConfigurationManager.AppSettings["SecurityServiceUrl"], (binding, httpTransport, address, factory) =>
             {
                 var basicAuthBehavior = new BasicAuthBehavior(ConfigurationManager.AppSettings["SecurityServiceUser"], ConfigurationManager.AppSettings["SecurityServicePassword"]);
                 factory.Endpoint.Behaviors.Add(basicAuthBehavior);               
             });

            try
            {
                var result = client.Channel.UserLogonByName(username, idApp);
            }
            catch (Exception)
            {

                throw;
            }

            return null;
        }
    }
}