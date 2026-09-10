using Domain.Base.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Inventory.MedicationType
{
    public interface IMedicationTypeAdminService : IDisposable
    {

        #region Methods

        /// <summary>
        ///  Obtiene el tipo de medicamento por su código.
        /// </summary>
        /// <param name="code">El código del tipo de medicamento a obtener.</param>
        /// <param name="audit">Información de auditoría para el registro de la operación.</param>
        /// <returns></returns>
        ActionResult<Domain.Entities.MedicationType> GetMedicationTypeByCode(String code, AuditMessage audit);


        /// <summary>
        ///  Guarda un nuevo tipo de medicamento.
        /// </summary>
        /// <param name="medicationType">El tipo de medicamento a guardar.</param>
        /// <param name="audit">Información de auditoría para el registro de la operación.</param>
        /// <param name="idSecuence">El identificador de secuencia</param>
        /// <returns></returns>
        ActionResult<Domain.Entities.MedicationType> SaveMedicationType(Domain.Entities.MedicationType medicationType, AuditMessage audit,  Int64 idSecuence = 0);

        /// <summary>
        ///  Elimina un tipo de medicamento.
        /// </summary>
        /// <param name="medicationType">El tipo de medicamento a eliminar.</param>
        /// <param name="audit">Información de auditoría para el registro de la operación.</param>
        /// <returns></returns>
        ActionResult DeleteMedicationType(Domain.Entities.MedicationType measureUnit, AuditMessage audit);


        /// <summary>
        /// Cambia el estado de un tipo de medicamento.
        /// </summary>
        /// <param name="medicationType">El tipo de medicamento cuyo estado se va a modificar.</param>
        /// <param name="state">El nuevo estado del tipo de medicamento (activo o inactivo).</param>
        /// <param name="audit">Información de auditoría para el registro de la operación.</param>
        /// <returns></returns>
        ActionResult<Domain.Entities.MedicationType> ChangeStateMedicationType(Domain.Entities.MedicationType medicationType, bool state, AuditMessage audit);


        #endregion

    }
}
