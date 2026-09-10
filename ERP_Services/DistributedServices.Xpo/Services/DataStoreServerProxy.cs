
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.Xpo.Helpers;
using DistributedService.Xpo.Deployment;
using DistributedServices.Xpo.Base;
using DistributedServices.Xpo.Contracts;
using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Xml;

namespace DistributedServices.Xpo.Services
{
    /// <summary>
    /// Clase que implementa la funcionalidad de cache para los objetos XPO
    /// </summary>
    /// <seealso cref="DistributedServices.Xpo.Base.IndigoServiceBase" />
    /// <seealso cref="DistributedServices.Xpo.Contracts.IDataStoreContract" />
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, 
        ConcurrencyMode = ConcurrencyMode.Multiple, 
        IncludeExceptionDetailInFaults = true, 
        MaxItemsInObjectGraph = int.MaxValue)]
    public class DataStoreServerProxy : IndigoServiceBase, IDataStoreContract
    {
        #region Constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="DataStoreServerProxy"/> class.
        /// </summary>
        public DataStoreServerProxy() : base(null) { }
        /// <summary>
        /// Initializes the <see cref="DataStoreServerProxy"/> class.
        /// </summary>
        static DataStoreServerProxy()
        {
            dataStoreProviderDictionary = new Dictionary<string, Tuple<ICachedDataStore, Dictionary<string, ICachedDataStore>>>();
        }
        #endregion

        #region IDataStoreContract Members        
        /// <summary>
        /// Notifies the dirty tables.
        /// </summary>
        /// <param name="company">The company.</param>
        /// <param name="cookie">The cookie.</param>
        /// <param name="dirtyTablesNames">The dirty tables names.</param>
        /// <returns></returns>
        public DataCacheResult NotifyDirtyTables(string company, 
            DataCacheCookie cookie, params string[] dirtyTablesNames) => GetCachedDataStore(company)
                .NotifyDirtyTables(cookie, dirtyTablesNames);

        /// <summary>
        /// Processes the cookie.
        /// </summary>
        /// <param name="company">The company.</param>
        /// <param name="cookie">The cookie.</param>
        /// <returns></returns>
        public DataCacheResult ProcessCookie(string company, 
            DataCacheCookie cookie) => GetCachedDataStore(company).ProcessCookie(cookie);

        /// <summary>
        /// Selects the data.
        /// </summary>
        /// <param name="company">The company.</param>
        /// <param name="cookie">The cookie.</param>
        /// <param name="selects">The selects.</param>
        /// <returns></returns>
        public DataCacheSelectDataResult SelectData(string company, DataCacheCookie cookie, SelectStatement[] selects)
        {
            return ValidateCacheNode(selects, company).SelectData(cookie, selects);
        }

        /// <summary>
        /// Selects the data.
        /// </summary>
        /// <param name="company">The company.</param>
        /// <param name="selects">The selects.</param>
        /// <returns></returns>
        public SelectedData SelectData(string company, params SelectStatement[] selects)
        {
            return ValidateCacheNode(selects, company).SelectData(selects);
        }
        #endregion  
    }
}
