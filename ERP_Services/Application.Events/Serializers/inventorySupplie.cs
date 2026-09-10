using Application.Events.Models;
using Application.Events.Models.inventorySupplie;
using System;

namespace Application.Events.Serializers
{
    public class inventorySupplie : IDittoDocument
    {
        /// <summary>
        /// Insumos
        /// </summary>
        /// <returns></returns>
        public object Generate(object obj, QueueParameter parameter)
        {
            Domain.Entities.InventorySupplie inventorySupplie = obj as Domain.Entities.InventorySupplie;

            MinventorySupplie minventorySupplie = new MinventorySupplie();
            MethodsinventorySupplie methodsinventorySupplie = new MethodsinventorySupplie();
            minventorySupplie.Code = inventorySupplie.Code;
            minventorySupplie.Name = inventorySupplie.SupplieName;
            minventorySupplie.InventoryRiskLevel = methodsinventorySupplie.GetMinventoryRiskLevel(inventorySupplie.RiskLevelId);
            minventorySupplie.PBSProduct = Convert.ToInt16(inventorySupplie.PBSProduct);
            minventorySupplie.Status = Convert.ToInt16(inventorySupplie.SupplieStatus);
            minventorySupplie.CreationUser = inventorySupplie.CreationUser;
            minventorySupplie.CreationUser = Convert.ToString(inventorySupplie.CreationUser);
            minventorySupplie.ModificationUser = inventorySupplie.ModificationUser;
            minventorySupplie.ModificationDate = Convert.ToString(inventorySupplie.ModificationDate);
            minventorySupplie.JustificationOfInputs = Convert.ToInt16(inventorySupplie.JustificationOfInputs);
            minventorySupplie.OsteosynthesisMaterial = Convert.ToInt16(inventorySupplie.OsteosynthesisMaterial);
            minventorySupplie.Consumption = Convert.ToInt16(inventorySupplie.Consumption);
            return minventorySupplie;
        }
    }
}
