using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using DistributedServices.Inventory.Contracts;
using Application.Inventory.LoanMerchandiseDetail;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService: IInventoryServiceLoanMerchandiseDetail 
    {
        public List<Domain.Entities.LoanMerchandiseDetail> ListLoanMerchandiseDetailByIdLoanMerchandise(int IdLoanMerchandise, bool isDevolution)
        {
            using (var service = Container.Current.Resolve<ILoanMerchandiseDetailAdminService>())
            {
                return service.ListLoanMerchandiseDetailByIdLoanMerchandise(IdLoanMerchandise, isDevolution);
            }
            //return _loanMerchandiseDeatilAdminService.ListLoanMerchandiseDetailByIdLoanMerchandise(IdLoanMerchandise, isDevolution);
        }
    }
}
