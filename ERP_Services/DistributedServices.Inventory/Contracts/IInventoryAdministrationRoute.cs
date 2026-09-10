///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Carlos Mario Arias Rubiano
/// Created          : 08/09/2014
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
    public interface IInventoryAdministrationRoute
    {

        /// <summary>
        /// Guarda o actualiza una via de administracion
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Base.Entities.ActionResult<Domain.Entities.AdministrationRoute> SaveAdministrationRoute(Domain.Entities.AdministrationRoute AdministrationRoute, long idSequense, AuditMessage audit);

        /// <summary>
        /// Elimina una via de administracion
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Base.Entities.ActionResult DeleteAdministrationRoute(Domain.Entities.AdministrationRoute AdministrationRoute, AuditMessage audit);

        /// <summary>
        /// Obtiene una via de administracion por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.AdministrationRoute> GetAdministrationRoute(string code, AuditMessage audit);

        /// <summary>
        /// Obtiene una via de administracion por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.AdministrationRoute> GetAdministrationRouteById(int id, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.AdministrationRoute> ChangeStateAdministrationRoute(string code, bool state, AuditMessage audit);

    }
}
