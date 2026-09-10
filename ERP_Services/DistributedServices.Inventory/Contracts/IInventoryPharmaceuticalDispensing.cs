///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Diego Andrés Roldán Lozano
/// Created          : 28-01-2015
///
/// Copyright        : (c) . All rights reserved.
///***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Crystal.Entities;
using Domain.Inventory.POCO;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryPharmaceuticalDispensing
    {
        /// <summary>
        /// metodo para actualizar la unidad funcional desde el dashboard
        /// </summary>
        /// <param name="ConsecutivePharmacy"></param>
        /// <param name="funcionalUnitCode"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult UpdateFunctionalUnitCrystal(decimal ConsecutivePharmacy, string funcionalUnitCode);
        /// <summary>
        /// metodo para validar la unidad funcional en el dashboard de solicitudes
        /// </summary>
        /// <param name="admissionNumber"></param>
        /// <param name="patientCode"></param>
        /// <param name="functionalUnitCode"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<string> ValidateFunctionalUnitDashBoard(string admissionNumber, string patientCode, string functionalUnitCode, int requestNumber);
        /// <summary>
        /// metodo para guardar los items del dashboard de farmacia
        /// </summary>
        /// <param name="ListPharmaceuticalDispensing"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<string> SaveDashboardPharmacy(List<Domain.Entities.PharmaceuticalDispensing> ListPharmaceuticalDispensing, List<ViewDashboardPharmacyDetail> ListDetailAnnular, long idSequense, AuditMessage audit);

        /// <summary>
        /// metodo para guardar la dispensación por paciente medilaser
        /// </summary>
        /// <param name="ListPharmaceuticalDispensing"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.SP_SaveDispensingByPatientMedilaser_Result> SaveDispensingByPatientMedilaser(List<Domain.Entities.SP_ListHCPRESCRDByCODCONCEC_Result> ListCrystal, List<Domain.Entities.PharmaceuticalDispensing> ListPharmaceuticalDispensing, List<ViewDashboardPharmacyDetail> ListDetailAnnular, string CareCenterCode, int WareHouseId, int CareGroupId, int BillingAuthorizationId, DateTime Date, string Number, SessionValues session, List<Domain.Crystal.Entities.ViewDashboardPharmacyDetailDeferred> ListDeferred, bool IsManual);

        /// <summary>
        /// Guarda una dispensación farmacéutica
        /// </summary>
        [OperationContract]
        ActionResult<Domain.Entities.PharmaceuticalDispensing> SavePharmaceuticalDispensing(Domain.Entities.PharmaceuticalDispensing pharmaceuticalDispensing, bool confirm, long idSequense, AuditMessage audit, bool affectInventory = true);

        /// <summary>
        /// Elimina una dispensación farmacéutica
        /// </summary>
        [OperationContract]
        ActionResult DeletePharmaceuticalDispensing(Domain.Entities.PharmaceuticalDispensing pharmaceuticalDispensing, AuditMessage audit);

        /// <summary>
        /// Obtiene una dispensación farmacéutica
        /// </summary>
        [OperationContract]
        ActionResult<Domain.Entities.PharmaceuticalDispensing> GetPharmaceuticalDispensing(string code, AuditMessage audit);

        /// <summary>
        /// Obtiene una dispensación farmacéutica
        /// </summary>
        [OperationContract]
        Domain.Entities.PharmaceuticalDispensing GetPharmaceuticalDispensingById(int id);

        [OperationContract]
        List<Domain.Entities.PharmaceuticalDispensingDetailBatchSerial> GetPharmaceuticalDispensingDetailBatchSerialByPharmaceuticalDispensingId(int pharmaceuticalDispensingId);

        //[OperationContract]
      //  ActionResult<string> SaveDashboardCentralMix(List<Domain.Entities.PharmaceuticalDispensing> ListPharmaceuticalDispensing, AuditMessage audit);

        
        [OperationContract]
        ActionResult<string>SaveDashboardPharmacySurgicalPackage(List<Domain.Entities.PharmaceuticalDispensing> ListPharmaceuticalDispensing, List<ViewDashBoardPharmacy_SurgicalPackageDeatils > ListDetailAnnular, long idSequense, AuditMessage audit);

        /// <summary>
        /// Obtiene el listado de medicamentos mediante una formula medica
        /// </summary>
        /// <param name="CODCONCEC"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<List<Domain.Entities.SP_ListHCPRESCRDByCODCONCEC_Result>> SP_ListHCPRESCRDByCODCONCEC(string CODCONCEC);
    }
}