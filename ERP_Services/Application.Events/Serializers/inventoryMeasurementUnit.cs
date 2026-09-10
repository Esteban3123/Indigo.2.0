using Application.Events.Models;
using Application.Events.Models.InventoryMeasurementUnit;
using System;

namespace Application.Events.Serializers
{
    class inventoryMeasurementUnit : IDittoDocument
    {
        public object Generate(object obj, QueueParameter parameter)
        {
            Domain.Entities.InventoryMeasurementUnit inventoryMeasurementUnit = obj as Domain.Entities.InventoryMeasurementUnit;

            MinventoryMeasurementUnit minventoryMeasurementUnit = new MinventoryMeasurementUnit();
            minventoryMeasurementUnit.Name = inventoryMeasurementUnit.Name;
            minventoryMeasurementUnit.Code = inventoryMeasurementUnit.Code;
            minventoryMeasurementUnit.Abbreviation = inventoryMeasurementUnit.Abbreviation;
            minventoryMeasurementUnit.UnitType = Convert.ToInt16(inventoryMeasurementUnit.UnitType);
            minventoryMeasurementUnit.Status = Convert.ToInt16(inventoryMeasurementUnit.Status);
            minventoryMeasurementUnit.CreationUser = Convert.ToString(inventoryMeasurementUnit.CreationUser);
            minventoryMeasurementUnit.CreationDate = Convert.ToString(inventoryMeasurementUnit.CreationDate);
            minventoryMeasurementUnit.ModificationUser = Convert.ToString(inventoryMeasurementUnit.ModificationUser);
            minventoryMeasurementUnit.ModificationDate = Convert.ToString(inventoryMeasurementUnit.ModificationDate);
            minventoryMeasurementUnit.CrystalMeasurementUnit = inventoryMeasurementUnit.Code;
            minventoryMeasurementUnit.AllowEditCostValue = Convert.ToInt16(inventoryMeasurementUnit.AllowEditCostValue);
            minventoryMeasurementUnit.CostValue = Convert.ToString(inventoryMeasurementUnit.CostValue);
            return minventoryMeasurementUnit;
        }
    }
}
