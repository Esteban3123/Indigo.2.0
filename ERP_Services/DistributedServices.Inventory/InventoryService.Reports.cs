#region Imports

using Application.Inventory.Reports;
using DistributedServices.Inventory.Contracts;
using DistributedServices.Inventory.Unity;
using Infrastructure.CrossCutting.Base;
using Microsoft.Practices.Unity;
using System.Collections;
using System;
using System.Collections.Generic;
using Domain.Entities;

#endregion

namespace DistributedServices.Inventory
{
    public partial class InventoryService : IInventoryServiceReports
    {

        public System.Data.DataSet GetReportControlMedications(Dictionary<string, string> criterias, SessionValues session)
        {
            using (var service = Container.Current.Resolve<IReportsAdminService>())
            {
                return service.GetReportControlMedications(criterias, session);
            }
        }

        public System.Data.DataSet GetReportFiscalAccount(Dictionary<string, string> criterias, SessionValues session)
        {
            using (var service = Container.Current.Resolve<IReportsAdminService>())
            {
                return service.GetReportFiscalAccount(criterias, session);
            }
        }

        public List<SP_ReportValuedInventory_Result> GetReportValuedInventory(Dictionary<String, String> filters, Dictionary< String, String> range, SessionValues session)
        {
            using (var service = Container.Current.Resolve<IReportsAdminService>())
            {
                return service.GetReportValuedInventory(filters, range, session);
            }
        }
    }
}
