Imports DevExpress.Data.Filtering
Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo.DB.Helpers
Imports System.ServiceModel

Namespace DistributedServices.Xpo.Contracts
    <ServiceContract([Namespace]:="http://www.genesis.com.co")>
    Public Interface IDataStoreContract
        <OperationContract>
        Function NotifyDirtyTables(company As String, cookie As DataCacheCookie, ParamArray dirtyTablesNames As String()) As DataCacheResult

        <OperationContract>
        Function ProcessCookie(company As String, cookie As DataCacheCookie) As DataCacheResult

        <OperationContract>
        <ServiceKnownType(GetType(AggregateOperand))>
        <ServiceKnownType(GetType(BetweenOperator))>
        <ServiceKnownType(GetType(BinaryOperator))>
        <ServiceKnownType(GetType(ContainsOperator))>
        <ServiceKnownType(GetType(FunctionOperator))>
        <ServiceKnownType(GetType(GroupOperator))>
        <ServiceKnownType(GetType(InOperator))>
        <ServiceKnownType(GetType(NotOperator))>
        <ServiceKnownType(GetType(NullOperator))>
        <ServiceKnownType(GetType(OperandProperty))>
        <ServiceKnownType(GetType(OperandValue))>
        <ServiceKnownType(GetType(ParameterValue))>
        <ServiceKnownType(GetType(QueryOperand))>
        <ServiceKnownType(GetType(UnaryOperator))>
        <ServiceKnownType(GetType(JoinOperand))>
        <ServiceKnownType(GetType(OperandParameter))>
        <ServiceKnownType(GetType(QuerySubQueryContainer))>
        <ServiceKnownType(GetType(ConstantValue))>
        Function SelectData(company As String, cookie As DataCacheCookie, selects As SelectStatement()) As DataCacheSelectDataResult



    End Interface
End Namespace
