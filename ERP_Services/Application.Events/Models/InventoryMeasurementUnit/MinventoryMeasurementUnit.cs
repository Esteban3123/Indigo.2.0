using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Events.Models.InventoryMeasurementUnit
{
    /// <summary>
    /// Inventory Measurement Unit
    /// </summary>
    public class MinventoryMeasurementUnit
    {
        public string Code;
        public string Name;
        public string Abbreviation;
        public int UnitType;
        public int Status;
        public string CreationUser;
        public string CreationDate;
        public string ModificationUser;
        public string ModificationDate;
        public string CrystalMeasurementUnit;
        public int AllowEditCostValue;
        public string CostValue;
    }
}
