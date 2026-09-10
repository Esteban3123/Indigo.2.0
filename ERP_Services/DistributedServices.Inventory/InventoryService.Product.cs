using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Application.Inventory.InventoryProduct;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {
        /// <summary>
        /// Saves the inventory product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.InventoryProduct> SaveInventoryProduct(Domain.Entities.InventoryProduct product, long idSequense, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryProductAdminService>())
            {                
                return service.SaveInventoryProduct(product, audit, idSequense);
            }
            //return _inventoryProductAdminService.SaveInventoryProduct(product, audit, idSequense);
        }

        /// <summary>
        /// Deletes the inventory product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult DeleteInventoryProduct(Domain.Entities.InventoryProduct product, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryProductAdminService>())
            {                
                return service.DeleteInventoryProduct(product, audit);
            }
            //return _inventoryProductAdminService.DeleteInventoryProduct(product, audit);
        }

        /// <summary>
        /// Updates the state product.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="state">if set to <c>true</c> [state].</param>
        /// <returns></returns>
        public async Task<Domain.Base.Entities.ActionResult<Domain.Entities.InventoryProduct>> UpdateStateProduct(string code, bool state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryProductAdminService>())
            {                
                return await service.UpdateStateProductAsync(code, state, audit);
            }
        }

        /// <summary>
        /// Loads the list hierarchy product.
        /// </summary>
        public Domain.Base.Entities.ActionResult<List<Domain.Entities.ProductHierarchy>> LoadListHierarchyProduct(Domain.Entities.InventoryProduct product)
        {
            using (var service = Container.Current.Resolve<IInventoryProductAdminService>())
            {
                return service.LoadListHierarchyProduct(product);
            }
            //return _inventoryProductAdminService.LoadListHierarchyProduct(product);
        }

        /// <summary>
        /// Gets the inventory product.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public async Task<Domain.Base.Entities.ActionResult<Domain.Entities.InventoryProduct>> GetInventoryProduct(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryProductAdminService>())
            {                
                return await service.GetInventoryProductAsync(code, audit);
            }
        }

        /// <summary>
        /// Gets the inventory product by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Domain.Entities.InventoryProduct GetInventoryProductById(int id)
        {
            using (var service = Container.Current.Resolve<IInventoryProductAdminService>())
            {
                return service.GetInventoryProductById(id);
            }
            //return _inventoryProductAdminService.GetInventoryProductById(id);
        }

        /// <summary>
        /// Obtiene registro de la tabla inventoryProduct filtrando por una lista de Ids
        /// </summary>
        /// <param name="ListIds"></param>
        /// <returns></returns>
        public List<Domain.Entities.InventoryProduct> GetInventoryProductByIds(List<int> ListIds)
        {
            using (var service = Container.Current.Resolve<IInventoryProductAdminService>())
            {
                return service.GetInventoryProductByIds(ListIds);
            }
        }

        /// <summary>
        /// Gets the inventory product by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Domain.Entities.InventoryProduct GetInventoryProductByIdSimple(int id)
        {
            using (var service = Container.Current.Resolve<IInventoryProductAdminService>())
            {
                return service.GetInventoryProductByIdSimple(id);
            }
            //return _inventoryProductAdminService.GetInventoryProductByIdSimple(id);
        }

        /// <summary>
        /// Gets the inventory product by identifier without Aggregates.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Domain.Entities.InventoryProduct GetInventoryProductByIdWithoutAggregates(int id)
        {
            using (var service = Container.Current.Resolve<IInventoryProductAdminService>())
            {
                return service.GetInventoryProductByIdWithoutAggregates(id);
            }
        }

        /// <summary>
        /// Gets the inventory product by code without Aggregates.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Domain.Entities.InventoryProduct GetInventoryProductByCodeWithoutAggregates(string code)
        {
            using (var service = Container.Current.Resolve<IInventoryProductAdminService>())
            {
                return service.GetInventoryProductByCodeWithoutAggregates(code);
            }
        }

        /// <summary>
        /// Gets the inventory product by codigo.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Domain.Entities.InventoryProduct GetInventoryProductByCodeWithProducGroup(string code)
        {
            using (var service = Container.Current.Resolve<IInventoryProductAdminService>())
            {
                return service.GetInventoryProductByCodeWithProducGroup(code);
            }
            //return _inventoryProductAdminService.GetInventoryProductByCodeWithProducGroup(code);
        }

        /// <summary>
        /// Metodo que realiza el llamado al storeProcedure [SP_ReportSISMEDVentas]
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public System.Data.DataSet GetReportSismedVentas(DateTime dateStart, DateTime dateEnd, SessionValues session)
        {
            using (var service = Container.Current.Resolve<IInventoryProductAdminService>())
            {
                return service.GetReportSismedVentas(dateStart, dateEnd, session);
            }
            //return _inventoryProductAdminService.GetReportSismedVentas(dateStart, dateEnd, session);
        }

        /// <summary>
        /// Metodo que realiza el llamado al storeProcedure [SP_ReportSISMEDCompras]
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public System.Data.DataSet GetReportSismedCompras(DateTime dateStart, DateTime dateEnd, SessionValues session)
        {
            using (var service = Container.Current.Resolve<IInventoryProductAdminService>())
            {
                return service.GetReportSismedCompras(dateStart, dateEnd, session);
            }
            //return _inventoryProductAdminService.GetReportSismedCompras(dateStart, dateEnd, session);
        }

        /// <summary>
        /// Genera el archivo plano de sismed de ventas
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<System.Text.StringBuilder> GenerateFileSismedVentas(DateTime dateStart, DateTime dateEnd, SessionValues session)
        {
            using (var service = Container.Current.Resolve<IInventoryProductAdminService>())
            {
                return service.GenerateFileSismedVentas(dateStart, dateEnd, session);
            }
            //return _inventoryProductAdminService.GenerateFileSismedVentas(dateStart, dateEnd, session);
        }

        /// <summary>
        /// Genera el archivo plano de sismed de compras
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<System.Text.StringBuilder> GenerateFileSismedCompras(DateTime dateStart, DateTime dateEnd, SessionValues session)
        {
            using (var service = Container.Current.Resolve<IInventoryProductAdminService>())
            {
                return service.GenerateFileSismedCompras(dateStart, dateEnd, session);
            }
            //return _inventoryProductAdminService.GenerateFileSismedCompras(dateStart, dateEnd, session);
        }

        /// <summary>
        /// Genera el archivo plano de sismed de compras y ventas Res006
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<System.Text.StringBuilder> GenerateFileSismedRes006(DateTime dateStart, DateTime dateEnd, SessionValues session)
        {
            using (var service = Container.Current.Resolve<IInventoryProductAdminService>())
            {
                return service.GenerateFileSismedRes006(dateStart, dateEnd, session);
            }
            //return _inventoryProductAdminService.GenerateFileSismedCompras(dateStart, dateEnd, session);
        }

        /// <summary>
        /// Genera el archivo plano de SISDIS Circular 015
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<StringBuilder> GenerateSISDIS015(DateTime dateStart, DateTime dateEnd, SessionValues session)
        {
            using (var service = Container.Current.Resolve<IInventoryProductAdminService>())
            {
                return service.GenerateSISDIS015(dateStart, dateEnd, session);
            }
        }

        /// <summary>
        /// Genera el archivo plano de SISDIS Circular 002 
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<System.Text.StringBuilder> GenerateSISDIS002(DateTime dateStart, DateTime dateEnd, SessionValues session,List<int> ProductSupplieId = null)
        {
            using (var service = Container.Current.Resolve<IInventoryProductAdminService>())
            {
                return service.GenerateSISDIS002(dateStart, dateEnd, session, ProductSupplieId);
            }
            //return _inventoryProductAdminService.GenerateFileSismedCompras(dateStart, dateEnd, session);
        }

        /// <summary>
        /// Metodo que realiza el llamado al storeProcedure [SP_ReportSISDIS002]
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public System.Data.DataSet GetReportSISDIS002(DateTime dateStart, DateTime dateEnd, SessionValues session, List<int> ProductSupplieId = null)
        {
            using (var service = Container.Current.Resolve<IInventoryProductAdminService>())
            {
                return service.GetReportSISDIS002(dateStart, dateEnd, session, ProductSupplieId);
            }
        }

        /// <summary>
        /// Metodo que realiza el llamado al storeProcedure [SP_ReportDocumentsDisorganizedInventoryVsAccounting]
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public System.Data.DataSet GetReportDocumentsDisorganizedInventoryVsAccounting(DateTime dateStart, DateTime dateEnd, SessionValues session)
        {
            using (var service = Container.Current.Resolve<IInventoryProductAdminService>())
            {
                return service.GetReportDocumentsDisorganizedInventoryVsAccounting(dateStart, dateEnd, session);
            }
            //return _inventoryProductAdminService.GetReportDocumentsDisorganizedInventoryVsAccounting(dateStart, dateEnd, session);
        }

        /// <summary>
        /// Metodo que realiza el llamado al storeProcedure [SP_ReportAccountingSummaryA]
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public System.Data.DataSet GetReportReportAccountingSummaryA(DateTime dateStart, DateTime dateEnd, SessionValues session)
        {
            using (var service = Container.Current.Resolve<IInventoryProductAdminService>())
            {
                return service.GetReportReportAccountingSummaryA(dateStart, dateEnd, session);
            }
            //return _inventoryProductAdminService.GetReportReportAccountingSummaryA(dateStart, dateEnd, session);
        }

        /// <summary>
        /// Metodo que realiza el llamado al storeProcedure [SP_ReportAccountingSummaryB]
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public System.Data.DataSet GetReportReportAccountingSummaryB(DateTime dateStart, DateTime dateEnd, SessionValues session)
        {
            using (var service = Container.Current.Resolve<IInventoryProductAdminService>())
            {
                return service.GetReportReportAccountingSummaryB(dateStart, dateEnd, session);
            }
            //return _inventoryProductAdminService.GetReportReportAccountingSummaryB(dateStart, dateEnd, session);
        }

        public System.Data.DataSet GetReportDeterioration(int Year, int Month, SessionValues session)
        {
            using (var service = Container.Current.Resolve<IInventoryProductAdminService>())
            {
                return service.GetReportDeterioration(Year, Month, session);
            }
        }

        /// <summary>
        /// Valida el tipo medicamento segun el producto/medicamento
        /// </summary>
        /// <param name="InventoryProduct"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult ValidateMedicationTypeByProduct(Domain.Entities.InventoryProduct InventoryProduct)
        {
            using (var service = Container.Current.Resolve<IInventoryProductAdminService>())
            {
                return service.ValidateMedicationTypeByProduct(InventoryProduct);
            }
        }


    }
}