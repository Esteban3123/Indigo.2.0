using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using DistributedServices.Inventory.Contracts;
using Domain.Crystal.Entities;
using Application.Inventory.PharmaceuticalDispensing;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;
using Application.Inventory.InventoryControl;
using Domain.Base.Entities;
using Domain.Entities;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {
        /// <summary>
        /// Updates the functional unit crystal.
        /// </summary>
        /// <param name="ConsecutivePharmacy">The consecutive pharmacy.</param>
        /// <param name="funcionalUnitCode">The funcional unit code.</param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult UpdateFunctionalUnitCrystal(decimal ConsecutivePharmacy, string funcionalUnitCode)
        {
            using (var service = Container.Current.Resolve<IPharmaceuticalDispensingAdminService>())
            {
                return service.UpdateFunctionalUnitCrystal(ConsecutivePharmacy, funcionalUnitCode);
            }
            //return _pharmaceuticalDispensingAdminService.UpdateFunctionalUnitCrystal(ConsecutivePharmacy, funcionalUnitCode);
        }

        /// <summary>
        /// Validates the functional unit dash board.
        /// </summary>
        /// <param name="admissionNumber">The admission number.</param>
        /// <param name="patientCode">The patient code.</param>
        /// <param name="functionalUnitCode">The functional unit code.</param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<string> ValidateFunctionalUnitDashBoard(string admissionNumber, string patientCode, string functionalUnitCode, int requestNumber)
        {
            using (var service = Container.Current.Resolve<IPharmaceuticalDispensingAdminService>())
            {
                return service.ValidateFunctionalUnitDashBoard(admissionNumber, patientCode, functionalUnitCode, requestNumber);
            }
            //return _pharmaceuticalDispensingAdminService.ValidateFunctionalUnitDashBoard(admissionNumber, patientCode, functionalUnitCode);
        }

        /// <summary>
        /// Saves the dashboard pharmacy.
        /// </summary>
        /// <param name="ListPharmaceuticalDispensing">The list pharmaceutical dispensing.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Domain.Base.Entities.ActionResult<string> SaveDashboardPharmacy(List<Domain.Entities.PharmaceuticalDispensing> ListPharmaceuticalDispensing, List<ViewDashboardPharmacyDetail> ListDetailAnnular, long idSequense, AuditMessage audit)
        {
            Domain.Base.Entities.ActionResult<string> result = null;

            using (var service = Container.Current.Resolve<IPharmaceuticalDispensingAdminService>())
            {               
                result = service.SaveDashboardPharmacy(ListPharmaceuticalDispensing, ListDetailAnnular, audit, idSequense);
            }
            
            return result;
        }

        /// <summary>
        /// Metodo que guarda la dispensación por paciente medilaser
        /// </summary>
        /// <param name="ListCrystal"></param>
        /// <param name="ListPharmaceuticalDispensing"></param>
        /// <param name="ListDetailAnnular"></param>
        /// <param name="CareCenterCode"></param>
        /// <param name="WareHouseId"></param>
        /// <param name="CareGroupId"></param>
        /// <param name="BillingAuthorizationId"></param>
        /// <param name="Date"></param>
        /// <param name="Number"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.SP_SaveDispensingByPatientMedilaser_Result> SaveDispensingByPatientMedilaser(List<SP_ListHCPRESCRDByCODCONCEC_Result> ListCrystal, List<PharmaceuticalDispensing> ListPharmaceuticalDispensing, List<ViewDashboardPharmacyDetail> ListDetailAnnular, string CareCenterCode, int WareHouseId, int CareGroupId, int BillingAuthorizationId, DateTime Date, string Number, SessionValues session, List<Domain.Crystal.Entities.ViewDashboardPharmacyDetailDeferred> ListDeferred, bool IsManual)
        {
            using (var service = Container.Current.Resolve<IPharmaceuticalDispensingAdminService>())
            {
                return service.SaveDispensingByPatientMedilaser(ListCrystal, ListPharmaceuticalDispensing, ListDetailAnnular, CareCenterCode, WareHouseId, CareGroupId, BillingAuthorizationId, Date, Number, session.AuditMessageWcf, ListDeferred, IsManual, session);
            }
        }

        /// <summary>
        /// Guarda una dispensación farmacéutica
        /// </summary>
        public Domain.Base.Entities.ActionResult<Domain.Entities.PharmaceuticalDispensing> SavePharmaceuticalDispensing(Domain.Entities.PharmaceuticalDispensing pharmaceuticalDispensing, bool confirm, long idSequense, AuditMessage audit, bool affectInventory = true)
        {
            using (var service = Container.Current.Resolve<IPharmaceuticalDispensingAdminService>())
            {                
                return service.SavePharmaceuticalDispensing(pharmaceuticalDispensing, audit, confirm, idSequense, affectInventory);
            }
            //return _pharmaceuticalDispensingAdminService.SavePharmaceuticalDispensing(pharmaceuticalDispensing, audit,confirm, idSequense,affectInventory );
        }

        /// <summary>
        /// Elimina una dispensación farmacéutica
        /// </summary>
        public Domain.Base.Entities.ActionResult DeletePharmaceuticalDispensing(Domain.Entities.PharmaceuticalDispensing pharmaceuticalDispensing, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IPharmaceuticalDispensingAdminService>())
            {               
                return service.DeletePharmaceuticalDispensing(pharmaceuticalDispensing, audit);
            }
            //return _pharmaceuticalDispensingAdminService.DeletePharmaceuticalDispensing(pharmaceuticalDispensing, audit);
        }

        /// <summary>
        /// Obtiene una dispensación farmacéutica
        /// </summary>
        public Domain.Base.Entities.ActionResult<Domain.Entities.PharmaceuticalDispensing> GetPharmaceuticalDispensing(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IPharmaceuticalDispensingAdminService>())
            {               
                return service.GetPharmaceuticalDispensing(code, audit);
            }
            //return _pharmaceuticalDispensingAdminService.GetPharmaceuticalDispensing(code, audit);
        }

        /// <summary>
        /// Obtiene una dispensación farmacéutica
        /// </summary>
        public Domain.Entities.PharmaceuticalDispensing GetPharmaceuticalDispensingById(int id)
        {
            using (var service = Container.Current.Resolve<IPharmaceuticalDispensingAdminService>())
            {
                return service.GetPharmaceuticalDispensingById(id);
            }
            //return _pharmaceuticalDispensingAdminService.GetPharmaceuticalDispensingById(id);
        }

        //public ActionResult<string> SaveDashboardCentralMix(List<PharmaceuticalDispensing> ListPharmaceuticalDispensing, AuditMessage audit)
        //{
        //    using (var service = Container.Current.Resolve<IPharmaceuticalDispensingAdminService>())
        //    {
        //        return service.SaveDashboardCentralMix(ListPharmaceuticalDispensing, audit);
        //    }
        //}


        public Domain.Base.Entities.ActionResult<string>SaveDashboardPharmacySurgicalPackage(List<Domain.Entities.PharmaceuticalDispensing> ListPharmaceuticalDispensing, List<ViewDashBoardPharmacy_SurgicalPackageDeatils > ListDetailAnnular, long idSequense, AuditMessage audit)
        {
            Domain.Base.Entities.ActionResult<string> result = null;

            using (var service = Container.Current.Resolve<IPharmaceuticalDispensingAdminService>())
            {
                result = service.SaveDashboardPharmacySurgicalPackage(ListPharmaceuticalDispensing, ListDetailAnnular, audit, idSequense);
            }

            return result;
        }

        /// <summary>
        /// Obtiene el listado de medicamentos mediante una formula medica
        /// </summary>
        /// <param name="CODCONCEC"></param>
        /// <returns></returns>
        public ActionResult<List<SP_ListHCPRESCRDByCODCONCEC_Result>> SP_ListHCPRESCRDByCODCONCEC(string CODCONCEC)
        {
            using (var service = Container.Current.Resolve<IPharmaceuticalDispensingAdminService>())
            {
                return service.SP_ListHCPRESCRDByCODCONCEC(CODCONCEC);
            }
        }

    }
}