//'************************************************************
//' Assembly         : Domain.Inventory.IGroupRepository
//' Author           : Juan Carlos Bermudez Gutierrez
//' Created          : 09/04/2015
//'
//' Copyright        : (c) . All rights reserved.
//'************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using DistributedServices.ConsumeService;
using Domain.Entities;
using DistributedServices.Inventory.Unity;
using Application.Inventory.ConsumeService;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {
        /// <summary>
        /// Consume el servicio de HEON
        /// </summary>
        /// <returns></returns>
        public ActionResult<WebServiceObject> GetDispensingByPatient(string parameters)
        {
            using (var service = Container.Current.Resolve<IConsumeServiceAdminService>())
            {
                return service.GetDispensingByPatient(parameters);
            }
        }

        /// <summary>
        /// Obtiene un listado de dispensaciones por rango de fechas
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public ActionResult<string> GetDispensingByDateRange(string parameters)
        {
            using (var service = Container.Current.Resolve<IConsumeServiceAdminService>())
            {
                return service.GetDispensingByDateRange(parameters);
            }
        }

        /// <summary>
        /// Confirma la dispensación con integración a HEON
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="PharmaceuticalDispensing"></param>
        /// <returns></returns>
        public ActionResult PostConfirmIntegration(string parameters, PharmaceuticalDispensing PharmaceuticalDispensing)
        {
            using (var service = Container.Current.Resolve<IConsumeServiceAdminService>())
            {
                AuditMessage audit = OperationContext.Current.IncomingMessageHeaders.GetHeader<AuditMessage>(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE);
                return service.PostConfirmIntegration(parameters, PharmaceuticalDispensing, audit);
            }
        }
    }
}
