using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProyectoTiempos.Startup))]
namespace ProyectoTiempos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
