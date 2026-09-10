using Application.Events.Models;
using Application.Events.Models.iPSService;
using System;

namespace Application.Events.Serializers
{
    public class iPSService : IDittoDocument
    {
        public object Generate(object obj, QueueParameter parameter)
        {
            Domain.Entities.IPSService iPSServiceEntity = obj as Domain.Entities.IPSService;

            MiPSService miPSService = new MiPSService();
            MethodsiPSService methodsiPSService = new MethodsiPSService();
            miPSService.Code = iPSServiceEntity.Code;
            miPSService.Name = iPSServiceEntity.Name;
            miPSService.ServiceManual = Convert.ToInt16(iPSServiceEntity.ServiceManual);
            miPSService.ServiceClass = Convert.ToInt16(iPSServiceEntity.ServiceClass);
            miPSService.BillingConcept = methodsiPSService.GetBillingConcept(iPSServiceEntity.BillingConceptId ?? 0);
            miPSService.AssociatedMaterialIPSService = methodsiPSService.GetAssociatedMaterialIPSService(iPSServiceEntity.AssociatedMaterialIPSServiceId ?? 0);
            miPSService.ServiceType = Convert.ToInt16(iPSServiceEntity.ServiceType);
            miPSService.Presentation = Convert.ToInt16(iPSServiceEntity.Presentation);
            miPSService.SurgicalGroup = methodsiPSService.GetSurgicalGroup(iPSServiceEntity.SurgicalGroupId ?? 0);
            miPSService.UVRNumber = Convert.ToInt16(iPSServiceEntity.UVRNumber);
            miPSService.Score = Convert.ToInt16(iPSServiceEntity.Score);
            miPSService.ApplyChangeScore = Convert.ToInt16(iPSServiceEntity.ApplyChangeScore);
            miPSService.NewScore = Convert.ToInt16(iPSServiceEntity.NewScore);
            miPSService.InPatientRecoveryFeeType = Convert.ToInt16(iPSServiceEntity.InPatientRecoveryFeeType);
            miPSService.OutPatientRecoveryFeeType = Convert.ToInt16(iPSServiceEntity.OutPatientRecoveryFeeType);
            miPSService.AuthorizationLevel = Convert.ToInt16(iPSServiceEntity.AuthorizationLevel);
            miPSService.ContributionsWeeks = Convert.ToInt16(iPSServiceEntity.ContributionsWeeks);
            miPSService.Procedure = Convert.ToInt16(iPSServiceEntity.Procedure);
            miPSService.SubattentionCode = Convert.ToInt16(iPSServiceEntity.SubattentionCode);
            miPSService.MinimunAgeUnit = Convert.ToInt16(iPSServiceEntity.MinimunAgeUnit);
            miPSService.MinimunAge = Convert.ToInt16(iPSServiceEntity.MinimunAge);
            miPSService.MaximumAgeUnit = Convert.ToInt16(iPSServiceEntity.MaximumAgeUnit);
            miPSService.MaximumAge = Convert.ToInt16(iPSServiceEntity.MaximumAge);
            miPSService.InMale = Convert.ToInt16(iPSServiceEntity.InMale);
            miPSService.InFemale = Convert.ToInt16(iPSServiceEntity.InFemale);
            miPSService.ChildbirthAbortion = Convert.ToInt16(iPSServiceEntity.ChildbirthAbortion);
            miPSService.POS = Convert.ToInt16(iPSServiceEntity.POS);
            miPSService.ComplexityLevel = Convert.ToInt16(iPSServiceEntity.ComplexityLevel);
            miPSService.PromotionAndPrevention = Convert.ToInt16(iPSServiceEntity.PromotionAndPrevention);
            miPSService.PromotionAndPreventionActivities = iPSServiceEntity.PromotionAndPreventionActivities;
            miPSService.SurgeryArtroscopica = Convert.ToInt16(iPSServiceEntity.SurgeryArtroscopica);
            miPSService.PathologyService = Convert.ToInt16(iPSServiceEntity.PathologyService);
            miPSService.Status = Convert.ToInt16(iPSServiceEntity.Status);
            miPSService.CreationUser = iPSServiceEntity.CreationUser;
            miPSService.CreationDate = Convert.ToString(iPSServiceEntity.CreationDate);
            miPSService.ModificationUser = iPSServiceEntity.ModificationUser;
            miPSService.ModificationDate = Convert.ToString(iPSServiceEntity.ModificationDate);
            miPSService.CupsHomologation = methodsiPSService.GetCupsHomologation(iPSServiceEntity);
            miPSService.SurgicalProcedureService = methodsiPSService.GetSurgicalProcedureService(iPSServiceEntity);
            return miPSService;
        }
    }
}
