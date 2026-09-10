///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Miguel Angel Fonseca
/// Created          : 2017-12-12
///
/// Copyright        : (c) . All rights reserved.
///***********************************************************************

#region Imports

using Domain.Base.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.ServiceModel;

#endregion Imports

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryServiceConsignmentInventoryRemission
    {
        /// <summary>
        /// obtiene una remision por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.ConsignmentInventoryRemission GetConsignmentInventoryRemissionByCode(string code, AuditMessage audit);

        /// <summary>
        /// obtiene una remision por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.ConsignmentInventoryRemission GetConsignmentInventoryRemissionById(int id);

        /// <summary>
        /// guarda una remision
        /// </summary>
        /// <param name="consignmentInventoryRemission"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.ConsignmentInventoryRemission> SaveConsignmentInventoryRemission(Domain.Entities.ConsignmentInventoryRemission consignmentInventoryRemission, long idSequense, AuditMessage audit, Domain.Entities.InventorySequence sequenceC);

        /// <summary>
        /// guardar y confirmar una remision
        /// </summary>
        /// <param name="consignmentInventoryRemission"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.ConsignmentInventoryRemission> SaveAndConfirmbConsignmentInventoryRemission(Domain.Entities.ConsignmentInventoryRemission consignmentInventoryRemission, long idSequense, AuditMessage audit, Domain.Entities.InventorySequence sequenceC, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, Boolean controlCost = false);

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
        [OperationContract]
        ActionResult<string> SaveAndConfirmConsignmentInventoryRemissionAndPharmaceuticalDispensing(
            Domain.Entities.ConsignmentInventoryRemission consignmentInventoryRemission, 
            Domain.Entities.PharmaceuticalDispensing pharmaceuticalDispensing, 
            AuditMessage audit, 
            long idSequense, 
            Domain.Entities.InventorySequence sequenceC, 
            Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert
        );
    }
}