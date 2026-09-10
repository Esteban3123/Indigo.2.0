//'************************************************************
//' Assembly         : Application.Inventory
//' Author           : Carlos Ernesto Cordoba
//' Created          : 08/02/2016
//'
//' Copyright        : (c) . All rights reserved.
//'************************************************************

using Domain.Base.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Inventory.InventoryMassiveConfirm
{
    public interface IInventoryMassiveConfirmAdminService : IDisposable
    {
        /// <summary>
        /// Metodo para confirmar un documentos del módulo de inventario
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
         Task<ActionResult<Tuple<string, int>>> ConfirmInventoryDocument(int processId, string code, AuditMessage audit);

        /// <summary>
        /// metodo para confirmar masivamente los documentos de inventarios
        /// </summary>
        /// <param name="listDocuments"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<List<Tuple<string, int>>> ConfirmInventoryDocuments(int processId, List<string> listDocuments, AuditMessage audit);
    }
}