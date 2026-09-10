using Application.Inventory.Sequense;
using Application.Inventory.Warehouse;
using DistributedService.SCM.Unity;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using Unity;

namespace DistributedService.SCM.Utilities
{
    public class CurrentSequenseUtils
    {
        public static (int, InventorySequence) GetIdCurrentSequense(string code, string idForm, int idWarehouse, int operativeUnitId, AuditMessage audit)
        {
            int idCurrentSequense = 0;
            var sequenceAdminService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventorySequenceAdminService>();
            var warehouseAdminService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IWarehouseAdminService>();

            InventorySequence inventorySequense = sequenceAdminService.GetSequenseByIdForm(idForm);
            if (inventorySequense.IsManual && String.IsNullOrEmpty(code))
            {
                throw new Exception("La secuencia númerica es Manual, por lo tanto debe escribir un Código.");
            }
            else
            {
                if (inventorySequense.Scope.Equals("O")) //El ambito es a nivel de organización
                {
                    var warehouseResult = warehouseAdminService.GetWarehouseById(idWarehouse, audit);
                    if (!warehouseResult.StateResult)
                    {
                        throw new Exception(warehouseResult.Message);
                    }
                    var inventorySequenseDetail = sequenceAdminService.GetSequenseByPrefix(warehouseResult.ObjectEmbbeded.Prefix, inventorySequense.Id);
                    idCurrentSequense = inventorySequenseDetail.Id;
                }
                else if (inventorySequense.Scope.Equals("OU")) //El ambito es a nivel de unidad operativa
                {
                    idCurrentSequense = (int)sequenceAdminService.GetCurrentSequenceByIdForm(idForm, operativeUnitId);
                }
            }
            return (idCurrentSequense, inventorySequense);
        }
    }
}