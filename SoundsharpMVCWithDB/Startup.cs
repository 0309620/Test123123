using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SoundsharpMVCWithDB.Startup))]
namespace SoundsharpMVCWithDB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
