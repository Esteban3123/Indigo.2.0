using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DistributedServices.Xpo.Contracts
{
    [ServiceContract]
    public interface IClearCacheXpo
    {
        [OperationContract()]
        void ClearAllCache();
        [OperationContract()]
        void ClearCache(string tableName);
    }
}
