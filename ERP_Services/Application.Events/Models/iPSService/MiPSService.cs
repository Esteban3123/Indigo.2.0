namespace Application.Events.Models.iPSService
{
    public class MiPSService
    {
        public string Code;
        public string Name;
        public int ServiceManual;
        public int ServiceClass;
        public BillingConcept BillingConcept;
        public AssociatedMaterialIPSService AssociatedMaterialIPSService;
        public int ServiceType;
        public int Presentation;
        public SurgicalGroup SurgicalGroup;
        public int UVRNumber;
        public int Score;
        public int ApplyChangeScore;
        public int NewScore;
        public int InPatientRecoveryFeeType;
        public int OutPatientRecoveryFeeType;
        public int AuthorizationLevel;
        public int ContributionsWeeks;
        public int Procedure;
        public int SubattentionCode;
        public int MinimunAgeUnit;
        public int MinimunAge;
        public int MaximumAgeUnit;
        public int MaximumAge;
        public int InMale;
        public int InFemale;
        public int ChildbirthAbortion;
        public int POS;
        public int ComplexityLevel;
        public int PromotionAndPrevention;
        public string PromotionAndPreventionActivities;
        public int SurgeryArtroscopica;
        public int PathologyService;
        public int Status;
        public string CreationUser;
        public string CreationDate;
        public string ModificationUser;
        public string ModificationDate;
        public CupsHomologation[] CupsHomologation;
        public SurgicalProcedureService[] SurgicalProcedureService;
    }
}
