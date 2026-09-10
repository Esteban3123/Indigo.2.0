///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Carlos Mario Arias Rubiano
/// Created          : 08/09/2014
///
/// Copyright        : (c) . All rights reserved.
///***********************************************************************

using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using System.Collections.Generic;
using System.ServiceModel;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryDCI
    {

        /// <summary>
        /// Guarda o actualiza un DCI
        /// </summary>
        /// <param name="DCI"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.DCI> SaveDCI(Domain.Entities.DCI DCI, List<Domain.Entities.DrugInteraction> ListDeleteDrugInteraction, List<Domain.Entities.DrugActive> ListDeleteDrugActive, List<LethalDoseLimits> ListDeleteLethalDoseLimits, List<RisksDescription> ListDeleteRisksDescription, List<DCIRiskFactors> ListDeleteDCIRiskFactors, long idSequense, SessionValues audit);

        /// <summary>
        /// Elimina un DCI
        /// </summary>
        /// <param name="DCI"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult DeleteDCI(Domain.Entities.DCI DCI, AuditMessage audit);

        /// <summary>
        /// Obtiene un DCI por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.DCI> GetDCI(string code, AuditMessage audit);

        /// <summary>
        /// Obtiene un DCI por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.DCI> GetDCIById(int id, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.DCI> ChangeStateDCI(string code, bool state, AuditMessage audit);

        /// <summary>
        /// Obtiene una interacción de medicamento por el Id del DCI padre
        /// </summary>
        /// <param name="ParentDCIid"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        List<Domain.Entities.DrugInteraction> GetDrugInteractionByDCIParentId(int ParentDCIid);

        [OperationContract]
        IEnumerable<WarningHighRiskDrugModel> GetHighRiskDrugsByAtcCode(List<string> atcCodes);
    }
}
