//***********************************************************************
// Assembly         : Domain.Inventory
// Author           : Diego Andrés Roldán Lozano
// Created          : 28-01-2015
//
// Copyright        : (c) . All rights reserved.
//***********************************************************************

using Domain.Base.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Crystal.Entities;
using Application.Inventory.Rollback;

namespace Application.Inventory.PharmaceuticalDispensing
{
    public interface IPharmaceuticalDispensingAdminService : IAdminServiceRollbackStrategy, IDisposable
    {
        /// <summary>
        /// metodo para actualizar la unidad funcional desde el dashboard
        /// </summary>
        /// <param name="ConsecutivePharmacy"></param>
        /// <param name="funcionalUnitCode"></param>
        /// <returns></returns>
        ActionResult UpdateFunctionalUnitCrystal(decimal ConsecutivePharmacy, string funcionalUnitCode);
        /// <summary>
        /// metodo para validar la unidad funcional en el dashboard de solicitudes
        /// </summary>
        /// <param name="admissionNumber"></param>
        /// <param name="patientCode"></param>
        /// <param name="functionalUnitCode"></param>
        /// <returns></returns>
        ActionResult<string> ValidateFunctionalUnitDashBoard(string admissionNumber, string patientCode, string functionalUnitCode, int requestNumber);
        /// <summary>
        /// metodo para guardar los items del dashboard de farmacia
        /// </summary>
        /// <param name="ListPharmaceuticalDispensing"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        ActionResult<string> SaveDashboardPharmacy(List<Domain.Entities.PharmaceuticalDispensing> ListPharmaceuticalDispensing, List<ViewDashboardPharmacyDetail> ListDetailAnnular, AuditMessage audit, Int64 idSecuence = 0);

        ActionResult<string> SaveDashboardCentralMix(List<Domain.Entities.PharmaceuticalDispensing> ListPharmaceuticalDispensing, AuditMessage audit);


        /// <summary>
        /// metodo para guardar la dispensación por paciente medilaser
        /// </summary>
        /// <param name="ListPharmaceuticalDispensing"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.SP_SaveDispensingByPatientMedilaser_Result> SaveDispensingByPatientMedilaser(List<Domain.Entities.SP_ListHCPRESCRDByCODCONCEC_Result> ListCrystal, List<Domain.Entities.PharmaceuticalDispensing> ListPharmaceuticalDispensing, List<ViewDashboardPharmacyDetail> ListDetailAnnular, string CareCenterCode, int WareHouseId, int CareGroupId, int BillingAuthorizationId, DateTime Date, string Number, AuditMessage audit, List<Domain.Crystal.Entities.ViewDashboardPharmacyDetailDeferred> ListDeferred, bool IsManual, SessionValues session);

        /// <summary>
        /// Guarda una dispensación farmacéutica
        /// </summary>
        ActionResult<Domain.Entities.PharmaceuticalDispensing> SavePharmaceuticalDispensing(Domain.Entities.PharmaceuticalDispensing pharmaceuticalDispensing, AuditMessage audit, bool confirm, Int64 idSecuence = 0, bool affectInventory = true);

        /// <summary>
        /// Elimina una dispensación farmacéutica
        /// </summary>
        ActionResult DeletePharmaceuticalDispensing(Domain.Entities.PharmaceuticalDispensing pharmaceuticalDispensing, AuditMessage audit);

        /// <summary>
        /// Obtiene una dispensación farmacéutica
        /// </summary>
        ActionResult<Domain.Entities.PharmaceuticalDispensing> GetPharmaceuticalDispensing(string code, AuditMessage audit);

        /// <summary>
        /// Obtiene una dispensación farmacéutica
        /// </summary>
        Domain.Entities.PharmaceuticalDispensing GetPharmaceuticalDispensingById(int id);


        /// <summary>
        /// Genera la dispensacion para los paquetes Quirurgicos
        /// </summary>
        /// <param name="ListPharmaceuticalDispensing">Listado de item dispensar</param>
        /// <param name="ListDetailAnnular">listado items a Anular</param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        ActionResult<string> SaveDashboardPharmacySurgicalPackage(List<Domain.Entities.PharmaceuticalDispensing> ListPharmaceuticalDispensing, List<ViewDashBoardPharmacy_SurgicalPackageDeatils> ListDetailAnnular, AuditMessage audit, long idSecuence = 0);

        /// <summary>
        /// Obtiene el listado de medicamentos mediante una formula medica
        /// </summary>
        /// <returns></returns>
        ActionResult<List<Domain.Entities.SP_ListHCPRESCRDByCODCONCEC_Result>> SP_ListHCPRESCRDByCODCONCEC(string CODCONCEC);
    }
}