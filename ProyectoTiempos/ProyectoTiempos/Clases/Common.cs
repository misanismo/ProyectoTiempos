using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ProyectoTiempos.Models;

namespace ProyectoTiempos.Clases
{
    public class Common
    {
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public TipoUser TipoUsuario { get; set; }

    }


    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        // the "new" must be used here because we are hiding
        // the Roles property on the underlying class
        public new TipoUser Roles;
        //private static bool _failedRolesAuth;

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            //var isAuthorized = base.AuthorizeCore(httpContext);
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                // The user is not authorized => no need to continue
                return false;
            }

            var cu = (CustomPrincipal)HttpContext.Current.User;
            var role = cu.TipoUsuario;

            if (Roles != 0 && (Roles & role) != role)
            {
                //_failedRolesAuth = true;
                return false;
            }

            return true;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            var skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) ||
                                filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(
                                    typeof(AllowAnonymousAttribute), true);
            if (!skipAuthorization)
            {
                base.OnAuthorization(filterContext);
            }
            //if (_failedRolesAuth)
            //{
            //    filterContext.Result = new ViewResult { ViewName = "NotAuth" };
            //}
        }

    }

    public class CustomPrincipal : ICustomPrincipal

    { 
      public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public TipoUser TipoUsuario { get; set; }
        public bool IsInRole(string role)
        {
            return false;
        }
        public IIdentity Identity { get;  set; }

        public CustomPrincipal(string userName)
        {
            Identity = new GenericIdentity(userName);
           
        }
    }

    public interface ICustomPrincipal : IPrincipal
    {
        int UsuarioId { get; set; }
        string NombreUsuario { get; set; }
        string Nombre { get; set; }
        string Apellidos { get; set; }
      
    }

    
}