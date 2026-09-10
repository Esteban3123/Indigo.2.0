using System.Web.Http;

namespace DistributedService.Causation.Controllers
{
    [RoutePrefix("/")]
    public class DefaultController : ApiController
    {
        [HttpGet]
        public string Get()
        {
            return "Running...";
        }
    }
}
