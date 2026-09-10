namespace Application.Events.Models
{
    /// <summary>
    /// Unidad de medida del peso, solo se llena si el tipo de formulacion es peso o peso - Volumen.
    /// </summary>
    public class WeightMeasureUnit
    {
        public string Code;
        public string Name;
        public string Abbreviation;
        public int FormulationType;
        public int Status;
        public string CrystalMeasurementUnit;
        public int AllowEditCostValue;
        public decimal CostValue;


    }
}
