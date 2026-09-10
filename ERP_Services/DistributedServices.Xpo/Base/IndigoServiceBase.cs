using DevExpress.Xpo.DB;
using DevExpress.Xpo.DB.Helpers;
using DistributedServices.Xpo.Contracts;
using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace DistributedServices.Xpo.Base
{
    public class IndigoServiceBase : CachedDataStoreService, IClearCacheXpo, IDisposable
    {
        #region Properties
        public static Dictionary<string, Tuple<ICachedDataStore, Dictionary<string, ICachedDataStore>>> dataStoreProviderDictionary;
        /// <summary>
        /// Xml que contiene las entidades XPO que implementan cache
        /// </summary>
        public static XmlDocument xmlNodeData;
        /// <summary>
        /// variable utilizada para conocer si se implementa cache o no
        /// </summary>
        public static bool IsCached = false;
        /// <summary>
        /// The wrapped data store
        /// </summary>
        // public static ICachedDataStore wrappedDataStore;
        /// <summary>
        /// The cache root
        /// </summary>
        public static DataCacheRoot _cacheRoot;
        ///// <summary>
        ///// The dictionary cache node
        ///// </summary>
        //private static Dictionary<string, ICachedDataStore> _dictionaryCacheNode
        //    = new Dictionary<string, ICachedDataStore>();
        #endregion

        #region Builder        
        /// <summary>
        /// Initializes a new instance of the <see cref="IndigoServiceBase"/> class.
        /// </summary>
        /// <param name="wrappedDataStore">The wrapped data store.</param>
        public IndigoServiceBase(ICachedDataStore wrappedDataStore) : base(wrappedDataStore)
        {
            IsCached = System.Configuration.ConfigurationManager.AppSettings.Get("CachedEnable").ToLower().Equals("true");
        }
        #endregion

        #region Methods
        // <summary>
        // Obtiene la conexión al almacén de datos
        // </summary>
        // <param name="dataBase">Base de datos a consultar</param>
        // <returns>Conexión al almacén de datos</returns>
        public static ICachedDataStore GetCachedDataStore(string dataBase)
        {
            try
            {
                if (!dataStoreProviderDictionary.ContainsKey(dataBase))
                {
                    IDisposable[] objectsToDisposeOnDisconnect = null;
                    AutoCreateOption defaultAutoCreateOption = AutoCreateOption.None;

                    string connectionString = string.Format(Helper.GetXpoConnectionStringByConnectionKey(), dataBase);
                    IDataStore dataStore;
                    ConnectionStringParser helper = new ConnectionStringParser(connectionString);
                    string providerType = helper.GetPartByName(DataStoreBase.XpoProviderTypeParameterName);
                    if (providerType != null && providerType.Length == 0)
                        providerType = null;
                    string plainConnectionString = string.Empty;
                    bool? pool;
                    int? poolSize;
                    int? poolMaxConnections;
                    GetPoolParameters(helper, ref plainConnectionString, out pool, out poolSize, out poolMaxConnections);
                    if ((pool.HasValue && pool.Value) || (!pool.HasValue && (poolSize.HasValue || poolMaxConnections.HasValue)))
                    {
                        helper.RemovePartByName(DataStoreBase.XpoProviderTypeParameterName);
                        plainConnectionString = helper.GetConnectionString();
                        IndigoDataStorePool dataStorePool = new IndigoDataStorePool(defaultAutoCreateOption, plainConnectionString, poolSize, poolMaxConnections);
                        objectsToDisposeOnDisconnect = new IDisposable[] { dataStorePool };
                        dataStore = dataStorePool;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(plainConnectionString))
                            plainConnectionString = connectionString;
                        if (providerType != null)
                        {
                            helper.RemovePartByName(DataStoreBase.XpoProviderTypeParameterName);
                            plainConnectionString = helper.GetConnectionString();
                        }
                        dataStore = DistributedService.Xpo.Deployment.MyMSSqlConnectionProvider.CreateProviderFromString(
                            plainConnectionString, defaultAutoCreateOption, out objectsToDisposeOnDisconnect);
                    }
                    _cacheRoot = new DataCacheRoot(dataStore);
                    _cacheRoot.Configure(new DataCacheConfiguration(DataCacheConfigurationCaching.All));

                    dataStoreProviderDictionary.Add(dataBase, new Tuple<ICachedDataStore, Dictionary<string, ICachedDataStore>>(_cacheRoot, new Dictionary<string, ICachedDataStore>()));
                }
                return dataStoreProviderDictionary[dataBase].Item1;
            }
            catch (Exception ex)
            {
                Helper.HandledException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Gets the pool parameters.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="plainConnectionString">The plain connection string.</param>
        /// <param name="pool">The pool.</param>
        /// <param name="poolSize">Size of the pool.</param>
        /// <param name="poolMaxConnections">The pool maximum connections.</param>
        private static void GetPoolParameters(ConnectionStringParser helper, ref string plainConnectionString, out bool? pool, out int? poolSize, out int? poolMaxConnections)
        {
            pool = null;
            poolSize = null;
            poolMaxConnections = null;
            string poolString = helper.GetPartByName(DataStorePool.XpoPoolParameterName);
            string poolSizeString = helper.GetPartByName(DataStorePool.XpoPoolSizeParameterName);
            string poolMaxConnectionsString = helper.GetPartByName(DataStorePool.XpoPoolMaxConnectionsParameterName);
            if (!string.IsNullOrEmpty(poolString))
            {
                pool = true;
            }
            if (!string.IsNullOrEmpty(poolSizeString))
            {
                int poolSizeInt;
                if (int.TryParse(poolSizeString, out poolSizeInt))
                {
                    poolSize = poolSizeInt;
                }
            }
            if (!string.IsNullOrEmpty(poolMaxConnectionsString))
            {
                int poolMaxConnectionsInt;
                if (int.TryParse(poolMaxConnectionsString, out poolMaxConnectionsInt))
                {
                    poolMaxConnections = poolMaxConnectionsInt;
                }
            }
            if (helper.PartExists(DataStorePool.XpoPoolParameterName)
                || helper.PartExists(DataStorePool.XpoPoolSizeParameterName)
                || helper.PartExists(DataStorePool.XpoPoolMaxConnectionsParameterName))
            {
                helper.RemovePartByName(DataStorePool.XpoPoolParameterName);
                helper.RemovePartByName(DataStorePool.XpoPoolSizeParameterName);
                helper.RemovePartByName(DataStorePool.XpoPoolMaxConnectionsParameterName);
                plainConnectionString = helper.GetConnectionString();
            }
        }

        /// <summary>
        /// Se valida el select para saber que entidad va a llamar para asi poder determinar el nodo donde se encuentra
        /// </summary>
        /// <param name="selects">The selects.</param>
        public ICachedDataStore ValidateCacheNode(SelectStatement[] selects, string database)
        {
            if (!dataStoreProviderDictionary.ContainsKey(database))
                GetCachedDataStore(database);
            if (!IsCached)
                return dataStoreProviderDictionary[database].Item1;
            if (selects != null && selects.Length > 0)
            {
                if (xmlNodeData == null)
                {
                    xmlNodeData = new XmlDocument();
                    xmlNodeData.LoadXml(ResourceXPO.IndigoCacheLevel);
                }
                if (xmlNodeData.SelectNodes("/XpoCacheData/CacheLevel").Count > 0)
                {
                    //XmlNode obj = xmlNodeData.SelectSingleNode($"/XpoCacheData/CacheLevel[XpoEntity='{selects[0].TableName.Split('.').LastOrDefault()}']");
                    XmlNode obj = xmlNodeData.SelectSingleNode($"/XpoCacheData/CacheLevel[XpoEntity='{selects[0].Table.Name}']");
                    if (obj != null)
                    {
                        if (dataStoreProviderDictionary[database].Item2.ContainsKey(obj.Attributes.Item(0).Value))
                            return dataStoreProviderDictionary[database].Item2[obj.Attributes.Item(0).Value];
                        else
                        {
                            dataStoreProviderDictionary[database].Item2.Add(obj.Attributes.Item(0).Value, null);
                            IndigoDataCacheNode node = new IndigoDataCacheNode(_cacheRoot)
                            {                                
                                MaxCacheLatency = TimeSpan.FromSeconds(Convert.ToDouble(obj.Attributes.Item(0).Value))
                            };
                            node.ProcessCookie(DataCacheCookie.Empty);
                            dataStoreProviderDictionary[database].Item2[obj.Attributes.Item(0).Value] = node;

                            return dataStoreProviderDictionary[database].Item2[obj.Attributes.Item(0).Value];
                        }
                    }
                    else
                        return dataStoreProviderDictionary[database].Item1;
                }
                else
                    return dataStoreProviderDictionary[database].Item1;
            }
            else
                return dataStoreProviderDictionary[database].Item1;
        }
        
        public void ClearAllCache()
        {
            if (dataStoreProviderDictionary.Any())
            {
                foreach (var item in dataStoreProviderDictionary)
                {
                    ((DataCacheRoot)item.Value.Item1).Reset();

                    if (item.Value.Item2.Any())
                    {
                        foreach (var item2 in item.Value.Item2)
                        {
                            ((IndigoDataCacheNode)item2.Value).Reset();
                        }
                    }
                }
            }
        }

        public void ClearCache(string tableName)
        {
            
        }
        #endregion

        #region IDisposable
        bool disposed = false;
        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            if (disposing)
            {
                // Free any other managed objects here.               
            }
            IndigoGC.Execute();
            disposed = true;
        }        
        #endregion
    }
}
