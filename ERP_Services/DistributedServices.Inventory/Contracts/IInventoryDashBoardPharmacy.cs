using Domain.Base.Entities;
using Domain.Crystal.Entities;
using Domain.Inventory.POCO;
using Infrastructure.CrossCutting.Base;
using System.ServiceModel;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryDashBoardPharmacy
    {
        /// <summary>
        /// Obtener una solicitud de farmacia por consecutivo
        /// </summary>
        /// <param name="consecutive"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<HCFARMEPC> PharmacyByConsecutive(decimal consecutive);

        /// <summary>
        /// Función para realiza la logica para obtener información de una admisión para realizar la dispensación farmaceutica
        /// </summary>
        /// <param name="dispensingIntegration"></param>
        /// <param name="admissionNumber"></param>
        /// <param name="functionalUnitCode"></param>
        /// <param name=""></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<AdmissionInformation> GetAdmissionInformation(int dispensingIntegration, int careGroupIdIntegrationMedilaser, string admissionNumber, string functionalUnitCode, AuditMessage audit);

        /// <summary>
        /// Obtener una solicitud de paquete QX por consecutivo
        /// </summary>
        /// <param name="consecutive"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<ViewDashBoardPharmacy_SurgicalPackage> SurgicalPackageByConsecutive(decimal consecutive);
    }
}
