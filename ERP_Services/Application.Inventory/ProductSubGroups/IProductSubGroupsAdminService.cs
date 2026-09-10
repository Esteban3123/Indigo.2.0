using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;

namespace Application.Inventory.ProductSubGroups
{
    public interface IProductSubGroupsAdminService : IDisposable
    {
        /// <summary>
        /// Guarda o actualiza un subgrupo
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.ProductSubGroup> SaveProductSubGroup(Domain.Entities.ProductSubGroup productSubGroup, AuditMessage audit, Int64 idSecuence = 0);

        /// <summary>
        /// Elimina un subgrupo
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult DeleteProductSubGroup(Domain.Entities.ProductSubGroup productSubGroup, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad de subgrupo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.ProductSubGroup> ChangeStateSubGroup(string code, bool state, AuditMessage audit);

        /// <summary>
        /// Consulta el grupo por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.ProductSubGroup> GetProductSubGroup(string code, AuditMessage audit);

        /// <summary>
        /// Consulta un grupo por id
        /// </summary>
        /// <param name="idProductGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.ProductSubGroup> GetProductSubGroupById(int idProductGroup, AuditMessage audit);
    }
}
