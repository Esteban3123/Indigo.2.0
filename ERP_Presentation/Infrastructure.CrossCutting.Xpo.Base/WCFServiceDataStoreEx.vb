#Region "Imports"

Imports DevExpress.Xpo.DB
Imports System.ServiceModel
Imports System.ServiceModel.Description
Imports System.ServiceModel.Channels
Imports DevExpress.Xpo.DB.Helpers
Imports Infrastructure.CrossCutting.Xpo.Base.DistributedServices.Xpo.Contracts

#End Region

Public Class WCFServiceDataStoreEx
    Inherits ClientBase(Of IXpoGateEx)
    Implements IDataStore

#Region "Fields"

    ''' <summary>
    ''' Empresa a consultar
    ''' </summary>
    Private ReadOnly company As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal endpointConfigurationName As String, ByVal company As String)
        MyBase.New(endpointConfigurationName)
        Me.company = company
    End Sub
    Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String, ByVal company As String)
        MyBase.New(endpointConfigurationName, remoteAddress)
        Me.company = company
    End Sub
    Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As EndpointAddress, ByVal company As String)
        MyBase.New(endpointConfigurationName, remoteAddress)
        Me.company = company
    End Sub
    Public Sub New(ByVal binding As Binding, ByVal remoteAddress As EndpointAddress, ByVal company As String)
        MyBase.New(binding, remoteAddress)
        Me.company = company
    End Sub

#End Region

#Region "Methods"

    Public ReadOnly Property AutoCreateOption As AutoCreateOption Implements IDataStore.AutoCreateOption
        Get
            Return AutoCreateOption.SchemaAlreadyExists
        End Get
    End Property

    Public Function ModifyData(ParamArray dmlStatements() As ModificationStatement) As ModificationResult Implements IDataStore.ModifyData
        Return Channel.ModifyData(company, dmlStatements).Result
    End Function

    Public Function SelectData(ParamArray selects() As SelectStatement) As SelectedData Implements IDataStore.SelectData
        Return Channel.SelectData(company, selects).Result
    End Function

    Public Function UpdateSchema(dontCreateIfFirstTableNotExist As Boolean, ParamArray tables() As DBTable) As UpdateSchemaResult Implements IDataStore.UpdateSchema
        Return Channel.UpdateSchema(dontCreateIfFirstTableNotExist, tables).Result
    End Function

#End Region

End Class

Public Class WCFServiceDataStoreCache
    Inherits ClientBase(Of IDataStoreContract)
    Implements ICachedDataStore

#Region "Fields"

    ''' <summary>
    ''' Empresa a consultar
    ''' </summary>
    Private ReadOnly company As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal endpointConfigurationName As String, ByVal company As String)
        MyBase.New(endpointConfigurationName)
        Me.company = company
    End Sub
    Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String, ByVal company As String)
        MyBase.New(endpointConfigurationName, remoteAddress)
        Me.company = company
    End Sub
    Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As EndpointAddress, ByVal company As String)
        MyBase.New(endpointConfigurationName, remoteAddress)
        Me.company = company
    End Sub
    Public Sub New(ByVal binding As Binding, ByVal remoteAddress As EndpointAddress, ByVal company As String)
        MyBase.New(binding, remoteAddress)
        Me.company = company
    End Sub

#End Region

#Region "Methods"

    Public ReadOnly Property AutoCreateOption As AutoCreateOption Implements IDataStore.AutoCreateOption
        Get
            Return AutoCreateOption.SchemaAlreadyExists
        End Get
    End Property

    Public Function ModifyData(cookie As DataCacheCookie, dmlStatements() As ModificationStatement) As DataCacheModificationResult Implements ICacheToCacheCommunicationCore.ModifyData
        'Try
        '    Return Channel.ModifyData(company, cookie, dmlStatements)
        'Catch ex As FaultException(Of LockingException)
        '    Throw ex.Detail
        'End Try
    End Function

    Public Function NotifyDirtyTables(cookie As DataCacheCookie, ParamArray dirtyTablesNames() As String) As DataCacheResult Implements ICacheToCacheCommunicationCore.NotifyDirtyTables
        Return Channel.NotifyDirtyTables(company, cookie, dirtyTablesNames)
    End Function

    Public Function ProcessCookie(cookie As DataCacheCookie) As DataCacheResult Implements ICacheToCacheCommunicationCore.ProcessCookie
        Return Channel.ProcessCookie(company, cookie)
    End Function

    Public Function SelectData(cookie As DataCacheCookie, selects() As SelectStatement) As DataCacheSelectDataResult Implements ICacheToCacheCommunicationCore.SelectData
        Return Channel.SelectData(company, cookie, selects)
    End Function

    Public Function UpdateSchema(cookie As DataCacheCookie, tables() As DBTable, dontCreateIfFirstTableNotExist As Boolean) As DataCacheUpdateSchemaResult Implements ICacheToCacheCommunicationCore.UpdateSchema
        Throw New NotSupportedException("Database schema modifications not allowed")
    End Function

    Public Function ModifyData(ParamArray dmlStatements() As ModificationStatement) As ModificationResult Implements IDataStore.ModifyData
        'Return Channel.ModifyData(company, DataCacheCookie.Empty, dmlStatements).ModificationResult
    End Function

    Public Function UpdateSchema(dontCreateIfFirstTableNotExist As Boolean, ParamArray tables() As DBTable) As UpdateSchemaResult Implements IDataStore.UpdateSchema
        Throw New NotSupportedException("Database schema modifications not allowed")
    End Function

    Public Function SelectData(ParamArray selects() As SelectStatement) As SelectedData Implements IDataStore.SelectData
        Return Channel.SelectData(company, DataCacheCookie.Empty, selects).SelectedData
        'Return CType(DevExpress.Xpo.XpoDefault.DataLayer, IDataStoreContract).SelectData(company, DataCacheCookie.Empty, selects).SelectedData
    End Function

#End Region

End Class

Public Class WCFServiceClearCache
    Inherits ClientBase(Of IClearCacheXpo)

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="endpointConfigurationName">Configuración del punto de entrada del servicio</param>
    ''' <param name="remoteAddress">Dirección remota del servicio</param>
    Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
        MyBase.New(endpointConfigurationName, remoteAddress)
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene el numero de Endpoint disponible para usar por una
    ''' nueva instancia de cliente
    ''' </summary>
    Public Sub ClearAllCache()
        Channel.ClearAllCache()
    End Sub

    Public Sub ClearCache(tableName As String)
        Channel.ClearCache(tableName)
    End Sub

#End Region

End Class