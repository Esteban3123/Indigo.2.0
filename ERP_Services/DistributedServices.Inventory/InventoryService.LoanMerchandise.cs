using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using DistributedServices.Inventory.Contracts;
using Application.Inventory.LoanMerchandise;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService : IInventoryServiceLoanMerchandise
    {
        public Domain.Entities.LoanMerchandise GetLoanMerchadiseByCode(string Code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<ILoanMerchandiseAdminService>())
            {                
                return service.GetLoanMerchadiseByCode(Code, audit);
            }
            //return _loanMerchandiseAdminService.GetLoanMerchadiseByCode(Code, audit);
        }

        public Domain.Entities.LoanMerchandise GetLoanMerchadiseById(int id)
        {
            using (var service = Container.Current.Resolve<ILoanMerchandiseAdminService>())
            {
                return service.GetLoanMerchadiseById(id);
            }
            //return _loanMerchandiseAdminService.GetLoanMerchadiseById(id);
        }

        public Domain.Base.Entities.ActionResult<Domain.Entities.LoanMerchandise> SaveLoanMerchadise(Domain.Entities.LoanMerchandise loadmerchadise, AuditMessage audit, long idSequense, Domain.Entities.InventorySequence sequenceC)
        {
            using (var service = Container.Current.Resolve<ILoanMerchandiseAdminService>())
            {               
                return service.SaveLoanMerchadise(loadmerchadise, audit, idSequense, sequenceC);
            }
            //return _loanMerchandiseAdminService.SaveLoanMerchadise(loadmerchadise, audit, idSequense, sequenceC);
        }

        public Domain.Base.Entities.ActionResult<Domain.Entities.LoanMerchandise> ConfirmLoandMerchadise(Domain.Entities.LoanMerchandise loadMerchadise)
        {
            using (var service = Container.Current.Resolve<ILoanMerchandiseAdminService>())
            {
                throw new NotImplementedException();
            }
        }

        public Domain.Base.Entities.ActionResult<Domain.Entities.LoanMerchandise> SaveAndConfirmLoadMerchadise(Domain.Entities.LoanMerchandise loadMerchadise, long idSequense, AuditMessage audit, Domain.Entities.InventorySequence sequenceC, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert)
        {
            using (var service = Container.Current.Resolve<ILoanMerchandiseAdminService>())
            {               
                return service.SaveAndConfirmLoadMerchadise(loadMerchadise, audit, idSequense, action, sequenceC);
            }
            //return _loanMerchandiseAdminService.SaveAndConfirmLoadMerchadise(loadMerchadise, audit, idSequense, action, sequenceC);
        }

    }
}
