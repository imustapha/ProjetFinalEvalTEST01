using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjetFinalEval.Startup))]
namespace ProjetFinalEval
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
