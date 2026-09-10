//***********************************************************************
// Assembly         : Domain.Inventory
// Author           : Diego Andrés Roldán Lozano
// Created          : 20-11-2014
//
// Copyright        : (c) . All rights reserved.
//***********************************************************************

using Domain.Base.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Inventory.InventoryProduct
{
    public interface IInventoryProductAdminService : IDisposable
    {
        /// <summary>
        /// Guarda o actualiza un tipo de producto
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        [Obsolete("Este método es obsoleto. Utilice SaveInventoryProductAsync en su lugar.")]
        ActionResult<Domain.Entities.InventoryProduct> SaveInventoryProduct(Domain.Entities.InventoryProduct product, AuditMessage audit, Int64 idSecuence = 0);

        /// <summary>
        /// Guarda o actualiza un tipo de producto de forma asíncrona
        /// </summary>
        /// <param name="product"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        Task<ActionResult<Domain.Entities.InventoryProduct>> SaveInventoryProductAsync(Domain.Entities.InventoryProduct product, AuditMessage audit, Int64 idSecuence = 0);

        /// <summary>
        /// Elimina un tipo de producto
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult DeleteInventoryProduct(Domain.Entities.InventoryProduct product, AuditMessage audit);

        /// <summary>
        /// Updates the state card.
        /// </summary>
        /// <param name="audit">The audit.</param>
        /// <returns></returns>
        Task<ActionResult<Domain.Entities.InventoryProduct>> UpdateStateProductAsync(string code, bool state, AuditMessage audit);

        /// <summary>
        /// Llena el listado de Jerarquías de un producto
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        ActionResult<List<Domain.Entities.ProductHierarchy>> LoadListHierarchyProduct(Domain.Entities.InventoryProduct product);

        /// <summary>
        /// Obtiene un producto por codigo
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        [Obsolete("Esta función está marcada como obsoleta. Utilice GetInventoryProductAsync en su lugar para mejor rendimiento y funcionalidad asíncrona.")]
        ActionResult<Domain.Entities.InventoryProduct> GetInventoryProduct(string code, AuditMessage audit);

        /// <summary>
        /// Obtiene un producto por codigo
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        Task<ActionResult<Domain.Entities.InventoryProduct>> GetInventoryProductAsync(string code, AuditMessage audit);

        /// <summary>
        /// Obtiene un producto por id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Domain.Entities.InventoryProduct GetInventoryProductById(int id);

        /// <summary>
        /// Obtiene registro de la tabla inventoryProduct filtrando por una lista de Ids
        /// </summary>
        /// <param name="ListIds"></param>
        /// <returns></returns>
        List<Domain.Entities.InventoryProduct> GetInventoryProductByIds(List<int> ListIds);

        /// <summary>
        /// Obtiene un producto por id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Domain.Entities.InventoryProduct GetInventoryProductByIdSimple(int id);

        /// <summary>
        /// Obtiene un producto por id sin agregados
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Domain.Entities.InventoryProduct GetInventoryProductByIdWithoutAggregates(int id);

        /// <summary>
        /// Obtiene un producto por codigo sin agregados
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Domain.Entities.InventoryProduct GetInventoryProductByCodeWithoutAggregates(string code);

        /// <summary>
        /// Obtiene un producto por codigo con su subgrupo y grupo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Domain.Entities.InventoryProduct GetInventoryProductByCodeWithProducGroup(string code);

        /// <summary>
        /// Metodo que realiza el llamado al storeProcedure [SP_ReportSISMEDVentas]
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        System.Data.DataSet GetReportSismedVentas(DateTime dateStart, DateTime dateEnd, SessionValues session);

        /// <summary>
        /// Metodo que realiza el llamado al storeProcedure [SP_ReportSISMEDCompras]
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        System.Data.DataSet GetReportSismedCompras(DateTime dateStart, DateTime dateEnd, SessionValues session);

        /// <summary>
        /// Genera el archivo plano de sismed de ventas
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        ActionResult<System.Text.StringBuilder> GenerateFileSismedVentas(DateTime dateStart, DateTime dateEnd, SessionValues session);

        /// <summary>
        /// Genera el archivo plano de sismed de compras
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        ActionResult<System.Text.StringBuilder> GenerateFileSismedCompras(DateTime dateStart, DateTime dateEnd, SessionValues session);

        /// <summary>
        /// Genera el archivo plano de sismed de compras y ventas res 006
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        ActionResult<System.Text.StringBuilder> GenerateFileSismedRes006(DateTime dateStart, DateTime dateEnd, SessionValues session);

        /// <summary>
        /// Metodo que realiza el llamado al storeProcedure [SP_ReportDocumentsDisorganizedInventoryVsAccounting]
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        System.Data.DataSet GetReportDocumentsDisorganizedInventoryVsAccounting(DateTime dateStart, DateTime dateEnd, SessionValues session);

        /// <summary>
        /// Metodo que realiza el llamado al storeProcedure [SP_ReportAccountingSummaryA]
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        System.Data.DataSet GetReportReportAccountingSummaryA(DateTime dateStart, DateTime dateEnd, SessionValues session);

        /// <summary>
        /// Metodo que realiza el llamado al storeProcedure [SP_ReportAccountingSummaryB]
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        System.Data.DataSet GetReportReportAccountingSummaryB(DateTime dateStart, DateTime dateEnd, SessionValues session);

        /// <summary>
        /// Metodo que realiza el llamado al storeProcedure [Inventory.SP_ReportDeterioration]
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        System.Data.DataSet GetReportDeterioration(int Year, int Month, SessionValues session);

        /// <summary>
        /// Metodo que realiza el llamado al storeProcedure [SP_ReportSISDIS002]
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        System.Data.DataSet GetReportSISDIS002(DateTime dateStart, DateTime dateEnd, SessionValues session, List<int> ProductSupplieId = null);

        /// <summary>
        /// Metodo que realiza el archivo plano
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        ActionResult<System.Text.StringBuilder> GenerateSISDIS002(DateTime dateStart, DateTime dateEnd, SessionValues session, List<int> ProductSupplieId = null);

        /// <summary>
        /// Metodo que realiza el archivo plano SISDIS Circular 015
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        ActionResult<StringBuilder> GenerateSISDIS015(DateTime dateStart, DateTime dateEnd, SessionValues session);

        /// <summary>
        /// Valida el tipo medicamento segun el producto/medicamento
        /// </summary>
        /// <param name="InventoryProduct"></param>
        /// <returns></returns>
        ActionResult ValidateMedicationTypeByProduct(Domain.Entities.InventoryProduct InventoryProduct);

    }
}
