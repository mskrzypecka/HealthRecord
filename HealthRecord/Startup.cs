using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(HealthRecord.Startup))]
namespace HealthRecord
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}