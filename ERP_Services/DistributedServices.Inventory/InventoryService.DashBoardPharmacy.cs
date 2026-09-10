using Application.Inventory.DashBoardPharmacy;
using DistributedServices.Inventory.Unity;
using Domain.Base.Entities;
using Domain.Crystal.Entities;
using Domain.Inventory.POCO;
using Infrastructure.CrossCutting.Base;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
	public partial class InventoryService
	{
		/// <summary>
		/// Obtener una solicitud de farmacia por consecutivo
		/// </summary>
		/// <param name="consecutive"></param>
		/// <returns></returns>
		public ActionResult<HCFARMEPC> PharmacyByConsecutive(decimal consecutive)
		{
			using (var service = Container.Current.Resolve<IDashBoardPharmacyAdminService>())
			{
				return service.PharmacyByConsecutive(consecutive);
			}
		}

		/// <summary>
		/// Función para realiza la logica para obtener información de una admisión para realizar la dispensación farmaceutica
		/// </summary>
		/// <param name="dispensingIntegration"></param>
		/// <param name="careGroupIdIntegrationMedilaser"></param>
		/// <param name="admissionNumber"></param>
		/// <param name="functionalUnitCode"></param>
		/// <param name=""></param>
		/// <param name="audit"></param>
		/// <returns></returns>
		public ActionResult<AdmissionInformation> GetAdmissionInformation(int dispensingIntegration, int careGroupIdIntegrationMedilaser, string admissionNumber, string functionalUnitCode, AuditMessage audit)
		{
			using (var service = Container.Current.Resolve<IDashBoardPharmacyAdminService>())
			{
				return service.GetAdmissionInformation(dispensingIntegration, careGroupIdIntegrationMedilaser, admissionNumber, functionalUnitCode, audit);
			}
        }

        /// <summary>
        /// Obtener una solicitud de paquete QX por consecutivo
        /// </summary>
        /// <param name="consecutive"></param>
        /// <returns></returns>
        public ActionResult<ViewDashBoardPharmacy_SurgicalPackage> SurgicalPackageByConsecutive(decimal consecutive)
        {
            using (var service = Container.Current.Resolve<IDashBoardPharmacyAdminService>())
            {
                return service.SurgicalPackageByConsecutive(consecutive);
            }
        }
    }
}
