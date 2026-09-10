//***********************************************************************
// Assembly         : Domain.Inventory
/// Author           : Angi Camila Duran Vargas
/// Created          : 27-07-2023
//
// Copyright        : (c) . All rights reserved.
//***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;

namespace Application.Inventory.ProductInTransit
{
    public interface IProductInTransitAdminService : IDisposable
    {
        /// <summary>
        /// obtiene una remision por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Domain.Entities.ProductInTransit GetProductInTransitByCode(string code, AuditMessage audit);

        /// <summary>
        /// obtiene una remision por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.Entities.ProductInTransit GetProductInTransitById(int id);
        

        /// <summary>
        /// obtiene una remision por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.Entities.PETDefaultSettings GetPETDefaultSettings();

        /// <summary>
        /// guarda una remision
        /// </summary>
        /// <param name="remissionEntrance"></param>
        /// <returns></returns>
        Task<ActionResult<Domain.Entities.ProductInTransit>> SaveProductInTransitAsync(Domain.Entities.ProductInTransit remissionEntrance, AuditMessage audit, long idSequence = 0, InventorySequence sequenceC = null);
        /// <summary>
        /// confirmar una remision
        /// </summary>
        /// <param name="ProductInTransitId"></param>
        /// <param name="audit"></param>
        /// <param name="controlCost"></param>
        /// <returns></returns>
        Task<ActionResult<Domain.Entities.ProductInTransit>> ConfirmProductInTransitAsync(Domain.Entities.ProductInTransit remissionEntrance, AuditMessage audit, Boolean controlCost = false);
        /// <summary>
        /// guardar y confirmar una remisio
        /// </summary>
        /// <param name="remissionEntrance"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <param name="controlCost"></param>
        /// <returns></returns>
        Task<ActionResult<Domain.Entities.ProductInTransit>> SaveAndConfirmbProductInTransitAsync(Domain.Entities.ProductInTransit remissionEntrance, AuditMessage audit, long idSequence = 0, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, InventorySequence sequenceC = null, Boolean controlCost = false);
        /// <summary>
        /// metodo para copiar y pergar informacion o importar un archivo de excel
        /// </summary>
        /// <param name="dataImportFile"></param>
        /// <param name="dataCopyPaste"></param>
        /// <returns></returns>
        ActionResult<List<Domain.Entities.ProductInTransitDetail>> SetCopyPasteOrImportFileProductInTransit(List<ImportFileRow> dataImportFile, List<List<string>> dataCopyPaste );

    }
}
