using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DistributedServices.Xpo.Base
{
    public class IndigoDataStorePool : DataStoreForkBase, IDisposable
    {
        readonly AutoCreateOption aco;
        readonly string ConnectionString;
        readonly Queue<IDataStore> freiPool = new Queue<IDataStore>();
        readonly Dictionary<IDataStore, IDisposable[]> garbage = new Dictionary<IDataStore, IDisposable[]>();
        readonly int PoolSize;
        readonly int MaxConnections;
        int threads, connections;
        readonly ManualResetEvent freeProvider = new ManualResetEvent(false);
        bool isDisposed;
        public object SyncRoot { get { return this; } }
        public static int DefaultPoolSize = 8;
        public static int DefaultMaxConnections = -1;
        public const string XpoPoolParameterName = "XpoDataStorePool";
        public const string XpoPoolSizeParameterName = "XpoDataStorePoolSize";
        public const string XpoPoolMaxConnectionsParameterName = "XpoDataStorePoolMaxConnections";
        public IndigoDataStorePool(AutoCreateOption autoCreateOption, string connectionString) : this(autoCreateOption, connectionString, null, null) { }
        public IndigoDataStorePool(AutoCreateOption autoCreateOption, string connectionString, int? poolSize) : this(autoCreateOption, connectionString, poolSize, null) { }
        public IndigoDataStorePool(AutoCreateOption autoCreateOption, string connectionString, int? poolSize, int? maxConnections)
            : base()
        {
            this.aco = autoCreateOption;
            this.ConnectionString = connectionString;
            this.PoolSize = poolSize ?? DefaultPoolSize;
            if (this.PoolSize < 0)
                this.PoolSize = int.MaxValue;
            this.MaxConnections = maxConnections ?? DefaultMaxConnections;
            if (this.MaxConnections < 0)
                this.MaxConnections = int.MaxValue;
            if (this.MaxConnections < this.PoolSize)
                this.MaxConnections = this.PoolSize;
            if (this.MaxConnections == 0)
                this.MaxConnections = 1;
        }
        public override AutoCreateOption AutoCreateOption
        {
            get { return this.aco; }
        }
        public override IDataStore AcquireChangeProvider()
        {
            bool threadRegistered = false;
            for (;;)
            {
                lock (SyncRoot)
                {
                    if (isDisposed)
                    {
                        if (threadRegistered)
                            --threads;
                        throw new ObjectDisposedException(this.ToString());
                    }
                    if (!threadRegistered)
                    {
                        threadRegistered = true;
                        ++threads;
                    }
                    if (freiPool.Count > 0)
                    {
                        return freiPool.Dequeue();
                    }
                    if (connections < MaxConnections)
                    {
                        ++connections;
                        break;
                    }
                    freeProvider.Reset();
                }
                freeProvider.WaitOne();
            }
            try
            {
                return CreateProvider();
            }
            catch
            {
                lock (SyncRoot)
                {
                    --connections;
                    if (threadRegistered)
                    {
                        --threads;
                    }
                }
                throw;
            }
        }
        public override void ReleaseChangeProvider(IDataStore provider)
        {
            lock (SyncRoot)
            {
                --threads;
                if ((freiPool.Count < this.PoolSize || connections <= threads) && !isDisposed)
                {
                    freiPool.Enqueue(provider);
                    freeProvider.Set();
                    return;
                }
                --connections;
            }
            DestroyProvider(provider);
        }
        IDataStore CreateProvider()
        {
            IDisposable[] toDispose;            
            IDataStore rv = DistributedService.Xpo.Deployment.MyMSSqlConnectionProvider.CreateProviderFromString(
                this.ConnectionString, this.AutoCreateOption, out toDispose);
            //IDataStore rv = XpoDefault.GetConnectionProvider(this.ConnectionString, this.AutoCreateOption, out toDispose);
            lock (this.garbage)
            {
                this.garbage.Add(rv, toDispose);
            }
            return rv;
        }
        void DestroyProvider(IDataStore provider)
        {
            IDisposable[] toDispose;
            lock (this.garbage)
            {
                toDispose = garbage[provider];
                garbage.Remove(provider);
            }
            if (toDispose != null)
            {
                foreach (IDisposable d in toDispose)
                {
                    d.Dispose();
                }
            }
        }
        public override IDataStore AcquireReadProvider()
        {
            return this.AcquireChangeProvider();
        }
        public override void ReleaseReadProvider(IDataStore provider)
        {
            this.ReleaseChangeProvider(provider);
        }
        public void Dispose()
        {
            lock (SyncRoot)
            {
                if (isDisposed)
                    return;
                isDisposed = true;
            }
            for (;;)
            {
                IDataStore prov;
                lock (SyncRoot)
                {
                    if (freiPool.Count == 0)
                        break;
                    prov = freiPool.Dequeue();
                }
                DestroyProvider(prov);
            }
        }
    }
}
