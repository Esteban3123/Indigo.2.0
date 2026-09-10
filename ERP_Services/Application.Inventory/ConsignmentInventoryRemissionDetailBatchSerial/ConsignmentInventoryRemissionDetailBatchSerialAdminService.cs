
///************************************************************
/// Assembly         : Aplication.Inventory
/// Author           : Miguel Angel Fonseca
/// Created          : 2017-12-12
///
/// Copyright        : (c) . All rights reserved.
///************************************************************

#region Imports

using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

#endregion Imports

namespace Application.Inventory.ConsignmentInventoryRemissionDetailBatchSerial
{
    public class ConsignmentInventoryRemissionDetailBatchSerialAdminService : IConsignmentInventoryRemissionDetailBatchSerialAdminService
    {
        #region Fields

        private IConsignmentInventoryRemissionDetailBatchSerialRepository _consignmentInventoryRemissionDetailBatchSerialRepository;

        #endregion Fields

        #region Builder

        public ConsignmentInventoryRemissionDetailBatchSerialAdminService(IConsignmentInventoryRemissionDetailBatchSerialRepository consignmentInventoryRemissionDetailBatchSerialRepository)
        {
            if (consignmentInventoryRemissionDetailBatchSerialRepository == null)
            {
                throw new ArgumentNullException("consignmentInventoryRemissionDetailBatchSerialRepository");
            }
            _consignmentInventoryRemissionDetailBatchSerialRepository = consignmentInventoryRemissionDetailBatchSerialRepository;
        }

        #endregion Builder

        #region Methods

        /// <summary>
        /// Obtiene los productos de la remision de entrada desde los lotes
        /// </summary>
        /// <param name="idSupplier"></param>
        /// <param name="idSupplierDistributionLine"></param>
        /// <returns></returns>
        public List<Domain.Entities.ConsignmentInventoryRemissionDetailBatchSerial> ListConsignmentInventoryRemissionDetailBatchSerialBySupplierAndSupplierDistributionLine(int idSupplier, int idSupplierDistributionLine)
        {
            try
            {
                return _consignmentInventoryRemissionDetailBatchSerialRepository.ListConsignmentInventoryRemissionDetailBatchSerialBySupplierAndSupplierDistributionLine(idSupplier, idSupplierDistributionLine);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.ConsignmentInventoryRemissionDetailBatchSerial>();
            }
        }

        /// <summary>
        /// lista los detalles del detalle de la remision de entrada
        /// </summary>
        /// <param name="ConsignmentInventoryRemissionId"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public List<Domain.Entities.ConsignmentInventoryRemissionDetailBatchSerial> ListConsignmentInventoryRemissionDetailBatchSerialByConsignmentInventoryRemissionId(int ConsignmentInventoryRemissionId)
        {
            try
            {
                return _consignmentInventoryRemissionDetailBatchSerialRepository.ListConsignmentInventoryRemissionDetailBatchSerialByConsignmentInventoryRemissionId(ConsignmentInventoryRemissionId);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.ConsignmentInventoryRemissionDetailBatchSerial>();
            }
        }

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
        public ActionMessageResult UpdateTheQuantityProductUsedInConsignmentInventoryRemission(int entityDetailId, int operativeUnitId, int functionalUnitId, int warehouseId, int productId, int? batchSerialId, MovementTypeRemissionUsed movement, int quantity, Decimal value, int entityId, string entityCode, string entityName, string creationUser, int? originId)
        {
            int movementType = 1;
            if (movement == MovementTypeRemissionUsed.OutPut)
            {
                movementType = 2;
            }

            String xml = createXmlRemission(entityDetailId, operativeUnitId, functionalUnitId, warehouseId, productId, batchSerialId, movementType, quantity, value, originId);
            String resultSP = _consignmentInventoryRemissionDetailBatchSerialRepository.UpdateTheQuantityProductUsedInConsignmentInventoryRemission(xml, entityId, entityCode, entityName, creationUser);

            ActionMessageResult result = new ActionMessageResult();
            result.StateResult =  String.IsNullOrEmpty(resultSP) ? true : false;
            result.Message = resultSP;
            return result;
        }

        private String createXmlRemission(int entityDetailId, int operativeUnitId, int functionalUnitId, int warehouseId, int productId, int? batchSerialId, int movementType, int quantity, Decimal value, int? originId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<Remission>");
            builder.Append("<EntityDetailId>" + entityDetailId.ToString() + "</EntityDetailId>");
            builder.Append("<OperatingUnitId>" + functionalUnitId.ToString() + "</OperatingUnitId>");
            builder.Append("<FunctionalUnitId>" + functionalUnitId.ToString() + "</FunctionalUnitId>");
            builder.Append("<WarehouseId>" + warehouseId.ToString() + "</WarehouseId>");
            builder.Append("<ProductId>" + productId.ToString() + "</ProductId>");
            if (batchSerialId != null)
            {
                builder.Append("<BatchSerialId>" + batchSerialId.ToString() + "</BatchSerialId>");
            }
            builder.Append("<MovementType>" + movementType.ToString() + "</MovementType>");
            builder.Append("<Quantity>" + quantity.ToString() + "</Quantity>");
            builder.Append("<Value>" + value.ToString().Replace(",", ".") + "</Value>");
            if (originId != null)
            {
                builder.Append("<OriginId>" + originId.ToString() + "</OriginId>");
            }
            builder.Append("</Remission>");
            return builder.ToString();
        }

        /// <summary>
        /// lista los detalles de la remision de consignacion sin legalizar
        /// </summary>
        /// <param name="code"></param>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        public List<Domain.Entities.ViewConsignmentInventoryRemissionWithoutLegalize> ListConsignmentInventoryRemissionWithoutLegalize(string code, int warehouseId)
        {
            try
            {
                return _consignmentInventoryRemissionDetailBatchSerialRepository.ListConsignmentInventoryRemissionWithoutLegalize(code, warehouseId);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.ViewConsignmentInventoryRemissionWithoutLegalize>();
            }
        }

        #endregion Methods

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
                if (disposing)
                {

                }
                _consignmentInventoryRemissionDetailBatchSerialRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion 
    }

    #region Enums

    public enum MovementTypeRemissionUsed
    {
        Input = 1,
        OutPut = 2
    }

    #endregion Enums
}