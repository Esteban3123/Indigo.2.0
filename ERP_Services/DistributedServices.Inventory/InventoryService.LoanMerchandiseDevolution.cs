using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using DistributedServices.Inventory.Contracts;
using Application.Inventory.LoanMerchandiseDevolution;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService : IInventoryServiceLoanMerchandiseDevolution
    {

        public Domain.Entities.LoanMerchandiseDevolution GetLoanMerchadiseDevolutionByCode(string Code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<ILoanMerchandiseDevolutionAdminService>())
            {                
                return service.GetLoanMerchadiseDevolutionByCode(Code, audit);
            }
            //return _loanMerchandiseDevolutionAdminService.GetLoanMerchadiseDevolutionByCode (Code, audit);
        }

        public Domain.Entities.LoanMerchandiseDevolution GetLoanMerchadiseDevolutionById(int id)
        {
            using (var service = Container.Current.Resolve<ILoanMerchandiseDevolutionAdminService>())
            {
                return service.GetLoanMerchadiseDevolutionById(id);
            }
            //return _loanMerchandiseDevolutionAdminService.GetLoanMerchadiseDevolutionById (id);
        }

        public Domain.Base.Entities.ActionResult<Domain.Entities.LoanMerchandiseDevolution> SaveLoanMerchadiseDevolution(Domain.Entities.LoanMerchandiseDevolution loadmerchadiseDevolution, AuditMessage audit, long idSequense, Domain.Entities.InventorySequence sequenceC)
        {
            using (var service = Container.Current.Resolve<ILoanMerchandiseDevolutionAdminService>())
            {                
                return service.SaveLoanMerchadiseDevolution(loadmerchadiseDevolution, audit, idSequense, sequenceC);
            }
            //return _loanMerchandiseDevolutionAdminService.SaveLoanMerchadiseDevolution(loadmerchadiseDevolution, audit, idSequense, sequenceC);
        }


        public async Task<Domain.Base.Entities.ActionResult<Domain.Entities.LoanMerchandiseDevolution>> SaveAndConfirmLoadMerchadiseDevolution(Domain.Entities.LoanMerchandiseDevolution loadMerchadiseDevolution, long idSequense, AuditMessage audit, Domain.Entities.InventorySequence sequenceC, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert)
        {
            using (var service = Container.Current.Resolve<ILoanMerchandiseDevolutionAdminService>())
            {                
                return await service.SaveAndConfirmLoadMerchadiseDevolution(loadMerchadiseDevolution, audit, idSequense, action, sequenceC);
            }
            //return _loanMerchandiseDevolutionAdminService.SaveAndConfirmLoadMerchadiseDevolution(loadMerchadiseDevolution, audit, idSequense, action, sequenceC);
        }
    }
}
