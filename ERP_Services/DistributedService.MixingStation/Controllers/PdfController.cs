using Infrastructure.CrossCutting.Base;
using System;
using System.IO;
using System.Linq;
using System.Web.Http;

namespace DistributedService.MixingStation.Controllers
{
    /// <summary>
    /// Controlador de pdf para poder descargar reportes
    /// </summary>
    public class PdfController : ApiController
    {
        /// <summary>
        /// Generación de reporte de liberacion de la línea
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public byte[] GetReport(int id)
        {
            var headers = Request.Headers;

            if (headers.Contains(ConfigurationFile.SESS_CONTAINER))
                SessionValues.Instance.TransactionalContainer = headers.GetValues(ConfigurationFile.SESS_CONTAINER).First();
            
            if (headers.Contains(ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
                SessionValues.Instance.SecurityContainer = headers.GetValues(ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME).First();
            
            var rptBasicBilling = new Presentation.Reporter.rptReleaseLine();
            rptBasicBilling.ParametrosReporte = new object[] { id };
            rptBasicBilling.CargarDataSource();
            long miliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            var path = Path.Combine(Path.GetTempPath(), $"releaseLine_{id}_{miliseconds}.pd");
            rptBasicBilling.ExportToPdf(path);
            rptBasicBilling.Dispose();
            var file = File.ReadAllBytes(path);
            File.Delete(path);
            return file;
        }
    }
}
