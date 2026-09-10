///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Rafael Eduardo Patiño Cabrera
/// Created          : 08-05-2015
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
namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryServiceLoanMerchandiseDevolution
    {
        /// <summary>
        /// obtiene una devolucion de prestamo por su codigo
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.LoanMerchandiseDevolution  GetLoanMerchadiseDevolutionByCode(string Code, AuditMessage audit);
        /// <summary>
        /// otiene una devolucion de prestamo por su id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.LoanMerchandiseDevolution GetLoanMerchadiseDevolutionById(int id);
        /// <summary>
        /// guarda una devolucion de prestamo
        /// </summary>
        /// <param name="loadmerchadise"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.LoanMerchandiseDevolution> SaveLoanMerchadiseDevolution(Domain.Entities.LoanMerchandiseDevolution loadmerchadiseDevolution, AuditMessage audit, long idSequense, Domain.Entities.InventorySequence sequenceC);
        /// <summary>
        /// guardar y confirmar una devolucion de prestamo
        /// </summary>
        /// <param name="loadMerchadise"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [OperationContract]
        Task<ActionResult<Domain.Entities.LoanMerchandiseDevolution>> SaveAndConfirmLoadMerchadiseDevolution(Domain.Entities.LoanMerchandiseDevolution loadMerchadiseDevolution, long idSequense, AuditMessage audit, Domain.Entities.InventorySequence sequenceC, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert);
    }
}
