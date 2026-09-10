
using Application.Inventory.ConsignmentInventoryRemission;
using Application.Inventory.EntranceVoucher;
using Application.Inventory.InventoryAdjustment;
using Application.Inventory.RemissionDevolution;
using Application.Inventory.RemissionEntrance;
using Application.Inventory.TransferOrder;
using Application.Inventory.AppEntranceVoucherDevolution;
using Infrastructure.CrossCutting.Rollback;
using System;
using System.Collections.Generic;
using Application.Inventory.LoanMerchandiseDevolution;
using Application.Inventory.PharmaceuticalDispensingDevolution;
using Application.Inventory.TransferOrderDevolution;
using Application.Inventory.PharmaceuticalDispensing;
using Application.Inventory.LoanMerchandise;

namespace Application.Inventory.Rollback
{
    public class RollbackStrategyFactory:IRollbackStrategyFactory
    {
        private readonly IDictionary<string, Type> _strategies;
        private bool disposedValue;

        public RollbackStrategyFactory()
        {
            _strategies = new Dictionary<string, Type>
            {
                {"EntranceVoucher", typeof(IEntranceVoucherAdminService)},
                {"LoanMerchandise", typeof(ILoanMerchandiseAdminService)},
                {"ConsigmentInventoryRemission", typeof(IConsignmentInventoryRemissionAdminService)},
                {"RemissionDevolution", typeof(IRemissionDevolutionAdminService)},
                {"RemissionEntrance", typeof(IRemissionEntranceAdminService)},
                {"InventoryAdjustment", typeof(IInventoryAdjustmentAdminService)},
                {"EntranceVoucherDevolution", typeof(IEntranceVoucherDevolutionAdminService)},
                {"LoanMerchandiseDevolution", typeof(ILoanMerchandiseDevolutionAdminService)},
                {"PharmaceuticalDispensingDevolution", typeof(IPharmaceuticalDispensingDevolutionAdminService)},
                {"TransferOrderDevolution", typeof(ITransferOrderDevolutionAdminService)},
                {"PharmaceuticalDispensing", typeof(IPharmaceuticalDispensingAdminService)},
                {"TransferOrder", typeof(ITransferOrderAdminService)}
            };
        }
        public Type GetStrategy(string strategyName)
        {
            if (!_strategies.TryGetValue(strategyName, out var strategyType))
            {
                throw new ArgumentException($"No se encontró un servicio asociado al tipo de transacción: {strategyName}");
            }

            return strategyType;
        }

#region "Dispose"
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: eliminar el estado administrado (objetos administrados)
                }

                // TODO: liberar los recursos no administrados (objetos no administrados) y reemplazar el finalizador
                // TODO: establecer los campos grandes como NULL
                disposedValue = true;
            }
        }

        // // TODO: reemplazar el finalizador solo si "Dispose(bool disposing)" tiene código para liberar los recursos no administrados
        // ~RollbackStrategyFactory()
        // {
        //     // No cambie este código. Coloque el código de limpieza en el método "Dispose(bool disposing)".
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en el método "Dispose(bool disposing)".
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
#endregion

    }
}
