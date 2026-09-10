//'************************************************************
//' Assembly         : Domain.Inventory.IGroupRepository
//' Author           : Carlos Mario Arias Rubiano
//' Created          : 12/09/2014
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
using Domain.Entities;

namespace Application.Inventory.ProductGroup
{
    public interface IProductGroupsAdminService : IDisposable
    {
        /// <summary>
        /// Guarda o actualiza un grupo
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.ProductGroup> SaveProductGroup(Domain.Entities.ProductGroup productGroup, AuditMessage audit, Int64 idSecuence = 0);

        /// <summary>
        /// Elimina un grupo
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult DeleteProductGroup(Domain.Entities.ProductGroup productGroup, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad de grupo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.ProductGroup> ChangeState(string code, bool state, AuditMessage audit);

        /// <summary>
        /// Consulta el grupo por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.ProductGroup> GetProductGroup(string code, AuditMessage audit);

        /// <summary>
        /// Consulta un grupo por id
        /// </summary>
        /// <param name="idProductGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.ProductGroup> GetProductGroupById(int idProductGroup, AuditMessage audit);

    }
}
