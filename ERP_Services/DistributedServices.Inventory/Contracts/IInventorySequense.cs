///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Carlos Mario Arias Rubiano
/// Created          : 08/09/2014
///
/// Copyright        : (c) . All rights reserved.
///***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using System.ServiceModel;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventorySequense
    {
        /// <summary>
        /// Obtiene secuencia numerica para el formulario
        /// </summary>
        /// <param name="idForm"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.InventorySequence GetSequenseByIdForm(string idForm);

        /// <summary>
        /// Obtiene un grupo de secuencias numericas por su id de configuracion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        List<string> GetNumericSequenseGroupById(Int32 id);

        [OperationContract]
        ActionResult SaveSequence(Domain.Entities.InventorySequence seq);
    }
}
