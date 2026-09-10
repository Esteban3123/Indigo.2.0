using DistributedService.Causation.Middlewares;
using Owin;
using System.Web.Http;

namespace DistributedService.Causation
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.Use<AzureServiceBusMiddleware>();
            app.UseWebApi(config);
        }
    }
}