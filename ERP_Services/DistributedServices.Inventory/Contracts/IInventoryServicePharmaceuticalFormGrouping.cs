///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Cesar Augusto Collazos 
/// Created          : 29-02-2024
/// 
/// Copyright        : (c) . All rights reserved.
///***********************************************************************

using System.ServiceModel;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryServicePharmaceuticalFormGrouping
    {

        /// <summary>
        /// Guarda o actualiza una forma farmacéutica
        /// </summary>
        /// <param name="PharmaceuticalForm"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<PharmaceuticalFormGrouping> SavePharmaceuticalFormGrouping(PharmaceuticalFormGrouping PharmaceuticalForm, AuditMessage audit);

        /// <summary>
        /// Obtiene una forma farmacéutica por código
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<PharmaceuticalFormGrouping> GetPharmaceuticalFormGrouping(string code, AuditMessage audit);

        /// <summary>
        /// Obtiene una forma farmacéutica por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<PharmaceuticalFormGrouping> GetPharmaceuticalFormGroupingById(int id, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<PharmaceuticalFormGrouping> ChangeStatePharmaceuticalFormGrouping(string code, bool state, AuditMessage audit);

    }
}