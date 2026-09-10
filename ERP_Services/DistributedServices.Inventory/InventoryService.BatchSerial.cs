using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using DistributedServices.Inventory.Unity;
using Application.Inventory.BatchSerial;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {
        /// <summary>
        /// Saves the batch serial.
        /// </summary>
        /// <param name="BatchSerial">The batch serial.</param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.BatchSerial> SaveBatchSerial(Domain.Entities.BatchSerial BatchSerial, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IBatchSerialAdminService>())
            {               
                return service.SaveBatchSerial(BatchSerial, audit);
            }
            //return _batchSerialAdminService.SaveBatchSerial(BatchSerial, audit);
        }

        /// <summary>
        /// Lists the batch serial by product identifier.
        /// </summary>
        /// <param name="ProductId">The product identifier.</param>
        /// <returns></returns>
        public List<Domain.Entities.BatchSerial> ListBatchSerialByProductId(int ProductId, DateTime? DateBatchSerial, int? WarehouseId, int? RemissionType)
        {
            using (var service = Container.Current.Resolve<IBatchSerialAdminService>())
            {
                return service.ListBatchSerialByProductId(ProductId, DateBatchSerial, WarehouseId, RemissionType);
            }
            //return _batchSerialAdminService.ListBatchSerialByProductId(ProductId, DateBatchSerial);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AdmissionNumber"></param>
        /// <param name="ProductId"></param>
        /// <param name="DateBatchSerial"></param>
        /// <param name="WarehouseId"></param>
        /// <param name="RemissionType"></param>
        /// <returns></returns>
        public List<Domain.Entities.BatchSerial> ListBatchSerialCustodyByProductId(String AdmissionNumber, int ProductId, DateTime? DateBatchSerial, int? WarehouseId, int? RemissionType)
        {
            using (var service = Container.Current.Resolve<IBatchSerialAdminService>())
            {
                return service.ListBatchSerialCustodyByProductId(AdmissionNumber,  ProductId,  DateBatchSerial,  WarehouseId,  RemissionType);
            }
            //return _batchSerialAdminService.ListBatchSerialByProductId(ProductId, DateBatchSerial);
        }


        /// <summary>
        /// consultar el lote por codigo
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.BatchSerial> BatchSerialByCode(int productId, string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IBatchSerialAdminService>())
            {               
                return service.BatchSerialByCode(productId, code, audit);
            }
            //return _batchSerialAdminService.BatchSerialByCode(code, audit);
        }

        /// <summary>
        /// Saves the batch serial.
        /// </summary>
        /// <param name="BatchSerial">The batch serial.</param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.BatchSerial> SaveListBatchSerial(List<Domain.Entities.BatchSerial> ListBatchSerial, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IBatchSerialAdminService>())
            {                
                return service.SaveBatchSerial(ListBatchSerial, audit);
            }
            //return _batchSerialAdminService.SaveBatchSerial(ListBatchSerial, audit);
        }

        /// <summary>
        /// Lists the batch serial by product identifier.
        /// </summary>
        /// <param name="ProductId">The product identifier.</param>
        /// <returns></returns>
        public List<Domain.Entities.BatchSerial> ListBatchSerialByProductIdIncludeExpirationDate(int ProductId)
        {
            using (var service = Container.Current.Resolve<IBatchSerialAdminService>())
            {
                return service.GetBatchSerialByProductIdIncludeExpirationDate(ProductId);
            }
            //return _batchSerialAdminService.GetBatchSerialByProductIdIncludeExpirationDate(ProductId);
        }
    }
}
