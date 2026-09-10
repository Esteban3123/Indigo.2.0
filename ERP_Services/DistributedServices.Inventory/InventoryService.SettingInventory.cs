using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using DistributedServices.Inventory.Contracts;
using Domain.Entities;
using Application.Inventory.SettingInventory;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;
using Domain.Base.Entities;

namespace DistributedServices.Inventory
{
    public partial class InventoryService : IInventorySettingInventory
    {
        public Domain.Entities.SettingInventory GetSettingInventory(int OperatingUnitId)
        {
            using (var service = Container.Current.Resolve<ISettingInventoryAdminService>())
            {
                return service.GetSettingInventory(OperatingUnitId);
            }
            //return _inventorySettingInventory.GetSettingInventory(OperatingUnitId);
        }

        /// <summary>
        /// Obtiene el registro de parametros de inventario para el formulario
        /// </summary>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.SettingInventory> GetInventorySettingsRegister(int OperatingUnitId)
        {
            using (var service = Container.Current.Resolve<ISettingInventoryAdminService>())
            {
                return service.GetInventorySettingsRegister(OperatingUnitId);
            }
            //return _inventorySettingInventory.GetInventorySettingsRegister(OperatingUnitId); 
        }

        /// <summary>
        /// Guarda o actualiza el registro de parametro de inventario
        /// </summary>
        /// <param name="settingInventory"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.SettingInventory> SaveSettingInventory(Domain.Entities.SettingInventory settingInventory, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<ISettingInventoryAdminService>())
            {                
                return service.SaveSettingInventory(settingInventory, audit);
            }
            //return _inventorySettingInventory.SaveSettingInventory(settingInventory, audit);
        }

        /// <summary>
        /// Metodo para cerrar el mes de inventario
        /// </summary>
        /// <param name="MonthClosed"></param>
        /// <param name="YearClosed"></param>
        /// <param name="OperatingUnitId"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.InventoryClosedMonth> ClosedMonthInventory(int MonthClosed, int YearClosed, int OperatingUnitId, AuditMessage audit, bool confirm = true)
        {
            using (var service = Container.Current.Resolve<ISettingInventoryAdminService>())
            {              
                return service.ClosedMonthInventory(MonthClosed, YearClosed, OperatingUnitId, audit, confirm);
            }
            //return _inventorySettingInventory.ClosedMonthInventory(MonthClosed, YearClosed, OperatingUnitId, audit, confirm);
        }

        public Domain.Base.Entities.ActionResult<Domain.Entities.InventoryProduct> ValidateStock(int productId, int operatingUnitId, int warehouseId = 0, int quantity = 0, InventoryStaticServices.MovementType movement = 0)
        {
            using (var service = Container.Current.Resolve<ISettingInventoryAdminService>())
            {
                return service.ValidateStock(productId, operatingUnitId, warehouseId, quantity, movement);
            }
            //return _inventorySettingInventory.ValidateStock(productId, operatingUnitId, warehouseId, quantity,movement);
        }

        public System.Data.DataSet GetReportCloseMonth(Dictionary<string, string> filters, SessionValues session)
        {
            using (var service = Container.Current.Resolve<ISettingInventoryAdminService>())
            {
                return service.GetReportCloseMonth(filters, session);
            }
        }

        /// <summary>
        /// Metodo para obtener los documentos que se encuentran sin confirmar
        /// </summary>
        /// <param name="MonthClosed"></param>
        /// <param name="YearClosed"></param>
        /// <returns></returns>
        public ActionResult<List<SP_VerifiyHasConfirmAllDocuments_Result>> VerifiyHasConfirmAllDocuments(int MonthClosed, int YearClosed)
        {
            using (var service = Container.Current.Resolve<ISettingInventoryAdminService>())
            {
                return service.VerifiyHasConfirmAllDocuments(MonthClosed, YearClosed);
            }
        }

        /// <summary>
        /// Metodo para obtener el resumen del mes a cerrar
        /// </summary>
        /// <param name="YearClosed"></param>
        /// <param name="MonthClosed"></param>
        /// <returns></returns>
        public ActionResult<List<SP_GetMonthlyClosureSummary_Result>> GetMonthlyClosureSummary(int YearClosed, int MonthClosed)
        {
            using (var service = Container.Current.Resolve<ISettingInventoryAdminService>())
            {
                return service.GetMonthlyClosureSummary(YearClosed, MonthClosed);
            }
        }


        /// <summary>
        /// Metodo para obtener informacion de cierre mensual
        /// </summary>
        /// <param name="yearClosed"></param>
        /// <param name="monthClosed"></param>
        /// <returns></returns>
        public Domain.Entities.ClosedMonthInventoryHeader GetMonthlyClosed(int yearClosed, int monthClosed)
        {
            using (var service = Container.Current.Resolve<ISettingInventoryAdminService>())
            {
                return service.GetMonthlyClosed(yearClosed,  monthClosed);
            }
        }

    }
}
