using Application.Events.Serializers;
using System;

namespace Application.Events.Models.CUPS
{
    public class cUPSEntity : IDittoDocument
    {
        public object Generate(object obj, QueueParameter parameter)
        {
            Domain.Entities.CUPSEntity cUPSEntity = obj as Domain.Entities.CUPSEntity;

            MCups mCups = new MCups();
            MethodsCups methodsCups = new MethodsCups();
            mCups.CUPSSubGroup = methodsCups.GenerateCUPSSubGroup(cUPSEntity.CUPSSubGroupId);
            mCups.Code = cUPSEntity.Code;
            mCups.Description = cUPSEntity.Description;
            mCups.RIPSCode = cUPSEntity.RIPSCode;
            mCups.RIPSDescription = cUPSEntity.RIPSDescription;
            mCups.RIPSConcept = cUPSEntity.RIPSConcept;
            mCups.BillingConcept = methodsCups.GenerateBillingConcept(cUPSEntity.BillingConceptId);
            mCups.BillingGroup = methodsCups.GenerateBillingGroup(cUPSEntity.BillingGroupId);
            mCups.ServiceType = cUPSEntity.ServiceType;
            mCups.Status = Convert.ToInt16(cUPSEntity.Status);
            mCups.CreationUser = cUPSEntity.CreationUser;
            mCups.CreationDate = Convert.ToString(cUPSEntity.CreationDate);
            mCups.ModificationUser = cUPSEntity.ModificationUser;
            mCups.ModificationDate = Convert.ToString(cUPSEntity.ModificationDate);
            mCups.MinimunAgeUnit = cUPSEntity.MinimunAgeUnit;
            mCups.MinimunAge = cUPSEntity.MinimunAge;
            mCups.MaximumAgeUnit = cUPSEntity.MaximumAgeUnit;
            mCups.MaximumAge = cUPSEntity.MaximumAge;
            mCups.Sex = cUPSEntity.Sex;
            mCups.ShowServiceMedicalOrder = Convert.ToInt16(cUPSEntity.ShowServiceMedicalOrder);
            mCups.ShowDashboardOf = Convert.ToInt16(cUPSEntity.ShowDashboardOf);
            mCups.TherapyProcedure = Convert.ToInt16(cUPSEntity.TherapyProcedure);
            mCups.AllowDiligenceInPlace = Convert.ToInt16(cUPSEntity.AllowDiligenceInPlace);
            mCups.AllowDiligenceReportRealizationQx = Convert.ToInt16(cUPSEntity.AllowDiligenceReportRealizationQx);
            mCups.SerialService = Convert.ToInt16(cUPSEntity.SerialService);
            mCups.RequiresInterpretation = Convert.ToInt16(cUPSEntity.RequiresInterpretation);
            mCups.RequiresConfirmationRealization = Convert.ToInt16(cUPSEntity.RequiresConfirmationRealization);
            mCups.NutritionConsultation = Convert.ToInt16(cUPSEntity.NutritionConsultation);
            mCups.PsychologyConsultation = Convert.ToInt16(cUPSEntity.PsychologyConsultation);
            mCups.YoungFirstTimeConsultation = Convert.ToInt16(cUPSEntity.YoungFirstTimeConsultation);
            mCups.AdultFirstTimeConsultation = Convert.ToInt16(cUPSEntity.AdultFirstTimeConsultation);
            mCups.AdvisoryPreTestElsaVIH = Convert.ToInt16(cUPSEntity.AdvisoryPreTestElsaVIH);
            mCups.AdvisoryPosTestElsaVIH = Convert.ToInt16(cUPSEntity.AdvisoryPosTestElsaVIH);
            mCups.NeonatalTSH = Convert.ToInt16(cUPSEntity.NeonatalTSH);
            mCups.SurfaceAntigen = Convert.ToInt16(cUPSEntity.SurfaceAntigen);
            mCups.SerologySyphilis = Convert.ToInt16(cUPSEntity.SerologySyphilis);
            mCups.ElisaVIH = Convert.ToInt16(cUPSEntity.ElisaVIH);
            mCups.Hemoglobin = Convert.ToInt16(cUPSEntity.Hemoglobin);
            mCups.Creatine = Convert.ToInt16(cUPSEntity.Creatine);
            mCups.GlycosylatedHemoglobin = Convert.ToInt16(cUPSEntity.GlycosylatedHemoglobin);
            mCups.Microalbuminuria = Convert.ToInt16(cUPSEntity.Microalbuminuria);
            mCups.HDL = Convert.ToInt16(cUPSEntity.HDL);
            mCups.DiagnosticSmearMicroscopy = Convert.ToInt16(cUPSEntity.DiagnosticSmearMicroscopy);
            mCups.PrenatalControlFirstTime = Convert.ToInt16(cUPSEntity.PrenatalControlFirstTime);
            mCups.PrenatalControl = Convert.ToInt16(cUPSEntity.PrenatalControl);
            mCups.VisualAcuityAssessment = Convert.ToInt16(cUPSEntity.VisualAcuityAssessment);
            mCups.OphthalmologyConsultation = Convert.ToInt16(cUPSEntity.OphthalmologyConsultation);
            mCups.GrowthDevelopmentFirstTimeConsultation = Convert.ToInt16(cUPSEntity.GrowthDevelopmentFirstTimeConsultation);
            mCups.FamilyPlanningFirstTime = Convert.ToInt16(cUPSEntity.FamilyPlanningFirstTime);
            mCups.Mammography = Convert.ToInt16(cUPSEntity.Mammography);
            mCups.CervicalBiopsy = Convert.ToInt16(cUPSEntity.CervicalBiopsy);
            mCups.BreastBiopsyBacaf = Convert.ToInt16(cUPSEntity.BreastBiopsyBacaf);
            mCups.BasalGlycaemia = Convert.ToInt16(cUPSEntity.BasalGlycaemia);
            mCups.Creatinuria = Convert.ToInt16(cUPSEntity.Creatinuria);
            mCups.TotalCholesterol = Convert.ToInt16(cUPSEntity.TotalCholesterol);
            mCups.LDL = Convert.ToInt16(cUPSEntity.LDL);
            mCups.PTH = Convert.ToInt16(cUPSEntity.PTH);
            mCups.SerineAlbumin = Convert.ToInt16(cUPSEntity.SerineAlbumin);
            mCups.PhosphorusAlbumin = Convert.ToInt16(cUPSEntity.PhosphorusAlbumin);
            mCups.ApplyRIAS = Convert.ToInt16(cUPSEntity.ApplyRIAS);
            mCups.RIASBillingConcept = methodsCups.GenerateRIASBillingConcept(cUPSEntity.RIASBillingConceptId ?? 0);
            mCups.RIASBillingGroup = methodsCups.GenerateRIASBillingGroup(cUPSEntity.RIASBillingGroupId ?? 0);
            mCups.OxigenService = Convert.ToInt16(cUPSEntity.OxigenService);
            mCups.FinancedResourceUPC = Convert.ToInt16(cUPSEntity.FinancedResourceUPC);
            mCups.RequestRoomAutomatically = Convert.ToInt16(cUPSEntity.RequestRoomAutomatically);
            mCups.CUPSEntityContractDescriptions = methodsCups.GenerateCUPSEntityContractDescriptions(cUPSEntity);
            return mCups;
        }
    }
}
