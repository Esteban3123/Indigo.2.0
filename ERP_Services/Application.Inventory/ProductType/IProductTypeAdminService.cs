using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;

namespace Application.Inventory.ProductType
{
    public interface IProductTypeAdminService : IDisposable
    {
        /// <summary>
        /// Guarda o actualiza un tipo de producto
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.ProductType> SaveProductType(Domain.Entities.ProductType productType, AuditMessage audit, Int64 idSecuence = 0);

        /// <summary>
        /// Elimina un tipo de producto
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult DeleteProductType(Domain.Entities.ProductType productType, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad de tipo de producto
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.ProductType> ChangeStateProductType(string code, bool state, AuditMessage audit);

        /// <summary>
        /// Consulta el tipo producto por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.ProductType> GetProductType(string code, AuditMessage audit);

        /// <summary>
        /// Consulta un tipo producto por id
        /// </summary>
        /// <param name="idProductGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.ProductType> GetProductTypeById(int idProductType, AuditMessage audit);
    }
}
