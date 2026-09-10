using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;

namespace Application.Inventory.MeasureUnit
{
    public interface IMeasureUnitAdminService : IDisposable
    {
        /// <summary>
        /// Guarda o actualiza una unidad de medida
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventoryMeasurementUnit> SaveMeasureUnit(Domain.Entities.InventoryMeasurementUnit measureUnit, AuditMessage audit, Int64 idSecuence = 0);

        /// <summary>
        /// Elimina una unidad de medida
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult DeleteMeasureUnit(Domain.Entities.InventoryMeasurementUnit measureUnit, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad de subgrupo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventoryMeasurementUnit> ChangeStateMeasureUnit(string code, bool state, AuditMessage audit);

        /// <summary>
        /// Consulta la unidad de medida por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventoryMeasurementUnit> GetMeasureUnit(string code, AuditMessage audit);

        /// <summary>
        /// Consulta la unidad de medida por id
        /// </summary>
        /// <param name="idProductGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventoryMeasurementUnit> GetMeasureUnitById(int idProductGroup, AuditMessage audit);
    }
}
