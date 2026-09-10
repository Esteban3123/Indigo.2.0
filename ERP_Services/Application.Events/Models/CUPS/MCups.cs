using Application.Events.Models.CUPS;
using Application.Events.Models.iPSService;

namespace Application.Events.Models
{
    public class MCups
    {
        public CUPSSubGroup CUPSSubGroup;
        public string Code;
        public string Description;
        public string RIPSCode;
        public string RIPSDescription;
        public string RIPSConcept;
        public BillingConcept BillingConcept;
        public BillingGroup BillingGroup;
        public int ServiceType;
        public int Status;
        public string CreationUser;
        public string CreationDate;
        public string ModificationUser;
        public string ModificationDate;
        public int MinimunAgeUnit;
        public int MinimunAge;
        public int MaximumAgeUnit;
        public int MaximumAge;
        public int Sex;
        public int ShowServiceMedicalOrder;
        public int ShowDashboardOf;
        public int TherapyProcedure;
        public int AllowDiligenceInPlace;
        public int AllowDiligenceReportRealizationQx;
        public int SerialService;
        public int RequiresInterpretation;
        public int RequiresConfirmationRealization;
        public int NutritionConsultation;
        public int PsychologyConsultation;
        public int YoungFirstTimeConsultation;
        public int AdultFirstTimeConsultation;
        public int AdvisoryPreTestElsaVIH;
        public int AdvisoryPosTestElsaVIH;
        public int NeonatalTSH;
        public int SurfaceAntigen;
        public int SerologySyphilis;
        public int ElisaVIH;
        public int Hemoglobin;
        public int Creatine;
        public int GlycosylatedHemoglobin;
        public int Microalbuminuria;
        public int HDL;
        public int DiagnosticSmearMicroscopy;
        public int PrenatalControlFirstTime;
        public int PrenatalControl;
        public int VisualAcuityAssessment;
        public int OphthalmologyConsultation;
        public int GrowthDevelopmentFirstTimeConsultation;
        public int FamilyPlanningFirstTime;
        public int Mammography;
        public int CervicalBiopsy;
        public int BreastBiopsyBacaf;
        public int BasalGlycaemia;
        public int Creatinuria;
        public int TotalCholesterol;
        public int LDL;
        public int PTH;
        public int SerineAlbumin;
        public int PhosphorusAlbumin;
        public int ApplyRIAS;
        public RIASBillingConcept RIASBillingConcept;
        public RIASBillingGroup RIASBillingGroup;
        public int OxigenService;
        public int FinancedResourceUPC;
        public int RequestRoomAutomatically;
        public CUPSEntityContractDescriptions[] CUPSEntityContractDescriptions;
    }
}
