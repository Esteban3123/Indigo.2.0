using DistributedServices.Inventory.Unity;
using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Application.Inventory.ProductInTransit;
using DistributedServices.Inventory.Contracts;


namespace DistributedServices.Inventory
{
    public partial class InventoryService  
    {
        /// <summary>
        /// obtiene una remision por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Entities.ProductInTransit GetProductInTransitByCode(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IProductInTransitAdminService>())
            {
                return service.GetProductInTransitByCode(code, audit);
            }
        }

        /// <summary>
        /// obtiene una remision por id
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Entities.PETDefaultSettings GetPETDefaultSettings()
        {
            using (var service = Container.Current.Resolve<IProductInTransitAdminService>())
            {
                return service.GetPETDefaultSettings();
            }
        }

        /// <summary>
        /// obtiene una remision por id
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Entities.ProductInTransit GetProductInTransitById(int id)
        {
            using (var service = Container.Current.Resolve<IProductInTransitAdminService>())
            {
                return service.GetProductInTransitById(id);
            }
        }
        /// <summary>
        /// guarda unan renmision
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<Domain.Base.Entities.ActionResult<Domain.Entities.ProductInTransit>> SaveProductInTransit(Domain.Entities.ProductInTransit remissionEntrance, AuditMessage audit, long idSequense, Domain.Entities.InventorySequence sequenceC)
        {
            using (var service = Container.Current.Resolve<IProductInTransitAdminService>())
            {
                return await service.SaveProductInTransitAsync(remissionEntrance, audit, idSequense, sequenceC);
            }
        }
        /// <summary>
        /// guarda y confirma la remision
        /// </summary>
        /// <param name="remissionEntrance"></param>
        /// <returns></returns>
        public async Task<Domain.Base.Entities.ActionResult<Domain.Entities.ProductInTransit>> SaveAndConfirmbProductInTransit(Domain.Entities.ProductInTransit remissionEntrance, AuditMessage audit, long idSequense, Domain.Entities.InventorySequence sequenceC, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, Boolean controlCost = false)
        {
            using (var service = Container.Current.Resolve<IProductInTransitAdminService>())
            {
                return await service.SaveAndConfirmbProductInTransitAsync(remissionEntrance, audit, idSequense, action, sequenceC, controlCost);
            }
        }

        public Domain.Base.Entities.ActionResult<List<Domain.Entities.ProductInTransitDetail>> SetCopyPasteOrImportFileProductInTransit(List<Domain.Base.Entities.ImportFileRow> dataImportFile, List<List<string>> dataCopyPaste)
        {
            using (var service = Container.Current.Resolve<IProductInTransitAdminService>())
            {
                return service.SetCopyPasteOrImportFileProductInTransit(dataImportFile, dataCopyPaste);
            }
            //return _productRateDetailAdminService.SetCopyPasteOrImportFileProductRate(dataImportFile, dataCopyPaste);
        }
    }
}
