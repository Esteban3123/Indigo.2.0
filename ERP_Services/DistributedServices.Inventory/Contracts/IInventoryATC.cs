///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Diego Andrés Roldán Lozano
/// Created          : 20-11-2014
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

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryATC
    {
        /// <summary>
        /// Guarda un ATC
        /// </summary>
        [OperationContract]
        ActionResult<Domain.Entities.ATC> SaveATC(Domain.Entities.ATC atc, long idSequense, AuditMessage audit);

        /// <summary>
        /// Elimina un ATC
        /// </summary>
        [OperationContract]
        ActionResult DeleteATC(Domain.Entities.ATC atc, AuditMessage audit);

        /// <summary>
        /// Actualiza el estado del atc
        /// </summary>
        [OperationContract]
        ActionResult<Domain.Entities.ATC> UpdateStateATC(string code, bool state, AuditMessage audit);

        /// <summary>
        /// Obtiene un atc por codigo
        /// </summary>
        [OperationContract]
        ActionResult<Domain.Entities.ATC> GetATC(string code, AuditMessage audit);

        /// <summary>
        /// Obtiene un atc por id
        /// </summary>
        [OperationContract]
        Domain.Entities.ATC GetATCById(int id);
    }
}
