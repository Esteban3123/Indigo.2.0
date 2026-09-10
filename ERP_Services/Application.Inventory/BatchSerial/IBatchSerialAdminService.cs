//***********************************************************************
// Assembly         : Domain.Inventory
// Author           : Diego Andrés Roldán Lozano
// Created          : 20-11-2014
//
// Copyright        : (c) . All rights reserved.
//***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;


namespace Application.Inventory.BatchSerial
{
    public interface IBatchSerialAdminService : IDisposable
    {
        /// <summary>
        /// guarda un lote
        /// </summary>
        /// <param name="BatchSerial"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.BatchSerial> SaveBatchSerial(Domain.Entities.BatchSerial BatchSerial,AuditMessage audit);
        /// <summary>
        /// lista los seriales por el id del producto
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
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
        List<Domain.Entities.BatchSerial> ListBatchSerialCustodyByProductId(string AdmissionNumber, int ProductId, DateTime? DateBatchSerial, int? WarehouseId, int? RemissionType);


        /// <summary>
        /// lista los seriales por el id del producto
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        List<Domain.Entities.BatchSerial> GetBatchSerialByProductIdIncludeExpirationDate(int ProductId);

        /// <summary>
        /// lista un lote por codigo de lote
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.BatchSerial> BatchSerialByCode(int productId, string code, AuditMessage audit);

        /// <summary>
        /// guarda un lote y actualiza la fecha de vencimineto dejando un historico 
        /// </summary>
        /// <param name="BatchSerial"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        ActionResult<Domain.Entities.BatchSerial> SaveBatchSerial(List<Domain.Entities.BatchSerial> ListBatchSerial, AuditMessage audit);
        
        
    }
}
