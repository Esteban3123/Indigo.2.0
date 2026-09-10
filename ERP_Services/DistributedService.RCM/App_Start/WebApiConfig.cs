using System.Web.Http;
using DistributedService.Rest.App_Start;

namespace DistributedService.Rest
{
    /// <summary>
    /// Configuración de Web API para producción.
    /// Todos los endpoints requieren Bearer JWT excepto /health ([AllowAnonymous]).
    /// </summary>
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new JwtAuthenticationFilter());

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
