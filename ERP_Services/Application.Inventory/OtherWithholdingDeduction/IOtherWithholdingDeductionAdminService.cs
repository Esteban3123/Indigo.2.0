using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;

namespace Application.Inventory.OtherWithholdingDeduction
{
    public interface IOtherWithholdingDeductionAdminService : IDisposable
    {
        /// <summary>
        /// Guarda o actualiza otras deducciones y retenciones
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.OtherWithholdingDeduction> SaveOtherWithholdingDeduction(Domain.Entities.OtherWithholdingDeduction otherWithholdingDeduction, AuditMessage audit, Int64 idSecuence = 0);

        /// <summary>
        /// Elimina otras deducciones y retenciones
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult DeleteOtherWithholdingDeduction(Domain.Entities.OtherWithholdingDeduction otherWithholdingDeduction, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad de otras deducciones y retenciones
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.OtherWithholdingDeduction> ChangeStateOtherWithholdingDeduction(string code, bool state, AuditMessage audit);

        /// <summary>
        /// Consulta otras deducciones y retenciones por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.OtherWithholdingDeduction> GetOtherWithholdingDeduction(string code, AuditMessage audit);

        /// <summary>
        /// Consulta otras deducciones y retenciones por id
        /// </summary>
        /// <param name="idProductGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.OtherWithholdingDeduction> GetOtherWithholdingDeductionById(int idWarehouse, AuditMessage audit);

        /// <summary>
        /// Obtiene un listado de las deducciones y retenciones
        /// </summary>
        /// <returns></returns>
        ActionResult <List <Domain.Entities.OtherWithholdingDeduction>> ListOtherWithholdingDeduction();
        
    }
}
