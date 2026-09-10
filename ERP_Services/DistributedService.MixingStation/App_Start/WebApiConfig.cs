using Infrastructure.CrossCutting.Base;
using System.Web.Http;

namespace DistributedService.MixingStation
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var _ = Unity.Container.Instance;
            ConfigurationFile.Instance.UrlXpoWebServer = System.Configuration.ConfigurationManager.AppSettings["XpoServiceURL"];
            ConfigurationFile.Instance.ProtocolUrlXpoWebServer = Protocol.basicHttp;
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
