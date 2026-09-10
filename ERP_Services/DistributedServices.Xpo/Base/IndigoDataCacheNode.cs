using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.Xpo.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace DistributedServices.Xpo.Base
{
    [Serializable]
    public class IndigoDataCacheNode : IndigoDataCacheBase
#if DEBUG
, IDataStoreForTests
#endif
    {
#if DEBUG
        public void ClearDatabase()
        {
            using (LockForChange())
            {
                ((IDataStoreForTests)Nested).ClearDatabase();
                ResetCore();
            }
        }
#endif
        ICacheToCacheCommunicationCore _nested;
#if !SL
        int objectsInCacheForPerfCounters = 0;
#endif
        protected readonly Dictionary<string, Dictionary<string, Base.CacheRecord>> RecordsByTables = new Dictionary<string, Dictionary<string, Base.CacheRecord>>();
        protected Base.CacheRecord First, Last;
        protected DateTime LastUpdateTime = DateTime.MinValue;
        public TimeSpan MaxCacheLatency = new TimeSpan(0, 0, 30);
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use GlobalTotalMemoryPurgeThreshold field instead")]
        public static long GlobalTotalMemoryPurgeTreshhold
        {
            get { return GlobalTotalMemoryPurgeThreshold; }
            set { GlobalTotalMemoryPurgeThreshold = value; }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use TotalMemoryPurgeThreshold field instead")]
        public long TotalMemoryPurgeTreshhold
        {
            get { return TotalMemoryPurgeThreshold; }
            set { TotalMemoryPurgeThreshold = value; }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use TotalMemoryNotPurgeThreshold field instead")]
        public long TotalMemoryNotPurgeTreshhold
        {
            get { return TotalMemoryNotPurgeThreshold; }
            set { TotalMemoryNotPurgeThreshold = value; }
        }
#if CF
		public static long GlobalTotalMemoryPurgeThreshold = 32L*1024L*1024L;
		public long TotalMemoryPurgeThreshold = 32L*1024L*1024L;
		public long TotalMemoryNotPurgeThreshold = 4L * 1024L * 1024L;
#else
        public static long GlobalTotalMemoryPurgeThreshold = long.MaxValue;
        public long TotalMemoryPurgeThreshold = long.MaxValue;
        public long TotalMemoryNotPurgeThreshold = 64L * 1024L * 1024L;
#endif
        public int MinCachedRequestsAfterPurge = 16;
        protected ICacheToCacheCommunicationCore Nested { get { return _nested; } }
        bool isAutoCreateOptionCached = false;
        AutoCreateOption _AutoCreateOption = AutoCreateOption.None;
        object _AutoCreateOptionLock = new object();
        public override AutoCreateOption AutoCreateOption
        {
            get
            {
                if (!isAutoCreateOptionCached)
                {
                    lock (_AutoCreateOptionLock)
                    {
                        if (!isAutoCreateOptionCached)
                        {
                            isAutoCreateOptionCached = true;
                            try
                            {
                                _AutoCreateOption = ((IDataStore)Nested).AutoCreateOption;
                            }
                            catch { }
                        }
                    }
                }
                return _AutoCreateOption;
            }
        }
        public IndigoDataCacheNode(ICacheToCacheCommunicationCore parentCache)
            : base(parentCache as ICommandChannel)
        {
            this._nested = parentCache;
#if !SL
            PerformanceCounters.DataCacheNodeCount.Increment();
            PerformanceCounters.DataCacheNodeCreated.Increment();
#endif
        }
#if !SL
        ~IndigoDataCacheNode()
        {
            PerformanceCounters.DataCacheNodeCachedCount.Decrement(objectsInCacheForPerfCounters);
            PerformanceCounters.DataCacheNodeCachedRemoved.Increment(objectsInCacheForPerfCounters);
            objectsInCacheForPerfCounters = 0;
            PerformanceCounters.DataCacheNodeCount.Decrement();
            PerformanceCounters.DataCacheNodeFinalized.Increment();
        }
#endif
        protected override DataCacheUpdateSchemaResult UpdateSchemaCore(DataCacheCookie cookie, DBTable[] tables, bool dontCreateIfFirstTableNotExist)
        {
#if !SL
            using (IDisposable c = new PerformanceCounters.QueueLengthCounter(PerformanceCounters.DataCacheNodeTotalRequests, PerformanceCounters.DataCacheNodeTotalQueue, PerformanceCounters.DataCacheNodeSchemaUpdateRequests, PerformanceCounters.DataCacheNodeSchemaUpdateQueue))
            {
#endif
                DataCacheUpdateSchemaResult result =
                    Nested.UpdateSchema(GetCurrentCookieSafe(), tables, dontCreateIfFirstTableNotExist);
                ProcessParentResult(result);
                ProcessChildResultSinceCookie(result, cookie);
                return result;
#if !SL
            }
#endif
        }
        protected override DataCacheSelectDataResult SelectDataCore(DataCacheCookie cookie, SelectStatement[] selects)
        {
#if !SL
            using (IDisposable c = new PerformanceCounters.QueueLengthCounter(PerformanceCounters.DataCacheNodeTotalRequests, PerformanceCounters.DataCacheNodeTotalQueue, PerformanceCounters.DataCacheNodeSelectRequests, PerformanceCounters.DataCacheNodeSelectQueue))
            {
                PerformanceCounters.DataCacheNodeSelectQueries.Increment(selects.Length);
#endif
                Base.CacheRecord[] newRecords = new Base.CacheRecord[selects.Length];
                for (int i = 0; i < selects.Length; ++i)
                {
                    SelectStatement stmt = selects[i];
                    if (IsGoodForCache(stmt))
                        newRecords[i] = new Base.CacheRecord(stmt);
                }
                if (newRecords.Length > 0 && newRecords[0] != null)
                {
                    bool isFirstRecordAvailable = false;
                    using (LockForRead())
                    {
                        isFirstRecordAvailable = GetCachedRecord(newRecords[0]) != null;
                    }
                    if (isFirstRecordAvailable)
                        ProcessCurrentCookieIfNeeded();
                }
                DataCacheSelectDataResult result = new DataCacheSelectDataResult();
                SelectStatementResult[] results = new SelectStatementResult[selects.Length];
                for (int i = 0; i < selects.Length;)
                {
                    if (newRecords[i] != null)
                    {
                        using (LockForChange())
                        {
                            Base.CacheRecord rec = GetCachedRecord(newRecords[i]);
                            if (rec != null)
                            {
                                if (i == 0)
                                    result.SelectingCookie = GetCurrentCookie();
                                results[i] = rec.QueryResult;
                                PromoteRecordToMRU(rec);
                                ++i;
#if !SL
                                PerformanceCounters.DataCacheNodeCacheHit.Increment();
#endif
                                continue;
                            }
                        }
                    }
                    int statementsToSendToParent = 1;
                    using (LockForRead())
                    {
                        for (;;)
                        {
                            int candidate = i + statementsToSendToParent;
                            if (candidate >= selects.Length)
                                break;
                            if (newRecords[candidate] != null && GetCachedRecord(newRecords[candidate]) != null)
                                break;
                            statementsToSendToParent++;
                            if (statementsToSendToParent >= 128 + 64)
                            {
                                statementsToSendToParent = 128;
                                break;
                            }
                        }
                    }
                    SelectStatement[] nestedCallSelects = new SelectStatement[statementsToSendToParent];
                    for (int j = 0; j < statementsToSendToParent; ++j)
                    {
                        nestedCallSelects[j] = selects[i + j];
                    }
                    DataCacheSelectDataResult rootResult = Nested.SelectData(GetCurrentCookieSafe(), nestedCallSelects);
                    ProcessParentResult(rootResult);
                    if (i == 0)
                        result.SelectingCookie = rootResult.SelectingCookie;
                    for (int j = 0; j < statementsToSendToParent; ++j)
                    {
                        SelectStatementResult nr = rootResult.SelectedData.ResultSet[j];
                        results[i] = nr;
                        Base.CacheRecord newRecord = newRecords[i];
                        bool passthrough;
                        if (newRecord == null)
                        {
                            passthrough = true;
                        }
                        else
                        {
                            using (LockForChange())
                            {
                                if (GetCachedRecord(newRecord) == null && IsGoodForCache(selects[i], nr) && IsNotInvalidated(newRecord, rootResult.SelectingCookie))
                                {
                                    newRecord.QueryResult = nr;
                                    RegisterNewRecord(newRecord);
                                    passthrough = false;
                                }
                                else
                                {
                                    passthrough = true;
                                }
                            }
                        }
#if !SL
                        if (passthrough)
                        {
                            PerformanceCounters.DataCacheNodeCachePassthrough.Increment();
                        }
                        else
                        {
                            PerformanceCounters.DataCacheNodeCacheMiss.Increment();
                        }
#else
						if(passthrough) { }
#endif
                        ++i;
                    }
                }
                result.SelectedData = new SelectedData(results);
                ProcessChildResultSinceCookie(result, cookie);
                return result;
#if !SL
            }
#endif
        }
        bool IsNotInvalidated(Base.CacheRecord newRecord, DataCacheCookie actualCookie)
        {
            ValidateLockedRead();
            if (actualCookie.Guid != this.MyGuid)
                return false;
            if (actualCookie.Age == this.Age)
                return true;
            System.Diagnostics.Debug.Assert(actualCookie.Age < this.Age);
            foreach (string tblName in newRecord.TablesInStatement)
            {
                long tblAge;
                if (TablesAges.TryGetValue(tblName, out tblAge))
                {
                    if (tblAge > actualCookie.Age)
                        return false;
                }
            }
            return true;
        }
        protected virtual bool IsGoodForCache(SelectStatement stmt)
        {
            if (IsBadForCache(cacheConfiguration, stmt)) return false;
            List<JoinNode> listToCollectNodes = new List<JoinNode>();
            IndeterminateStatmentFinder indeterminateStatementFinder = new IndeterminateStatmentFinder(listToCollectNodes);
            foreach (CriteriaOperator criteria in stmt.Operands)
            {
                if (indeterminateStatementFinder.Process(criteria)) return false;
            }
            if (indeterminateStatementFinder.Process(stmt.Condition)) return false;
            foreach (CriteriaOperator criteria in stmt.GroupProperties)
            {
                if (indeterminateStatementFinder.Process(criteria)) return false;
            }
            if (indeterminateStatementFinder.Process(stmt.GroupCondition)) return false;
            foreach (SortingColumn column in stmt.SortProperties)
            {
                if (indeterminateStatementFinder.Process(column.Property)) return false;
            }
            foreach (JoinNode node in stmt.SubNodes)
            {
                if (!IsGoodForCacheJoinNode(indeterminateStatementFinder, node)) return false;
            }
            for (int i = 0; i < listToCollectNodes.Count; i++)
            {
                if (!IsGoodForCacheJoinNode(indeterminateStatementFinder, listToCollectNodes[i])) return false;
            }
            return true;
        }
        bool IsGoodForCacheJoinNode(IndeterminateStatmentFinder indeterminateStatementFinder, JoinNode node)
        {
            if (IsBadForCache(cacheConfiguration, node)) return false;
            if (indeterminateStatementFinder.Process(node.Condition)) return false;
            foreach (JoinNode subNode in node.SubNodes)
            {
                if (!IsGoodForCacheJoinNode(indeterminateStatementFinder, subNode)) return false;
            }
            return true;
        }
        protected virtual bool IsGoodForCache(SelectStatement stmt, SelectStatementResult stmtResult)
        {
            if (IsBadForCache(cacheConfiguration, stmt)) return false;
            return true;
        }
        protected override DataCacheModificationResult ModifyDataCore(DataCacheCookie cookie, ModificationStatement[] dmlStatements)
        {
#if !SL
            using (IDisposable c = new PerformanceCounters.QueueLengthCounter(PerformanceCounters.DataCacheNodeTotalRequests, PerformanceCounters.DataCacheNodeTotalQueue, PerformanceCounters.DataCacheNodeModifyRequests, PerformanceCounters.DataCacheNodeModifyQueue))
            {
                PerformanceCounters.DataCacheNodeModifyStatements.Increment(dmlStatements.Length);
#endif
                DataCacheModificationResult result =
                    Nested.ModifyData(GetCurrentCookieSafe(), dmlStatements);
                using (LockForChange())
                {
                    ProcessParentResult(result);
                    ProcessChildResultSinceCookie(result, cookie);
                }
                return result;
#if !SL
            }
#endif
        }
        protected override DataCacheResult NotifyDirtyTablesCore(DataCacheCookie cookie, params string[] dirtyTablesNames)
        {
            if (dirtyTablesNames == null)
                dirtyTablesNames = new string[0];
#if !SL
            using (IDisposable c = new PerformanceCounters.QueueLengthCounter(PerformanceCounters.DataCacheNodeTotalRequests, PerformanceCounters.DataCacheNodeTotalQueue, PerformanceCounters.DataCacheNodeNotifyDirtyTablesRequests, PerformanceCounters.DataCacheNodeNotifyDirtyTablesQueue))
            {
                PerformanceCounters.DataCacheNodeNotifyDirtyTablesTables.Increment(dirtyTablesNames.Length);
#endif
                DataCacheResult result =
                    Nested.NotifyDirtyTables(GetCurrentCookieSafe(), dirtyTablesNames);
                using (LockForChange())
                {
                    ProcessParentResult(result);
                    ProcessChildResultSinceCookie(result, cookie);
                }
                return result;
#if !SL
            }
#endif
        }
        public void CatchUp()
        {
            NotifyDirtyTables();
        }
        protected override DataCacheResult ProcessCookieCore(DataCacheCookie cookie)
        {

#if !SL
            using (IDisposable c = new PerformanceCounters.QueueLengthCounter(PerformanceCounters.DataCacheNodeTotalRequests, PerformanceCounters.DataCacheNodeTotalQueue, PerformanceCounters.DataCacheNodeProcessCookieRequests, PerformanceCounters.DataCacheNodeProcessCookieQueue))
            {
#endif
                ProcessCurrentCookieIfNeeded();
                DataCacheResult result = new DataCacheResult();
                ProcessChildResultSinceCookie(result, cookie);
                return result;
#if !SL
            }
#endif
        }
        protected bool IsCacheFresh
        {
            get
            {
                if (MaxCacheLatency == TimeSpan.Zero)
                    return false;
                DateTime now = DateTime.UtcNow;
                DateTime lastUpdated = LastUpdateTime;
                if (now - lastUpdated >= MaxCacheLatency)
                    return false;
                if (lastUpdated - now >= MaxCacheLatency)
                    return false;
                return true;
            }
        }
        protected void ProcessCurrentCookieIfNeeded()
        {
            using (LockForRead())
            {
                if (IsCacheFresh)
                    return;
            }
            this.Reset();
            DataCacheResult result = Nested.ProcessCookie(GetCurrentCookieSafe());
            ProcessParentResult(result);
        }
        protected void ProcessParentResult(DataCacheResult result)
        {
            using (LockForChange())
            {
                if (result.CacheConfig != null)
                    cacheConfiguration = result.CacheConfig;
                if (this.MyGuid != result.Cookie.Guid)
                {
                    ResetCore();
                    this.MyGuid = result.Cookie.Guid;
                    this.Age = result.Cookie.Age;
                }
                else
                {
                    if (result.Cookie.Age > this.Age)
                    {
                        this.Age = result.Cookie.Age;
                        foreach (TableAge ta in result.UpdatedTableAges)
                        {
                            long currentAge;
                            if (!this.TablesAges.TryGetValue(ta.Name, out currentAge) || currentAge < ta.Age)
                            {
                                this.TablesAges[ta.Name] = ta.Age;
                                UnregisterRecordsForTable(ta.Name);
                            }
                        }
                    }
                }
                LastUpdateTime = DateTime.UtcNow;
            }
        }
        protected override void ResetCore()
        {
            base.ResetCore();
#if !SL
            PerformanceCounters.DataCacheNodeCachedCount.Decrement(objectsInCacheForPerfCounters);
            PerformanceCounters.DataCacheNodeCachedRemoved.Increment(objectsInCacheForPerfCounters);
            objectsInCacheForPerfCounters = 0;
#endif
            RecordsByTables.Clear();
            First = null;
            Last = null;
        }
        protected Base.CacheRecord GetCachedRecord(Base.CacheRecord sample)
        {
            ValidateLockedRead();
            return GetCachedRecord(sample.TableName, sample.HashString);
        }
        protected Base.CacheRecord GetCachedRecord(string rootTableName, string statementUniqueString)
        {
            Dictionary<string, Base.CacheRecord> nodes;
            if (!RecordsByTables.TryGetValue(rootTableName, out nodes))
                return null;
            Base.CacheRecord oldNode;
            nodes.TryGetValue(statementUniqueString, out oldNode);
            return oldNode;
        }
        protected void PromoteRecordToMRU(Base.CacheRecord record)
        {
            if (record.Prev == null)
                return;
            record.Prev.Next = record.Next;
            if (record.Next != null)
                record.Next.Prev = record.Prev;
            else
                this.Last = record.Prev;
            record.Next = this.First;
            record.Prev = null;
            this.First.Prev = record;
            this.First = record;
        }
        protected void RegisterNewRecord(Base.CacheRecord newRecord)
        {
            foreach (string tableName in newRecord.TablesInStatement)
            {
                Dictionary<string, Base.CacheRecord> tableRecords;
                if (!RecordsByTables.TryGetValue(tableName, out tableRecords))
                {
                    tableRecords = new Dictionary<string, Base.CacheRecord>();
                    RecordsByTables.Add(tableName, tableRecords);
                }
                tableRecords.Add(newRecord.HashString, newRecord);
            }
            if (this.First != null)
                this.First.Prev = newRecord;
            newRecord.Next = this.First;
            this.First = newRecord;
            if (this.Last == null)
                this.Last = newRecord;
#if !SL
            objectsInCacheForPerfCounters++;
            DevExpress.Xpo.Helpers.PerformanceCounters.DataCacheNodeCachedCount.Increment();
            DevExpress.Xpo.Helpers.PerformanceCounters.DataCacheNodeCachedAdded.Increment();
#endif
            PurgeIfNeeded();
        }
        protected void UnregisterRecordsForTable(string table)
        {
            Dictionary<string, Base.CacheRecord> tableRecords;
            if (!RecordsByTables.TryGetValue(table, out tableRecords))
                return;
            foreach (Base.CacheRecord record in new List<Base.CacheRecord>(tableRecords.Values))
            {
                RemoveFromCache(record);
            }
            System.Diagnostics.Debug.Assert(tableRecords.Count == 0);
        }
        protected void RemoveFromCache(Base.CacheRecord record)
        {
            foreach (string table in record.TablesInStatement)
            {
                Dictionary<string, Base.CacheRecord> records = RecordsByTables[table];
                System.Diagnostics.Debug.Assert(records.ContainsKey(record.HashString));
                records.Remove(record.HashString);
            }
            if (record.Prev == null)
            {
                this.First = record.Next;
            }
            else
            {
                record.Prev.Next = record.Next;
            }
            if (record.Next == null)
            {
                this.Last = record.Prev;
            }
            else
            {
                record.Next.Prev = record.Prev;
            }
#if !SL
            objectsInCacheForPerfCounters--;
            DevExpress.Xpo.Helpers.PerformanceCounters.DataCacheNodeCachedCount.Decrement();
            DevExpress.Xpo.Helpers.PerformanceCounters.DataCacheNodeCachedRemoved.Increment();
#endif
        }
        protected bool IsWorkingSetOverlap(long totalMemory)
        {
#if CF || SL
			return false;
#else
            return totalMemory > Environment.WorkingSet;
#endif
        }
        protected virtual void PurgeIfNeeded()
        {
            System.Diagnostics.Debug.Assert(Last != null);
            if (!purgingEnabled)
                return;
            long totalMemory = GC.GetTotalMemory(false);
            if (totalMemory <= TotalMemoryNotPurgeThreshold)
                return;
            if (totalMemory >= TotalMemoryPurgeThreshold || totalMemory >= GlobalTotalMemoryPurgeThreshold || IsWorkingSetOverlap(totalMemory))
            {
                DoPurge();
            }
        }
        bool purgingEnabled = true;
        class GCFlagger
        {
            public readonly IndigoDataCacheNode Node;
            public GCFlagger(IndigoDataCacheNode owner)
            {
                this.Node = owner;
                System.Diagnostics.Debug.Assert(this.Node.purgingEnabled);
                this.Node.purgingEnabled = false;
            }
            ~GCFlagger()
            {
                try
                {
                    this.Node.purgingEnabled = true;
                }
                catch { }
            }
        }
        protected virtual void DoPurge()
        {
            System.Diagnostics.Debug.Assert(Last != null);
            System.Diagnostics.Debug.Assert(purgingEnabled == true);
            new Thread(new ThreadStart(Purge)).Start();
        }
        protected virtual void Purge()
        {
            using (LockForChange())
            {
                if (Last == null)
                    return;
                if (!purgingEnabled)
                    return;
                new GCFlagger(this);
                Base.CacheRecord currentRecord = this.First;
                int recordsCount = 1;
                for (;;)
                {
                    System.Diagnostics.Debug.Assert(currentRecord != null);
                    if (currentRecord.Next == null)
                    {
                        System.Diagnostics.Debug.Assert(ReferenceEquals(currentRecord, this.Last));
                        break;
                    }
                    ++recordsCount;
                    currentRecord = currentRecord.Next;
                }
                if (recordsCount <= MinCachedRequestsAfterPurge)
                    return;
                int recordsToLeft = Math.Max(recordsCount / 3 * 2, MinCachedRequestsAfterPurge);
                while (recordsCount > recordsToLeft)
                {
                    RemoveFromCache(this.Last);
                    --recordsCount;
                }
            }
        }
        public override string[] GetStorageTablesList(bool includeViews)
        {
            IDataStoreSchemaExplorer nestedSource = Nested as IDataStoreSchemaExplorer;
            if (nestedSource == null)
                return null;
            return nestedSource.GetStorageTablesList(includeViews);
        }
        public override DBTable[] GetStorageTables(params string[] tables)
        {
            IDataStoreSchemaExplorer nestedSource = Nested as IDataStoreSchemaExplorer;
            if (nestedSource == null)
                return null;
            return nestedSource.GetStorageTables(tables);
        }
        public override void Configure(DataCacheConfiguration configuration)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }
    }
}
