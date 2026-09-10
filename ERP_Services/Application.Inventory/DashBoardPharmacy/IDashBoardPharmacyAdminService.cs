using Domain.Base.Entities;
using Domain.Crystal.Entities;
using Domain.Inventory.POCO;
using Infrastructure.CrossCutting.Base;
using System;

namespace Application.Inventory.DashBoardPharmacy
{
    public interface IDashBoardPharmacyAdminService : IDisposable
    {
        ActionResult<HCFARMEPC> PharmacyByConsecutive(decimal consecutive);

        ActionResult<AdmissionInformation> GetAdmissionInformation(int dispensingIntegration, int careGroupIdIntegrationMedilaser, string admissionNumber, string functionalUnitCode, AuditMessage audit);

        ActionResult<ViewDashBoardPharmacy_SurgicalPackage> SurgicalPackageByConsecutive(decimal consecutive);
    }
}
