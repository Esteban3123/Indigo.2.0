///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Henry Alejandro Vargas Polania
/// Created          : 15-01-2015
///
/// Copyright        : (c) . All rights reserved.
///***********************************************************************

using System.ServiceModel;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;
using System.Collections.Generic;
using System;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventorySettingInventory
    {
        [OperationContract]
        Domain.Entities.SettingInventory GetSettingInventory(int OperatingUnitId);

        /// <summary>
        /// Obtiene el registro de parametros de inventario para el formulario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.SettingInventory> GetInventorySettingsRegister(int OperatingUnitId);

        /// <summary>
        /// Guarda o actualiza el registro de parametro de inventario
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.SettingInventory> SaveSettingInventory(Domain.Entities.SettingInventory settingInventory, AuditMessage audit);

        /// <summary>
        /// Metodo para cerrar el mes de inventario
        /// </summary>
        /// <param name="MonthClosed"></param>
        /// <param name="YearClosed"></param>
        /// <param name="OperatingUnitId"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryClosedMonth> ClosedMonthInventory(int MonthClosed, int YearClosed, int OperatingUnitId, AuditMessage audit, bool confirm = true);

        /// <summary>
        /// Metodo para validar el stock del producto
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="operatingUnitId"></param>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryProduct> ValidateStock(int productId, int operatingUnitId, int warehouseId = 0, int quantity = 0, InventoryStaticServices.MovementType movement = 0);

        /// <summary>
        /// Metodo para obtener los datos del reporte de cierre mensual de inventarios
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <param name="WarehouseInitial"></param>
        /// <param name="WarehouseEnd"></param>
        /// <param name="GroupInitial"></param>
        /// <param name="GroupEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        [OperationContract]
        System.Data.DataSet GetReportCloseMonth(Dictionary<string, string> filters, SessionValues session);

        /// <summary>
        /// Metodo para obtener los documentos que se encuentran sin confirmar
        /// </summary>
        /// <param name="MonthClosed"></param>
        /// <param name="YearClosed"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<List<SP_VerifiyHasConfirmAllDocuments_Result>> VerifiyHasConfirmAllDocuments(int MonthClosed, int YearClosed);


        /// <summary>
        /// Metodo que obtiene el resumen del mes a cerrar
        /// </summary>
        /// <param name="YearClosed"></param>
        /// <param name="MonthClosed"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<List<SP_GetMonthlyClosureSummary_Result>> GetMonthlyClosureSummary( int YearClosed, int MonthClosed);
    }
}
