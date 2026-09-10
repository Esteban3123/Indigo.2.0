Imports System.ServiceModel

Namespace DistributedServices.Xpo.Contracts
    <ServiceContract>
    Public Interface IClearCacheXpo
        <OperationContract()>
        Sub ClearAllCache()
        <OperationContract()>
        Sub ClearCache(tableName As String)
    End Interface
End Namespace
