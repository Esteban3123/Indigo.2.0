//'************************************************************
//' Assembly         : DistributedService
//' Author           : Carlos Mario Arias Rubiano
//' Created          : 07/04/2018
//'
//' Copyright        : (c) . All rights reserved.
//'************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using System.ServiceModel;
using Domain.Entities;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryServiceConsumeService
    {

        /// <summary>
        /// Obtiene la dispensación por formula medica
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ActionResult<WebServiceObject> GetDispensingByPatient(string parameters);

        /// <summary>
        /// Obtiene un listado de dispensaciones por rango de fechas
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ActionResult<string> GetDispensingByDateRange(string parameters);

        /// <summary>
        /// Confirma la dispensación con integración a HEON
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ActionResult PostConfirmIntegration(string parameters, Domain.Entities.PharmaceuticalDispensing PharmaceuticalDispensing);

    }
}
