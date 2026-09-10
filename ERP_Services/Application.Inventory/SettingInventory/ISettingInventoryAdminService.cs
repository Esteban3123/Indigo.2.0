//'************************************************************
//' Assembly         : Domain.Inventory.InventoryContractRepository
//' Author           : Henry Alejandro Vargas Polania
//' Created          : 07/01/2015
//'
//' Copyright        : (c) . All rights reserved.
//'************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;

namespace Application.Inventory.SettingInventory
{
    public interface ISettingInventoryAdminService : IDisposable
    {
        /// <summary>
        /// Obtiene los parametros de inventario
        /// </summary>
        /// <returns></returns>
        Domain.Entities.SettingInventory GetSettingInventory(int OperatingUnitId);

        /// <summary>
        /// Obtiene el registro de parametros de inventario para el formulario por unidad operativa
        /// </summary>
        /// <returns></returns>
        ActionResult<Domain.Entities.SettingInventory> GetInventorySettingsRegister(int OperatingUnitId);

        /// <summary>
        /// Guarda o actualiza el parametro de inventario
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.SettingInventory> SaveSettingInventory(Domain.Entities.SettingInventory settingInventory, AuditMessage audit);

        /// <summary>
        /// Metodo para cerrar el mes de inventario
        /// </summary>
        /// <param name="MonthClosed"></param>
        /// <param name="YearClosed"></param>
        /// <returns></returns>
        ActionResult<InventoryClosedMonth> ClosedMonthInventory(int MonthClosed, int YearClosed, int OperatingUnitId, AuditMessage audit, bool confirm = true);

        /// <summary>
        /// Metodo Para validar el stock del producto
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="operatingUnit"></param>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventoryProduct> ValidateStock(int productId, int operatingUnit, int warehouseId = 0, int quantity = 0, InventoryStaticServices.MovementType movement = 0);

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
        System.Data.DataSet GetReportCloseMonth(Dictionary<string, string> filters, SessionValues session);

        /// <summary>
        /// Metodo para obtener los documentos que se encuentran sin confirmar
        /// </summary>
        /// <param name="MonthClosed"></param>
        /// <param name="YearClosed"></param>
        /// <returns></returns>
        ActionResult<List<SP_VerifiyHasConfirmAllDocuments_Result>> VerifiyHasConfirmAllDocuments(int MonthClosed, int YearClosed);


        /// <summary>
        /// Metodo para obtener el resumen del mes a cerrar
        /// </summary>
        /// <param name="YearClosed"></param>
        /// <param name="MonthClosed"></param>
        /// <returns></returns>
        ActionResult<List<SP_GetMonthlyClosureSummary_Result>> GetMonthlyClosureSummary(int YearClosed,int MonthClosed);


        /// <summary>
        /// Obtiene los datos del cierre mensual 
        /// </summary>
        /// <returns></returns>
        Domain.Entities.ClosedMonthInventoryHeader GetMonthlyClosed(int yearClosed, int monthClosed);



    }
}
