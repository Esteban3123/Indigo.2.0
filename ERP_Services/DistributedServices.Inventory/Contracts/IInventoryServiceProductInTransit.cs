///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Angi Camila Duran Vargas
/// Created          : 27-07-2023
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
    public interface IInventoryServiceProductInTransit
    {
        /// <summary>
        /// Guarda o actualiza un tipo de producto
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        Task<ActionResult<Domain.Entities.ProductInTransit>> SaveProductInTransit(Domain.Entities.ProductInTransit product, AuditMessage audit,long idSequense, Domain.Entities.InventorySequence sequenceC);

        /// <summary>
        /// Guarda o actualiza un tipo de producto
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        Task<ActionResult<Domain.Entities.ProductInTransit>> SaveAndConfirmbProductInTransit(Domain.Entities.ProductInTransit product, AuditMessage audit, long idSequense, Domain.Entities.InventorySequence sequenceC, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, Boolean controlCost = false);


        /// <summary>
        /// Obtiene un producto por codigo
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.ProductInTransit GetProductInTransitByCode(string code, AuditMessage audit);

        /// <summary>
        /// Obtiene un producto por id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.ProductInTransit GetProductInTransitById(int id);


        /// <summary>
        /// metodo para copiar y pergar informacion o importar un archivo de excel
        /// </summary>
        /// <param name="dataImportFile"></param>
        /// <param name="dataCopyPaste"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<List<Domain.Entities.ProductInTransitDetail>> SetCopyPasteOrImportFileProductInTransit(List<ImportFileRow> dataImportFile, List<List<string>> dataCopyPaste);

        /// <summary>
        /// Obtiene los parametros PET
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.PETDefaultSettings GetPETDefaultSettings();

    }
}