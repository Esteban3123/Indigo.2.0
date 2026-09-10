///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Diego Andrés Roldán Lozano
/// Created          : 20-11-2014
///
/// Copyright        : (c) . All rights reserved.
///***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using System.ServiceModel;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryProduct
    {
        /// <summary>
        /// Guarda o actualiza un tipo de producto
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryProduct> SaveInventoryProduct(Domain.Entities.InventoryProduct product, long idSequense, AuditMessage audit);

        /// <summary>
        /// Elimina un tipo de producto
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ActionResult DeleteInventoryProduct(Domain.Entities.InventoryProduct product, AuditMessage audit);

        /// <summary>
        /// Updates the state card.
        /// </summary>
        /// <param name="audit">The audit.</param>
        /// <returns></returns>
        [OperationContract]
        Task<ActionResult<Domain.Entities.InventoryProduct>> UpdateStateProduct(string code, bool state, AuditMessage audit);

        /// <summary>
        /// Llena el listado de Jerarquías de un producto
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<List<Domain.Entities.ProductHierarchy>> LoadListHierarchyProduct(Domain.Entities.InventoryProduct product);

        /// <summary>
        /// Obtiene un producto por codigo
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        [OperationContract]
        Task<ActionResult<Domain.Entities.InventoryProduct>> GetInventoryProduct(string code, AuditMessage audit);

        /// <summary>
        /// Obtiene un producto por id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.InventoryProduct GetInventoryProductById(int id);

        /// <summary>
        /// Obtiene registro de la tabla inventoryProduct filtrando por una lista de Ids
        /// </summary>
        /// <param name="ListIds"></param>
        /// <returns></returns>
        [OperationContract]
        List<Domain.Entities.InventoryProduct> GetInventoryProductByIds(List<int> ListIds);

        /// <summary>
        /// Obtiene un producto por id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.InventoryProduct GetInventoryProductByIdSimple(int id);

        /// <summary>
        /// Obtiene un producto por id sin agregados
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.InventoryProduct GetInventoryProductByIdWithoutAggregates(int id);

        /// <summary>
        /// Obtiene un producto por el codigo sin agregados
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.InventoryProduct GetInventoryProductByCodeWithoutAggregates(string code);

        /// <summary>
        /// Obtiene un producto por codigo
        /// </summary>
        /// <param name="code">The identifier.</param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.InventoryProduct GetInventoryProductByCodeWithProducGroup(string code);

        /// <summary>
        /// Metodo que realiza el llamado al storeProcedure [SP_ReportSISMEDVentas]
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        [OperationContract]
        System.Data.DataSet GetReportSismedVentas(DateTime dateStart, DateTime dateEnd, SessionValues session);

        /// <summary>
        /// Metodo que realiza el llamado al storeProcedure [SP_ReportSISMEDCompras]
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        [OperationContract]
        System.Data.DataSet GetReportSismedCompras(DateTime dateStart, DateTime dateEnd, SessionValues session);

        /// <summary>
        /// Genera el archivo plano de sismed de ventas
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<System.Text.StringBuilder> GenerateFileSismedVentas(DateTime dateStart, DateTime dateEnd, SessionValues session);

        /// <summary>
        /// Genera el archivo plano de sismed de compras
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<System.Text.StringBuilder> GenerateFileSismedCompras(DateTime dateStart, DateTime dateEnd, SessionValues session);

        /// <summary>
        /// Genera el archivo plano de sismed de compras y ventas 006
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<System.Text.StringBuilder> GenerateFileSismedRes006(DateTime dateStart, DateTime dateEnd, SessionValues session);

        /// <summary>
        /// Metodo que realiza el llamado al storeProcedure [SP_ReportDocumentsDisorganizedInventoryVsAccounting]
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        [OperationContract]
        System.Data.DataSet GetReportDocumentsDisorganizedInventoryVsAccounting(DateTime dateStart, DateTime dateEnd, SessionValues session);

        /// <summary>
        /// Metodo que realiza el llamado al storeProcedure [SP_ReportAccountingSummaryA]
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        [OperationContract]
        System.Data.DataSet GetReportReportAccountingSummaryA(DateTime dateStart, DateTime dateEnd, SessionValues session);

        /// <summary>
        /// Metodo que realiza el llamado al storeProcedure [SP_ReportAccountingSummaryB]
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        [OperationContract]
        System.Data.DataSet GetReportReportAccountingSummaryB(DateTime dateStart, DateTime dateEnd, SessionValues session);

        /// <summary>
        /// Metodo que realiza el llamado al storeProcedure [Inventory.SP_ReportDeterioration]
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        [OperationContract]
        System.Data.DataSet GetReportDeterioration(int Year, int Month, SessionValues session);

        /// <summary>
        /// Genera el archivo plano SISDIS Circular 002
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<System.Text.StringBuilder> GenerateSISDIS002(DateTime dateStart, DateTime dateEnd, SessionValues session, List<int> ProductSupplieId = null);

        /// <summary>
        /// Genera el archivo plano SISDIS Circular 015
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<StringBuilder> GenerateSISDIS015(DateTime dateStart, DateTime dateEnd, SessionValues session);

        /// <summary>
        /// Valida el tipo medicamento segun el producto/medicamento
        /// </summary>
        /// <param name="InventoryProduct"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Base.Entities.ActionResult ValidateMedicationTypeByProduct(Domain.Entities.InventoryProduct InventoryProduct);

    }
}