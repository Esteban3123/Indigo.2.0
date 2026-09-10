using Application.Events.Models;
using Application.Events.Models.inventoryRiskLevel;
using System;

namespace Application.Events.Serializers
{
    public class inventoryRiskLevel : IDittoDocument
    {
        public object Generate(object obj, QueueParameter parameter)
        {
            Domain.Entities.InventoryRiskLevel inventoryRiskLevel = obj as Domain.Entities.InventoryRiskLevel;

            MinventoryRiskLevel minventoryRiskLevel = new MinventoryRiskLevel();
            minventoryRiskLevel.Code = inventoryRiskLevel.Code;
            minventoryRiskLevel.Name = inventoryRiskLevel.Name;
            minventoryRiskLevel.CreationUser = inventoryRiskLevel.CreationUser;
            minventoryRiskLevel.CreationDate = Convert.ToString(inventoryRiskLevel.CreationDate);
            minventoryRiskLevel.ModificationUser = inventoryRiskLevel.ModificationUser;
            minventoryRiskLevel.ModificationDate = Convert.ToString(inventoryRiskLevel.ModificationDate);
            return minventoryRiskLevel;
        }
    }
}
