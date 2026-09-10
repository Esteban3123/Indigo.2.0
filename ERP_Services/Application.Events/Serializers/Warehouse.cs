using Application.Events.Models;
using Application.Events.Models.Warehouse;
using System;

namespace Application.Events.Serializers
{
    public class Warehouse : IDittoDocument
    {
        public object Generate(object obj, QueueParameter parameter)
        {
            Domain.Entities.Warehouse warehouseEntity = obj as Domain.Entities.Warehouse;
            MWarehouse warehouse = new MWarehouse();
            warehouse.Code = warehouseEntity.Code;
            warehouse.Name = warehouseEntity.Name;
            warehouse.Status = Convert.ToInt16(warehouseEntity.Status);
            warehouse.WareHouseType = warehouseEntity.WareHouseType;
            warehouse.CreationUser = warehouseEntity.CreationUser;
            warehouse.CreationDate = Convert.ToString(warehouseEntity.CreationDate);
            warehouse.ModificationUser = warehouseEntity.ModificationUser;
            warehouse.ModificationDate = Convert.ToString(warehouseEntity.ModificationDate);

            return warehouse;

        }
    }
}
