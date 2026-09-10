//***********************************************************************
// Assembly         : Aplication.Inventory
// Author           : Miguel Angel Fonseca
// Created          : 2017-12-12
//
// Copyright        : (c) . All rights reserved.
//***********************************************************************

#region Imports

using System;
using Domain.Base.Entities;
using System.Collections.Generic;

#endregion Imports

namespace Application.Inventory.ConsignmentInventoryRemissionDetailBatchSerial
{
    public interface IConsignmentInventoryRemissionDetailBatchSerialAdminService : IDisposable
    {
        /// <summary>
        /// Obtiene los detalles de la remision desde el Lote
        /// </summary>
        /// <param name="idSupplier"></param>
        /// <param name="idSupplierDistributionLine"></param>
        /// <returns></returns>
        List<Domain.Entities.ConsignmentInventoryRemissionDetailBatchSerial> ListConsignmentInventoryRemissionDetailBatchSerialBySupplierAndSupplierDistributionLine(int idSupplier, int idSupplierDistributionLine);

        /// <summary>
        /// lista los detalles del detalle de la remision de entrada
        /// </summary>
        /// <param name="ConsignmentInventoryRemissionId"></param>
        /// <returns></returns>
        List<Domain.Entities.ConsignmentInventoryRemissionDetailBatchSerial> ListConsignmentInventoryRemissionDetailBatchSerialByConsignmentInventoryRemissionId(int ConsignmentInventoryRemissionId);

        /// <summary>
        /// Funcion para actualizar la cantidades usadas de la remisión de inventario en consignación
        /// </summary>
        /// <param name="entityDetailId">Id del detalle del registro principal</param>
        /// <param name="operativeUnitId">Id de la unidad operativa</param>
        /// <param name="functionalUnitId">Id de la unidad funcional</param>
        /// <param name="warehouseId">Id del almacen</param>
        /// <param name="product">Producto que va hacer el movimiento</param>
        /// <param name="batchSerialId">Id del lote o serial</param>
        /// <param name="movement">Tipo de movimiento, Entrada o Salida</param>
        /// <param name="quantity">Cantidad del producto</param>
        /// <param name="value">Costo promedio actual del producto</param>
        /// <param name="entityId">Id de la entidad que esta generando el movimiento</param>
        /// <param name="entityCode">Codigo de la entidad que esta generando el movimiento</param>
        /// <param name="entityName">Nombre de la entidad que esta generando el movimiento (Es el nombre de la clase o tabla)</param>
        /// <param name="creationUser">Codigo del usuario que esta generando el movimiento</param>
        /// <param name="originId">Id del registro que se pretende devolver</param>
        /// <returns></returns>
        ActionMessageResult UpdateTheQuantityProductUsedInConsignmentInventoryRemission(int entityDetailId, int operativeUnitId, int functionalUnitId, int warehouseId, int productId, int? batchSerialId, MovementTypeRemissionUsed movement, int quantity, Decimal value, int entityId, string entityCode, string entityName, string creationUser, int? originId);

        /// <summary>
        /// lista los detalles de la remision de consignacion sin legalizar
        /// </summary>
        /// <param name="code"></param>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        List<Domain.Entities.ViewConsignmentInventoryRemissionWithoutLegalize> ListConsignmentInventoryRemissionWithoutLegalize(string code, int warehouseId);
    }
}