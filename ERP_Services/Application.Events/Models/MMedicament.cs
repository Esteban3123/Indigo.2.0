namespace Application.Events.Models
{
    /// <summary>
    /// Propiedades Medicamento
    /// </summary>
    public class MMedicament
    {
        public string DCICode;
        public string ATCEntityCode;
        public string Code;
        public string Name;
        public string AbbreviationName;
        public MAdministrationRoute AdministrationRoute;
        public MATCAdministrationRoute[] ATCAdministrationRoute;
        public PharmacologicalGroup PharmacologicalGroup;
        public string Presentations;
        public string Concentration;
        public InventoryRiskLevel InventoryRiskLevel;
        public int StabilityMinimumHours;
        public decimal StabilityMaximumHours;
        public int FormulationType;
        public decimal? Weight;
        public WeightMeasureUnit WeightMeasureUnit;
        public decimal? Volume;
        public WeightMeasureUnit VolumeMeasureUnit;
        public WeightMeasureUnit AdministrationUnit;
        public string Warning;
        public string WarningHtml;
        public string Dosage;
        public string DosageHtml;
        public string Indications;
        public string IndicationsHtml;
        public string ContraIndications;
        public string ContraIndicationsHtml;
        public string Precautions;
        public string PrecautionsHtml;
        public string AdverseReactions;
        public string AdverseReactionsHtml;
        public int AutomaticCalculation;
        public int TransferSurplusProduct;
        public int DiluentProduct;
        public int SupplieProduct;
        public int JustificationForSpecialDrugs;
        public int JustificationOfInputs;
        public int IndicatorDrug;
        public int? POSProduct;
        public int? AllPOSPathologies;
        public BillingGroupNoPos BillingGroupNoPos;
        public int Status;
        public string CreationUser;
        public string CreationDate;
        public string ModificationUser;
        public string ModificationDate;
        public int ProductNPT;
        public decimal? Osmolarity;
        public decimal? Density;
        public int? Consumption;
        public PharmaceuticalForm PharmaceuticalForm;
        public int? HasSupplieMedicine;
        public MRelatedSupplieMedicine[] RelatedSupplieMedicine;
        public int? Antibiotic;
        public int? RequireMedicalBoard;
        public int? HighCost;
        public int? DefineProfessional;
        public string ClinicalJustification;
        public int? Conditioned;
        public int UNIRS;
        public byte Multidose;
        public byte Stability;
        public byte? SuitableForReconstitution;
        public byte Combined;
        public decimal ConcentrationQuantity;
        public string ConcentrationMeasureUnitCode;
        public string UPRUnitsCode;
    }
}
