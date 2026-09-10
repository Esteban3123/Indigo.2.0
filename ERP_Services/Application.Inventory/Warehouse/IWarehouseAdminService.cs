using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;

namespace Application.Inventory.Warehouse
{
    public interface IWarehouseAdminService : IDisposable
    {
        /// <summary>
        /// Guarda o actualiza un almacen
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.Warehouse> SaveWarehouse(Domain.Entities.Warehouse warehouse, AuditMessage audit, List<Domain.Entities.DecreaseMaximumLimit> Decrease = null, Int64 idSecuence = 0);

        /// <summary>
        /// Elimina un almacen
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult DeleteWarehouse(Domain.Entities.Warehouse warehouse, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad de almacen
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.Warehouse> ChangeStateWarehouse(string code, bool state, AuditMessage audit);

        /// <summary>
        /// Consulta el almacen por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.Warehouse> GetWarehouse(string code, AuditMessage audit);

        /// <summary>
        /// Consulta un almacen por id
        /// </summary>
        /// <param name="idProductGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.Warehouse> GetWarehouseById(int idWarehouse, AuditMessage audit);


        List<string> ListPrefixs();

        /// <summary>
        /// Obtiene la bodega de un proveedor por tipo
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="type"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.Warehouse> GetWarehouseSupplierByType(int supplierId, int type, AuditMessage audit);
    }
}
