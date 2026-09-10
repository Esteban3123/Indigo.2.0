using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Inventory.DecreaseMaximumLimit
{
   
    public class DecreaseMaximumLimitAdminService : IDecreaseMaximumLimitAdminService
    {
        #region Fields
        private IDecreaseMaximumLimitRepository _decreaseMaximumLimitRepository;
        private IConsignmentInventoryRemissionDetailBatchSerialRepository _consignmentInventoryDetailBatchSerialRepository;
        #endregion

        #region Builder
        public DecreaseMaximumLimitAdminService(IDecreaseMaximumLimitRepository decreaseMaximumLimitRepository, IConsignmentInventoryRemissionDetailBatchSerialRepository consignmentInventoryDetailBatchSerialRepository)
        {
            _decreaseMaximumLimitRepository = decreaseMaximumLimitRepository;
            _consignmentInventoryDetailBatchSerialRepository = consignmentInventoryDetailBatchSerialRepository;
        }
        #endregion
        public ActionResult SaveDecreaseProduct(List<Domain.Entities.DecreaseMaximumLimit> listDecrease, AuditMessage audit)
        {
            try
            {
                foreach (Domain.Entities.DecreaseMaximumLimit item in listDecrease)
                {
                    Domain.Entities.DecreaseMaximumLimit decreaseMaximumLimit = _decreaseMaximumLimitRepository.GetByFilter(x => x.ProductId == item.ProductId && x.WarehouseId == item.WarehouseId).FirstOrDefault();
                    if (decreaseMaximumLimit != null)
                    {
                        decreaseMaximumLimit.Justification = item.Justification;
                        decreaseMaximumLimit.Quantity = item.Quantity;
                        decreaseMaximumLimit.ModificationDate = DateTime.Now;                       
                    }
                    else
                    {
                        decreaseMaximumLimit = new Domain.Entities.DecreaseMaximumLimit();
                        decreaseMaximumLimit.Quantity = item.Quantity;
                        decreaseMaximumLimit.Justification = item.Justification;
                        decreaseMaximumLimit.ProductId = item.ProductId;
                        decreaseMaximumLimit.BatchSerialId = item.BatchSerialId;
                        decreaseMaximumLimit.WarehouseId = item.WarehouseId;                                       
                        decreaseMaximumLimit.CreationDate = DateTime.Now;
                        decreaseMaximumLimit.CreationUser = audit.CodeUser;
                    }
                    var batchserial = _consignmentInventoryDetailBatchSerialRepository.GetCosignmentInventoryRemissionDetailBatchSerialToDecreaseMaximum(item.WarehouseId, item.ProductId);
                    int DecreaceQuantity = item.Quantity;
                    foreach (Domain.Entities.ConsignmentInventoryRemissionDetailBatchSerial itemBatch in batchserial)
                    {
                        itemBatch.DecreaseQuantity = DecreaceQuantity <= itemBatch.Quantity ? DecreaceQuantity : itemBatch.Quantity;
                        DecreaceQuantity -= itemBatch.Quantity;
                        _consignmentInventoryDetailBatchSerialRepository.SaveEntity(itemBatch);
                        if (DecreaceQuantity <= 0)
                        {
                            break;
                        }
                    }
                    _decreaseMaximumLimitRepository.SaveEntity(decreaseMaximumLimit);
                }
                _decreaseMaximumLimitRepository.UnitWork.Commit();
                _consignmentInventoryDetailBatchSerialRepository.UnitWork.Commit();
                return new ActionResult { StateResult = true};
            }
            catch (Exception ex)
            {
                return new ActionResult { StateResult = false, Message = Utils.GetInnerExceptionMessageToString(ex) };
            }
        }

        #region IDisposable Support
        private bool disposedValue;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                _decreaseMaximumLimitRepository = null;
                _consignmentInventoryDetailBatchSerialRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
