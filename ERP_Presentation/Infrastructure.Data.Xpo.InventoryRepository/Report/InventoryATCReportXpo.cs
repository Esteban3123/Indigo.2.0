using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.ATC")]
    public class InventoryATCReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        int fDCIId;
        public int DCIId
        {
            get { return fDCIId; }
            set { SetPropertyValue<int>("DCIId", ref fDCIId, value); }
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
        string fAbbreviationName;
        public string AbbreviationName
        {
            get { return fAbbreviationName; }
            set { SetPropertyValue<string>("AbbreviationName", ref fAbbreviationName, value); }
        }
        InventoryAdministrationRouteReportXpo fAdministrationRouteId;
        [Association(@"Inventory_ATCReferencesInventory_AdministrationRoute")]
        public InventoryAdministrationRouteReportXpo AdministrationRouteId
        {
            get { return fAdministrationRouteId; }
            set { SetPropertyValue<InventoryAdministrationRouteReportXpo>("AdministrationRouteId", ref fAdministrationRouteId, value); }
        }
        InventoryPharmacologicalGroupReportXpo fPharmacologicalGroupId;
        [Association(@"InventoryATCReportXpoReferencesInventoryPharmacologicalGroupReportXpo")]
        public InventoryPharmacologicalGroupReportXpo PharmacologicalGroupId
        {
            get { return fPharmacologicalGroupId; }
            set { SetPropertyValue<InventoryPharmacologicalGroupReportXpo>("PharmacologicalGroupId", ref fPharmacologicalGroupId, value); }
        }

        InventoryPharmaceuticalFormReportXpo fPharmaceuticalFormId;
        [Association(@"InventoryATCReportXpoReferencesInventory_PharmaceuticalForm")]
        public InventoryPharmaceuticalFormReportXpo PharmaceuticalFormId
        {
            get { return fPharmaceuticalFormId; }
            set { SetPropertyValue<InventoryPharmaceuticalFormReportXpo>("PharmaceuticalFormId", ref fPharmaceuticalFormId, value); }
        }

        /*
        int fPharmacologicalGroupId;
        public int PharmacologicalGroupId
        {
            get { return fPharmacologicalGroupId; }
            set { SetPropertyValue<int>("PharmacologicalGroupId", ref fPharmacologicalGroupId, value); }
        }*/
        string fConcentration;
        [Size(50)]
        public string Concentration
        {
            get { return fConcentration; }
            set { SetPropertyValue<string>("Concentration", ref fConcentration, value); }
        }
        int fInventoryRiskLevelId;
        public int InventoryRiskLevelId
        {
            get { return fInventoryRiskLevelId; }
            set { SetPropertyValue<int>("InventoryRiskLevelId", ref fInventoryRiskLevelId, value); }
        }
        int fStabilityMinimumHours;
        public int StabilityMinimumHours
        {
            get { return fStabilityMinimumHours; }
            set { SetPropertyValue<int>("StabilityMinimumHours", ref fStabilityMinimumHours, value); }
        }
        int fStabilityMaximumHours;
        public int StabilityMaximumHours
        {
            get { return fStabilityMaximumHours; }
            set { SetPropertyValue<int>("StabilityMaximumHours", ref fStabilityMaximumHours, value); }
        }
        byte fFormulationType;
        public byte FormulationType
        {
            get { return fFormulationType; }
            set { SetPropertyValue<byte>("FormulationType", ref fFormulationType, value); }
        }
        decimal fWeight;
        public decimal Weight
        {
            get { return fWeight; }
            set { SetPropertyValue<decimal>("Weight", ref fWeight, value); }
        }
        InventoryMeasurementUnitReportXpo fWeightMeasureUnit;
        [Association(@"InventoryATCReportXpo3ReferencesInventoryMeasurementUnitReportXpo")]
        public InventoryMeasurementUnitReportXpo WeightMeasureUnit
        {
            get { return fWeightMeasureUnit; }
            set { SetPropertyValue<InventoryMeasurementUnitReportXpo>("WeightMeasureUnit", ref fWeightMeasureUnit, value); }
        }
        decimal fVolume;
        public decimal Volume
        {
            get { return fVolume; }
            set { SetPropertyValue<decimal>("Volume", ref fVolume, value); }
        }
        InventoryMeasurementUnitReportXpo fVolumeMeasureUnit;
        [Association(@"InventoryATCReportXpo2ReferencesInventoryMeasurementUnitReportXpo")]
        public InventoryMeasurementUnitReportXpo VolumeMeasureUnit
        {
            get { return fVolumeMeasureUnit; }
            set { SetPropertyValue<InventoryMeasurementUnitReportXpo>("VolumeMeasureUnit", ref fVolumeMeasureUnit, value); }
        }
        InventoryMeasurementUnitReportXpo fAdministrationUnitId;
        [Association(@"InventoryATCReportXpoReferencesInventoryMeasurementUnitReportXpo")]
        public InventoryMeasurementUnitReportXpo AdministrationUnitId
        {
            get { return fAdministrationUnitId; }
            set { SetPropertyValue<InventoryMeasurementUnitReportXpo>("AdministrationUnitId", ref fAdministrationUnitId, value); }
        }        
        string fWarning;
        [Size(SizeAttribute.Unlimited)]
        public string Warning
        {
            get { return fWarning; }
            set { SetPropertyValue<string>("Warning", ref fWarning, value); }
        }
        string fWarningHtml;
        [Size(SizeAttribute.Unlimited)]
        public string WarningHtml
        {
            get { return fWarningHtml; }
            set { SetPropertyValue<string>("WarningHtml", ref fWarningHtml, value); }
        }
        string fDosage;
        [Size(SizeAttribute.Unlimited)]
        public string Dosage
        {
            get { return fDosage; }
            set { SetPropertyValue<string>("Dosage", ref fDosage, value); }
        }
        string fDosageHtml;
        [Size(SizeAttribute.Unlimited)]
        public string DosageHtml
        {
            get { return fDosageHtml; }
            set { SetPropertyValue<string>("DosageHtml", ref fDosageHtml, value); }
        }
        string fIndications;
        [Size(SizeAttribute.Unlimited)]
        public string Indications
        {
            get { return fIndications; }
            set { SetPropertyValue<string>("Indications", ref fIndications, value); }
        }
        string fIndicationsHtml;
        [Size(SizeAttribute.Unlimited)]
        public string IndicationsHtml
        {
            get { return fIndicationsHtml; }
            set { SetPropertyValue<string>("IndicationsHtml", ref fIndicationsHtml, value); }
        }
        string fContraIndications;
        [Size(SizeAttribute.Unlimited)]
        public string ContraIndications
        {
            get { return fContraIndications; }
            set { SetPropertyValue<string>("ContraIndications", ref fContraIndications, value); }
        }
        string fContraIndicationsHtml;
        [Size(SizeAttribute.Unlimited)]
        public string ContraIndicationsHtml
        {
            get { return fContraIndicationsHtml; }
            set { SetPropertyValue<string>("ContraIndicationsHtml", ref fContraIndicationsHtml, value); }
        }
        string fPrecautions;
        [Size(SizeAttribute.Unlimited)]
        public string Precautions
        {
            get { return fPrecautions; }
            set { SetPropertyValue<string>("Precautions", ref fPrecautions, value); }
        }
        string fPrecautionsHtml;
        [Size(SizeAttribute.Unlimited)]
        public string PrecautionsHtml
        {
            get { return fPrecautionsHtml; }
            set { SetPropertyValue<string>("PrecautionsHtml", ref fPrecautionsHtml, value); }
        }
        string fAdverseReactions;
        [Size(SizeAttribute.Unlimited)]
        public string AdverseReactions
        {
            get { return fAdverseReactions; }
            set { SetPropertyValue<string>("AdverseReactions", ref fAdverseReactions, value); }
        }
        string fAdverseReactionsHtml;
        [Size(SizeAttribute.Unlimited)]
        public string AdverseReactionsHtml
        {
            get { return fAdverseReactionsHtml; }
            set { SetPropertyValue<string>("AdverseReactionsHtml", ref fAdverseReactionsHtml, value); }
        }
        bool fAutomaticCalculation;
        public bool AutomaticCalculation
        {
            get { return fAutomaticCalculation; }
            set { SetPropertyValue<bool>("AutomaticCalculation", ref fAutomaticCalculation, value); }
        }
        bool fTransferSurplusProduct;
        public bool TransferSurplusProduct
        {
            get { return fTransferSurplusProduct; }
            set { SetPropertyValue<bool>("TransferSurplusProduct", ref fTransferSurplusProduct, value); }
        }
        bool fDiluentProduct;
        public bool DiluentProduct
        {
            get { return fDiluentProduct; }
            set { SetPropertyValue<bool>("DiluentProduct", ref fDiluentProduct, value); }
        }
        bool fJustificationForSpecialDrugs;
        public bool JustificationForSpecialDrugs
        {
            get { return fJustificationForSpecialDrugs; }
            set { SetPropertyValue<bool>("JustificationForSpecialDrugs", ref fJustificationForSpecialDrugs, value); }
        }
        bool fJustificationOfInputs;
        public bool JustificationOfInputs
        {
            get { return fJustificationOfInputs; }
            set { SetPropertyValue<bool>("JustificationOfInputs", ref fJustificationOfInputs, value); }
        }
        bool fIndicatorDrug;
        public bool IndicatorDrug
        {
            get { return fIndicatorDrug; }
            set { SetPropertyValue<bool>("IndicatorDrug", ref fIndicatorDrug, value); }
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
        public DateTime ModificationDate
        {
            get { return fModificationDate; }
            set { SetPropertyValue<DateTime>("ModificationDate", ref fModificationDate, value); }
        }
        [Association(@"InventoryProductReportXpoReferencesInventoryATCReportXpo", typeof(InventoryProductReportXpo))]
        public XPCollection<InventoryProductReportXpo> InventoryProductReportXpo { get { return GetCollection<InventoryProductReportXpo>("InventoryProductReportXpo"); } }
                    
        public InventoryATCReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
