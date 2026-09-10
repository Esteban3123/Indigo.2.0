using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.DB.Helpers;
using System;
using System.ServiceModel;

namespace DistributedServices.Xpo.Contracts
{
    [ServiceContract(Namespace = "http://www.genesis.com.co")]
    public interface IDataStoreContract
    {
        [OperationContract]
        DataCacheResult NotifyDirtyTables(string company, DataCacheCookie cookie, params string[] dirtyTablesNames);

        [OperationContract]
        DataCacheResult ProcessCookie(string company, DataCacheCookie cookie);

        [OperationContract]
        [ServiceKnownType(typeof(AggregateOperand))]
        [ServiceKnownType(typeof(BetweenOperator))]
        [ServiceKnownType(typeof(BinaryOperator))]
        [ServiceKnownType(typeof(ContainsOperator))]
        [ServiceKnownType(typeof(FunctionOperator))]
        [ServiceKnownType(typeof(GroupOperator))]
        [ServiceKnownType(typeof(InOperator))]
        [ServiceKnownType(typeof(NotOperator))]
        [ServiceKnownType(typeof(NullOperator))]
        [ServiceKnownType(typeof(OperandProperty))]
        [ServiceKnownType(typeof(OperandValue))]
        [ServiceKnownType(typeof(ParameterValue))]
        [ServiceKnownType(typeof(QueryOperand))]
        [ServiceKnownType(typeof(UnaryOperator))]
        [ServiceKnownType(typeof(JoinOperand))]
        [ServiceKnownType(typeof(OperandParameter))]
        [ServiceKnownType(typeof(QuerySubQueryContainer))]
        [ServiceKnownType(typeof(ConstantValue))]
        DataCacheSelectDataResult SelectData(string company, DataCacheCookie cookie, SelectStatement[] selects);        



    }
}
