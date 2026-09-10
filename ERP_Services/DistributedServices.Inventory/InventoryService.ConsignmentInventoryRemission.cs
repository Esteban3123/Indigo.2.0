#region Imports

using Application.Inventory.ConsignmentInventoryRemission;
using DistributedServices.Inventory.Unity;
using Infrastructure.CrossCutting.Base;
using System;
using System.ServiceModel;
using Microsoft.Practices.Unity;

#endregion Imports

namespace DistributedServices.Inventory
{
    partial class InventoryService
    {
        /// <summary>
        /// obtiene una remision por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Entities.ConsignmentInventoryRemission GetConsignmentInventoryRemissionByCode(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IConsignmentInventoryRemissionAdminService>())
            {              
                return service.GetConsignmentInventoryRemissionByCode(code, audit);
            }
            //return _consignmentInventoryRemissionAdminService.GetConsignmentInventoryRemissionByCode(code, audit);
        }

        /// <summary>
        /// obtiene una remision por id
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Entities.ConsignmentInventoryRemission GetConsignmentInventoryRemissionById(int id)
        {
            using (var service = Container.Current.Resolve<IConsignmentInventoryRemissionAdminService>())
            {
                return service.GetConsignmentInventoryRemissionById(id);
            }
            //return _consignmentInventoryRemissionAdminService.GetConsignmentInventoryRemissionById(id);
        }

        /// <summary>
        /// guarda unan renmision
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.ConsignmentInventoryRemission> SaveConsignmentInventoryRemission(Domain.Entities.ConsignmentInventoryRemission consignmentInventoryRemission, long idSequense, AuditMessage audit, Domain.Entities.InventorySequence sequenceC)
        {
            using (var service = Container.Current.Resolve<IConsignmentInventoryRemissionAdminService>())
            {                
                return service.SaveConsignmentInventoryRemission(consignmentInventoryRemission, audit, idSequense, sequenceC);
            }
            //return _consignmentInventoryRemissionAdminService.SaveConsignmentInventoryRemission(consignmentInventoryRemission, audit, idSequense, sequenceC);
        }

        /// <summary>
        /// guarda y confirma la remision
        /// </summary>
        /// <param name="consignmentInventoryRemission"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.ConsignmentInventoryRemission> SaveAndConfirmbConsignmentInventoryRemission(Domain.Entities.ConsignmentInventoryRemission consignmentInventoryRemission, long idSequense, AuditMessage audit, Domain.Entities.InventorySequence sequenceC, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, Boolean controlCost = false)
        {
            using (var service = Container.Current.Resolve<IConsignmentInventoryRemissionAdminService>())
            {               
                return service.SaveAndConfirmbConsignmentInventoryRemission(consignmentInventoryRemission, audit, idSequense, action, sequenceC, controlCost);
            }
            //return _consignmentInventoryRemissionAdminService.SaveAndConfirmbConsignmentInventoryRemission(consignmentInventoryRemission, audit, idSequense, action, sequenceC, controlCost);
        }

        /// <summary>
        /// guardar y confirmar una remision al igual que su dispensación, esto para los tipos de remisión de gastos directos
        /// </summary>
        /// <param name="consignmentInventoryRemission"></param>
        /// <param name="pharmaceuticalDispensing"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <param name="sequenceC"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<string> SaveAndConfirmConsignmentInventoryRemissionAndPharmaceuticalDispensing(
            Domain.Entities.ConsignmentInventoryRemission consignmentInventoryRemission,
            Domain.Entities.PharmaceuticalDispensing pharmaceuticalDispensing,
            AuditMessage audit,
            long idSequense,
            Domain.Entities.InventorySequence sequenceC,
            Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert
        )
        {
            using (var service = Container.Current.Resolve<IConsignmentInventoryRemissionAdminService>())
            {
                return service.SaveAndConfirmConsignmentInventoryRemissionAndPharmaceuticalDispensing(consignmentInventoryRemission, pharmaceuticalDispensing, audit, idSequense, sequenceC, action);
            }
        }
    }
}