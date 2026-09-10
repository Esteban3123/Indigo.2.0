///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Carlos Ernesto Cordoba
/// Created          : 08-01-2015
///
/// Copyright        : (c) . All rights reserved.
///***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using System.ServiceModel;
using Domain.Crystal.Entities;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryServicePharmaceuticalDispensingDevolution
    {
        /// <summary>
        /// metodo para hacer la devolucion del sumistro desde el dashboard
        /// </summary>
        /// <param name="ListPharmaceuticalDispensingDevolution"></param>
        /// <param name="ListDetailAnnular"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Base.Entities.ActionResult<string> SaveDashboardPharmacyDevolution(List<Domain.Entities.PharmaceuticalDispensingDevolution> ListPharmaceuticalDispensingDevolution, List<Domain.Crystal.Entities.ViewDashboardPharmacyDetailDevolution> ListDetailAnnular, long idSequense, AuditMessage audit);
        /// <summary>
        /// obtiene una devolucion por id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.PharmaceuticalDispensingDevolution GetPharmaceuticalDispensingDevolutionById(int id);
        /// <summary>
        /// obtiene una devolucion por codigo
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="audit">The audit.</param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.PharmaceuticalDispensingDevolution GetPharmaceuticalDispensingDevolutionByCode(string code, AuditMessage audit);
        /// <summary>
        /// guarda una devolucion
        /// </summary>
        /// <param name="pharmaceuticalDispensingDevolution">The pharmaceutical dispensing devolution.</param>
        /// <param name="audit">The audit.</param>
        /// <param name="idSequence">The identifier sequence.</param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.PharmaceuticalDispensingDevolution> SavePharmaceuticalDispensingDevolution(Domain.Entities.PharmaceuticalDispensingDevolution pharmaceuticalDispensingDevolution, long idSequense, AuditMessage audit, Domain.Entities.InventorySequence sequenceC);
        /// <summary>
        /// guarda y confirma una devolucion
        /// </summary>
        /// <param name="pharmaceuticalDispensingDevolution"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.PharmaceuticalDispensingDevolution> SaveAndConfirmPharmaceuticalDispensingDevolution(Domain.Entities.PharmaceuticalDispensingDevolution pharmaceuticalDispensingDevolution, long idSequense, AuditMessage audit, Domain.Entities.InventorySequence sequenceC, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert);


    }
}
