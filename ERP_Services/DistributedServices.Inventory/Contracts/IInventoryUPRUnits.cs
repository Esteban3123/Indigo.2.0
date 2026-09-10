using Domain.Base.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryUPRUnits
        
    {
        /// <summary>
        /// Guarda o actualiza una Unidad UPR
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.UPRUnits> SaveUPRUnits(Domain.Entities.UPRUnits uPRUnits, long idSequense, AuditMessage audit);

        /// <summary>
        /// Obtiene una Unidad UPR por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.UPRUnits> GetUPRUnits(string code, AuditMessage audit);

        /// <summary>
        /// Obtiene una Unidad UPR por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.UPRUnits> GetUPRUnitsById(int id, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.UPRUnits> ChangeStateUPRUnits(string code, bool state, AuditMessage audit);

    }
}
