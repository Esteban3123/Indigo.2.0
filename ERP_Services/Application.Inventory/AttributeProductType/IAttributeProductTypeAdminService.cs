using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;

namespace Application.Inventory.AttributeProductType
{
    public interface IAttributeProductTypeAdminService : IDisposable
    {
        /// <summary>
        /// Guarda o actualiza un atributo
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.AttributeProductType> SaveAttributeProductType(Domain.Entities.AttributeProductType attributeProductType, AuditMessage audit, Int64 idSecuence = 0);

        /// <summary>
        /// Elimina un atributo
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult DeleteAttributeProductType(Domain.Entities.AttributeProductType attributeProductType, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad de atributo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.AttributeProductType> ChangeStateAttributeProductType(string code, bool state, AuditMessage audit);

        /// <summary>
        /// Consulta el atributo por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.AttributeProductType> GetAttributeProductType(string code, AuditMessage audit);

        /// <summary>
        /// Consulta un atributo por id
        /// </summary>
        /// <param name="idProductGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.AttributeProductType> GetAttributeProductTypeById(int idAttributeProductType, AuditMessage audit);
    }
}
