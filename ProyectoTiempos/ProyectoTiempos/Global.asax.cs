using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using ProyectoTiempos.Clases;

namespace ProyectoTiempos
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {
                var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                var serializer = new JavaScriptSerializer();

                var serializeModel = serializer.Deserialize<Common>(authTicket.UserData);

                var newUser = new CustomPrincipal(authTicket.Name)
                {

                    UsuarioId = serializeModel.UsuarioId,
                    NombreUsuario = serializeModel.NombreUsuario,
                    Nombre = serializeModel.Nombre,
                    Apellidos = serializeModel.Apellidos,
                    TipoUsuario = serializeModel.TipoUsuario
                };
                HttpContext.Current.User = newUser;
            }
        }
    }


}
