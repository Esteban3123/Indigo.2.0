using DevExpress.Xpo;
using System;

namespace Infrastructure.Data.Xpo.InventoryRepository.Relation
{

    [Persistent(@"Contract.CareGroup")]
    public partial class ContractCareGroupXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        string fCode;
        [Size(20)]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        string fName;
        [Size(200)]
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }

        byte fCareGroupType;
        public byte CareGroupType
        {
            get { return fCareGroupType; }
            set { SetPropertyValue<byte>("CareGroupType", ref fCareGroupType, value); }
        }

        byte fApplyRIAS;
        public byte ApplyRIAS
        {
            get { return fApplyRIAS; }
            set { SetPropertyValue<byte>("ApplyRIAS", ref fApplyRIAS, value); }
        }

        byte fDefaultManual;
        public byte DefaultManual
        {
            get { return fDefaultManual; }
            set { SetPropertyValue<byte>("DefaultManual", ref fDefaultManual, value); }
        }

        int fContractId;
        public int ContractId
        {
            get { return fContractId; }
            set { SetPropertyValue<int>("ContractId", ref fContractId, value); }
        }

        byte fLiquidationType;
        public byte LiquidationType
        {
            get { return fLiquidationType; }
            set { SetPropertyValue<byte>("LiquidationType", ref fLiquidationType, value); }
        }

        int fCostCenterId;
        public int CostCenterId
        {
            get { return fCostCenterId; }
            set { SetPropertyValue<int>("CostCenterId", ref fCostCenterId, value); }
        }

        byte fBillingPeriod;
        public byte BillingPeriod
        {
            get { return fBillingPeriod; }
            set { SetPropertyValue<byte>("BillingPeriod", ref fBillingPeriod, value); }
        }

        double fMaximumIndividualBilling;
        public double MaximumIndividualBilling
        {
            get { return fMaximumIndividualBilling; }
            set { SetPropertyValue<double>("MaximumIndividualBilling", ref fMaximumIndividualBilling, value); }
        }

        double fPeriodMaximumBilling;
        public double PeriodMaximumBilling
        {
            get { return fPeriodMaximumBilling; }
            set { SetPropertyValue<double>("PeriodMaximumBilling", ref fPeriodMaximumBilling, value); }
        }

        byte fTypeLiquidationEmergencyStays;
        public byte TypeLiquidationEmergencyStays
        {
            get { return fTypeLiquidationEmergencyStays; }
            set { SetPropertyValue<byte>("TypeLiquidationEmergencyStays", ref fTypeLiquidationEmergencyStays, value); }
        }

        byte fMinimumObservationTime;
        public byte MinimumObservationTime
        {
            get { return fMinimumObservationTime; }
            set { SetPropertyValue<byte>("MinimumObservationTime", ref fMinimumObservationTime, value); }
        }

        byte fMaximumObservationTime;
        public byte MaximumObservationTime
        {
            get { return fMaximumObservationTime; }
            set { SetPropertyValue<byte>("MaximumObservationTime", ref fMaximumObservationTime, value); }
        }

        byte fHoursOfRecoveryIncluded;
        public byte HoursOfRecoveryIncluded
        {
            get { return fHoursOfRecoveryIncluded; }
            set { SetPropertyValue<byte>("HoursOfRecoveryIncluded", ref fHoursOfRecoveryIncluded, value); }
        }

        int fRequirementsTemplateId;
        public int RequirementsTemplateId
        {
            get { return fRequirementsTemplateId; }
            set { SetPropertyValue<int>("RequirementsTemplateId", ref fRequirementsTemplateId, value); }
        }

        int fInvoiceDeadlines;
        public int InvoiceDeadlines
        {
            get { return fInvoiceDeadlines; }
            set { SetPropertyValue<int>("InvoiceDeadlines", ref fInvoiceDeadlines, value); }
        }

        int fProcedureTemplateId;
        public int ProcedureTemplateId
        {
            get { return fProcedureTemplateId; }
            set { SetPropertyValue<int>("ProcedureTemplateId", ref fProcedureTemplateId, value); }
        }

        int fProductRateId;
        public int ProductRateId
        {
            get { return fProductRateId; }
            set { SetPropertyValue<int>("ProductRateId", ref fProductRateId, value); }
        }

        byte fConceptToBill;
        public byte ConceptToBill
        {
            get { return fConceptToBill; }
            set { SetPropertyValue<byte>("ConceptToBill", ref fConceptToBill, value); }
        }

        byte fEntityType;
        public byte EntityType
        {
            get { return fEntityType; }
            set { SetPropertyValue<byte>("EntityType", ref fEntityType, value); }
        }

        int fContractAccountingStructureId;
        public int ContractAccountingStructureId
        {
            get { return fContractAccountingStructureId; }
            set { SetPropertyValue<int>("ContractAccountingStructureId", ref fContractAccountingStructureId, value); }
        }

        bool fAffectBudget;
        public bool AffectBudget
        {
            get { return fAffectBudget; }
            set { SetPropertyValue<bool>("AffectBudget", ref fAffectBudget, value); }
        }

        bool fExtramuralPharmaceuticalDispensing;
        public bool ExtramuralPharmaceuticalDispensing
        {
            get { return fExtramuralPharmaceuticalDispensing; }
            set { SetPropertyValue<bool>("ExtramuralPharmaceuticalDispensing", ref fExtramuralPharmaceuticalDispensing, value); }
        }

        bool fStatus;
        public bool Status
        {
            get { return fStatus; }
            set { SetPropertyValue<bool>("Status", ref fStatus, value); }
        }

        string fCreationUser;
        [Size(20)]
        public string CreationUser
        {
            get { return fCreationUser; }
            set { SetPropertyValue<string>("CreationUser", ref fCreationUser, value); }
        }

        DateTime fCreationDate;
        [Size(20)]
        public DateTime CreationDate
        {
            get { return fCreationDate; }
            set { SetPropertyValue<DateTime>("CreationDate", ref fCreationDate, value); }
        }

        string fModificationUser;
        [Size(20)]
        public string ModificationUser
        {
            get { return fModificationUser; }
            set { SetPropertyValue<string>("ModificationUser", ref fModificationUser, value); }
        }

        DateTime fModificationDate;
        [Size(20)]
        public DateTime ModificationDate
        {
            get { return fModificationDate; }
            set { SetPropertyValue<DateTime>("ModificationDate", ref fModificationDate, value); }
        }

        [Size(50)]
        [PersistentAlias("concat(concat(Code,' - '),Name)")]
        public string CodeName
        {
            get { return Convert.ToString(this.EvaluateAlias("CodeName")); }
        }

        [Association(@"MedicalFormulaReferencesCareGroup", typeof(MedicalFormulaXpo))]
        public XPCollection<MedicalFormulaXpo> MedicalFormulaXpo { get { return GetCollection<MedicalFormulaXpo>("MedicalFormulaXpo"); } }

        public ContractCareGroupXpo(Session session) : base(session) { }
    }
}
