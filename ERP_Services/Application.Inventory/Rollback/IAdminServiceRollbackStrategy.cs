using Domain.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Inventory.Rollback
{
    public interface IAdminServiceRollbackStrategy:IDisposable
    {
        Task<int> ExecuteRollbackAsync(string rollbackObjectCode);
    }
}
