using System;
using System.Net;
using System.Web.Http;

namespace DistributedService.Rest.Controllers
{
    /// <summary>
    /// Endpoints de health check.
    /// </summary>
    [RoutePrefix("health")]
    public class HealthController : ApiController
    {
        /// <summary>
        /// GET /health - Verifica que el servicio está en ejecución.
        /// Usado por Azure Load Balancer, Kubernetes liveness/readiness, etc.
        /// No requiere autenticación.
        /// </summary>
        [Route("")]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Get()
        {
            return Content(HttpStatusCode.OK, new
            {
                status = "healthy",
                service = "DistributedService.RCM",
                timestamp = DateTime.UtcNow
            });
        }

        /// <summary>
        /// GET /health/ping - Ping protegido por JWT para verificar que el token funciona.
        /// Requiere Authorization: Bearer &lt;token&gt;
        /// </summary>
        [Route("ping")]
        [HttpGet]
        public IHttpActionResult Ping()
        {
            return Content(HttpStatusCode.OK, new
            {
                status = "ok",
                message = "pong",
                timestamp = DateTime.UtcNow
            });
        }
    }
}
