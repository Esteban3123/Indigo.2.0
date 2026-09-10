//using Application.Inventory.Rollback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting.Rollback
{
    public interface IRollbackStrategyFactory : IDisposable
    {
        Type GetStrategy(string source);
    }
}