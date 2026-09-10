///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Carlos Mario Arias Rubiano
/// Created          : 16/09/2014
///
/// Copyright        : (c) . All rights reserved.
///***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using System.ServiceModel;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryPharmacologicalGroup
    {

        /// <summary>
        /// Guarda o actualiza un grupo farmacologico
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.PharmacologicalGroup> SavePharmacologicalGroup(Domain.Entities.PharmacologicalGroup pharmacologicalGroup, long idSequense, AuditMessage audit);

        /// <summary>
        /// Elimina un grupo farmacologico
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult DeletePharmacologicalGroup(Domain.Entities.PharmacologicalGroup pharmacologicalGroup, AuditMessage audit);

        /// <summary>
        /// Obtiene un grupo farmacologico por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.PharmacologicalGroup> GetPharmacologicalGroup(string code, AuditMessage audit);

        /// <summary>
        /// Obtiene un grupo farmacologico por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.PharmacologicalGroup> GetPharmacologicalGroupById(int id, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.PharmacologicalGroup> ChangeStatePharmacologicalGroup(string code, bool state, AuditMessage audit);

    }
}
