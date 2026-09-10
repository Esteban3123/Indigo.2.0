//'************************************************************
//' Assembly         : Domain.Inventory.IGroupRepository
//' Author           : Juan Carlos Bermudez Gutierrez
//' Created          : 09/04/2015
//'
//' Copyright        : (c) . All rights reserved.
//'************************************************************

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
    public interface IInventoryServicePackagingUnit
    {

        /// <summary>
        /// Guarda o actualiza una unidad de paquete
        /// </summary>
        /// <param name="measureUnit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.PackagingUnit> SavePackagingUnit(Domain.Entities.PackagingUnit packagingUnit, long idSequense, AuditMessage audit);

        /// <summary>
        /// Elimina una unidad de paquete
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult DeletePackagingUnit(Domain.Entities.PackagingUnit packagingUnit, AuditMessage audit);

        /// <summary>
        /// Obtiene una unidad de medida por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.PackagingUnit> GetPackagingUnitByCode(string code, AuditMessage audit);

        /// <summary>
        /// Obtiene una unidad de medida por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.PackagingUnit> GetPackagingUnitById(int id, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.PackagingUnit> ChangeStatePackagingUnit(string code, bool state, AuditMessage audit);

    }
}
