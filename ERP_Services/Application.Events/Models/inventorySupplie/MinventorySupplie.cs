using Application.Events.Models.inventoryRiskLevel;

namespace Application.Events.Models.inventorySupplie
{
    public class MinventorySupplie
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public inventoryRiskLevelSupplie InventoryRiskLevel { get; set; }
        public int PBSProduct { get; set; }
        public int Status { get; set; }
        public string CreationUser { get; set; }
        public string CreationDate { get; set; }
        public string ModificationUser { get; set; }
        public string ModificationDate { get; set; }
        public int JustificationOfInputs { get; set; }
        public int OsteosynthesisMaterial { get; set; }
        public int Consumption { get; set; }               
    }
}
