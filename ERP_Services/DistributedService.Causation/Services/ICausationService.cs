using DistributedService.Causation.Models;
using Domain.Base.Entities;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DistributedService.Causation.Services
{
    /// <summary>
    /// Servicio de causación
    /// </summary>
    public interface ICausationService
    {
        /// <summary>
        /// Causar facturas
        /// </summary>
        /// <param name="invoices"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        Task<ActionResult> CausateInvoicesAsync(List<InvoiceEvent> invoices, string container, string usercode, List<byte> allowedServiceTypes = null);
        Task<ActionResult> RemoveInvoiceFromPendingAsync(List<InvoiceEvent> invoiceEvents, string container, string userCode);
        ActionResult RetryCausateDetailInvoice(ViewListNoSurgical invoiceDetail, string container, string usercode);
        
        /// <summary>
        /// Versión optimizada para procesar grandes volúmenes de causaciones pendientes 
        /// por medio de las actualizacion de los contratos de los profesionales de la salud
        /// </summary>
        /// <param name="contractUpdateData"></param>
        /// <param name="container"></param>
        /// <param name="usercode"></param>
        /// <returns></returns>
        Task<ActionResult> ProcessContractCausationsAsync(ContractUpdateRequest contractUpdateData, string container, string usercode);

        /// <summary>
        /// Ejecuta auto-causación de servicios no facturados (OS en estado Registrada).
        /// Procesa todas las unidades operativas.
        /// </summary>
        /// <param name="batchSize">Tamaño del batch por iteración</param>
        /// <param name="container">Contenedor de la sesión</param>
        /// <param name="usercode">Código del usuario</param>
        Task<ActionResult> AutoCausateUnbilledAsync(int batchSize, string container, string usercode);
    }
}
