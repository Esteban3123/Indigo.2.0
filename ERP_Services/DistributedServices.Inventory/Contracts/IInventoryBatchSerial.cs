///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Carlos Mario Arias Rubiano
/// Created          : 08/09/2014
///
/// Copyright        : (c) . All rights reserved.
///***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using System.ServiceModel;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryBatchSerial
    {
        /// <summary>
        /// guarda un lote
        /// </summary>
        /// <param name="BatchSerial"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.BatchSerial> SaveBatchSerial(Domain.Entities.BatchSerial BatchSerial, AuditMessage audit);
        /// <summary>
        /// lista los seriales por el id del producto
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        [OperationContract]
        List<Domain.Entities.BatchSerial> ListBatchSerialByProductId(int ProductId, DateTime? DateBatchSerial, int? WarehouseId, int? RemissionType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AdmissionNumber"></param>
        /// <param name="ProductId"></param>
        /// <param name="DateBatchSerial"></param>
        /// <param name="WarehouseId"></param>
        /// <param name="RemissionType"></param>
        /// <returns></returns>
        [OperationContract]
        List<Domain.Entities.BatchSerial> ListBatchSerialCustodyByProductId(string AdmissionNumber, int ProductId, DateTime? DateBatchSerial, int? WarehouseId, int? RemissionType);

        /// <summary>
        /// lista los seriales por el codigo
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.BatchSerial> BatchSerialByCode(int productId, string code, AuditMessage audit);

        /// <summary>
        /// guarda un lote y actualiza la fecha de vencimineto dejando un historico 
        /// </summary>
        /// <param name="BatchSerial"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        [OperationContract]
        ActionResult<Domain.Entities.BatchSerial> SaveListBatchSerial(List<Domain.Entities.BatchSerial> ListBatchSerial, AuditMessage audit);

        /// <summary>
        /// lista los seriales por el id del producto
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        [OperationContract]
        List<Domain.Entities.BatchSerial> ListBatchSerialByProductIdIncludeExpirationDate(int ProductId);

    }
}
