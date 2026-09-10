namespace Application.Events.Models.DCI
{
    public class DrugInteraction
    {        
        public DCI DCI { get; set; }
        public int RiskLevel { get; set; }
        public string Description { get; set; }
        public int IsDelete { get; set; }
    }
}
