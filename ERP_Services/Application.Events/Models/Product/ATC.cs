using Application.Events.Models.InventoryMeasurementUnit;

namespace Application.Events.Models.Product
{
    public class ATC
    {
        public string Code;
        public string Name;
        public string Presentations;
        public PharmaceuticalForm PharmaceuticalForm;
        public string Concentration;
        public decimal ConcentrationQuantity;
        public WeightMeasureUnit ConcentrationMeasurementUnit;
        public int StabilityMinimumHours;
        public decimal StabilityMaximumHours;
        public int FormulationType;
        public decimal? Weight;
        public decimal? Volume;
        public WeightMeasureUnit WeightMeasureUnit;
        public WeightMeasureUnit VolumeMeasureUnit;
        public WeightMeasureUnit AdministrationUnit;
        public string Warning;
        public string Dosage;
        public int DiluentProduct;
        public decimal? Osmolarity;
        public decimal? Density;
        public int? Antibiotic;
        public PharmacologicalGroup PharmacologicalGroup;
        public AdministrationRoute[] ATCAdministrationRoute;
        public bool RequireMedicalBoard;
    }
}
