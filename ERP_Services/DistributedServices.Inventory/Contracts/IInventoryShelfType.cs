///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Judy Andrea Díaz Reyes
/// Created          : 28/05/2019
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
    public interface IInventoryShelfType
    {

		/// <summary>
		/// Obtiene todos los tipos de estante
		/// </summary>
		/// <returns></returns>
		[OperationContract]
		List<Domain.Entities.ShelfType> ListAllShelfType(AuditMessage audit);

		/// <summary>
		/// Guarda o actualiza un tipo de estante
		/// </summary>
		/// <param name="shelfType"></param>
		/// <returns></returns>
		[OperationContract]
        ActionResult<Domain.Entities.ShelfType> SaveShelfType(Domain.Entities.ShelfType shelfType, long idSequense, AuditMessage audit);

		/// <summary>
		/// Elimina un tipo de estante
		/// </summary>
		/// <param name="shelfType"></param>
		/// <returns></returns>
		[OperationContract]
        ActionResult DeleteShelfType(Domain.Entities.ShelfType shelfType, AuditMessage audit);

        /// <summary>
        /// Obtiene un tipo de estante por código
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.ShelfType> GetShelfType(string code, AuditMessage audit);

        /// <summary>
        /// Obtiene un tipo de estane por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.ShelfType> GetShelfTypeById(int id, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.ShelfType> UpdateStateShelfType(string code, bool state, AuditMessage audit);

    }
}
