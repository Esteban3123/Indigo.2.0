//'************************************************************
//' Assembly         : Application.Inventory.AntineoplasicoMedication
//' Author           : Cesar Augusto Collazos
//' Created          : 29/02/2024
//'
//' Copyright        : (c) . All rights reserved.
//'************************************************************

using Domain.Base.Entities;
using Infrastructure.CrossCutting.Base;
using System;

namespace Application.Inventory.PharmaceuticalFormGrouping
{
    public interface IPharmaceuticalFormGroupingAdminService : IDisposable
    {
        /// <summary>
        /// Guarda o actualiza una forma farmacéutica
        /// </summary>
        /// <param name="PharmaceuticalForm"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.PharmaceuticalFormGrouping> SavePharmaceuticalFormGrouping(Domain.Entities.PharmaceuticalFormGrouping PharmaceuticalForm, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad de PharmaceuticalFormGrouping
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.PharmaceuticalFormGrouping> ChangeStatePharmaceuticalFormGrouping(string code, bool state, AuditMessage audit);

        /// <summary>
        /// Consulta una forma farmacéutica por código
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.PharmaceuticalFormGrouping> GetPharmaceuticalFormGrouping(string code, AuditMessage audit);

        /// <summary>
        /// Consulta una forma farmacéutica por id
        /// </summary>
        /// <param name="idProductGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.PharmaceuticalFormGrouping> GetPharmaceuticalFormGroupingById(int idPharmaceuticalForm, AuditMessage audit);
    }
}
