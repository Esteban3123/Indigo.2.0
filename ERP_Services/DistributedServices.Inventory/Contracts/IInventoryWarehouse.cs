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
using Domain.Entities;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryWarehouse
    {

        /// <summary>
        /// Guarda o actualiza un almacen
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.Warehouse> SaveWarehouse(Domain.Entities.Warehouse warehouse, long idSequense, AuditMessage audit, List<DecreaseMaximumLimit> Decrease = null);

        /// <summary>
        /// Elimina un almacen
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult DeleteWarehouse(Domain.Entities.Warehouse warehouse, AuditMessage audit);

        /// <summary>
        /// Obtiene un almacen por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.Warehouse> GetWarehouse(string code, AuditMessage audit);

        /// <summary>
        /// Obtiene un almacen por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.Warehouse> GetWarehouseById(int id, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.Warehouse> ChangeStateWarehouse(string code, bool state, AuditMessage audit);

        [OperationContract]
        List<string> ListPrefixs();

        /// <summary>
        /// Obtiene la bodega de un proveedor por tipo
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="type"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.Warehouse> GetWarehouseSupplierByType(int supplierId, int type, AuditMessage audit);
    }
}
