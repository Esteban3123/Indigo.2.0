using Domain.Base.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Inventory.DecreaseMaximumLimit
{
    public interface IDecreaseMaximumLimitAdminService : IDisposable
    {
        ActionResult SaveDecreaseProduct(List<Domain.Entities.DecreaseMaximumLimit> listDecrease, AuditMessage audit);
    }
}
