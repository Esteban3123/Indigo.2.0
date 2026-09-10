using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using DistributedServices.Inventory.Contracts;
using Application.Inventory.LoanMerchandiseDevolutionDetail;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService: IInventoryServiceLoanMerchandiseDevolutionDetail 
    {
        public List<Domain.Entities.LoanMerchandiseDevolutionDetail> ListLoanMerchandiseDevolutionDetailByIdLoanMerchandiseDevolution(int IdLoanMerchandiseDevolution)
        {
            using (var service = Container.Current.Resolve<ILoanMerchandiseDevolutionDetailAdminService>())
            {
                return service.ListLoanMerchandiseDevolutionDetailByIdLoanMerchandise(IdLoanMerchandiseDevolution);
            }
            //return _loanMerchandiseDevolutionDetailAdminService.ListLoanMerchandiseDevolutionDetailByIdLoanMerchandise(IdLoanMerchandiseDevolution);
        }
    }
}
