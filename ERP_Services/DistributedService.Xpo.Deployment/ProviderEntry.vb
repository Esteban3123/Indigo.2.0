#Region "Imports"

Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo.Helpers

#End Region

''' <summary>
''' Estructura de datos que representa una entrada en el diccionario
''' de proveedores de cache de datos
''' </summary>
Public Structure CachedProviderEntry

#Region "Fields"

    ''' <summary>
    ''' Proveedor de almacenamiento cache
    ''' </summary>
    Private _provider As ICachedDataStore
    ''' <summary>
    ''' Canal de comando
    ''' </summary>
    Private _commandChannel As ICommandChannel

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene el proveedor de almacenamiento cache
    ''' </summary>
    ''' <returns>proveedor de almacenamiento cache</returns>
    Public ReadOnly Property Provider As ICachedDataStore
        Get
            Return Me._provider
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el canal de comando
    ''' </summary>
    ''' <returns>canal de comando</returns>
    Public ReadOnly Property CommandChannel As ICommandChannel
        Get
            Return Me._commandChannel
        End Get
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la estructura
    ''' </summary>
    ''' <param name="provider">Proveedor de almacenamiento cache</param>
    ''' <param name="commandChannel">Canal de comando</param>
    Public Sub New(ByVal provider As ICachedDataStore, ByVal commandChannel As ICommandChannel)
        Me._provider = provider
        Me._commandChannel = commandChannel
    End Sub

#End Region

End Structure

''' <summary>
''' Estructura de datos que representa una entrada en el diccionario
''' de proveedores de datos
''' </summary>
Public Structure DataStoreProviderEntry

#Region "Fields"

    ''' <summary>
    ''' Proveedor de almacenamiento
    ''' </summary>
    Private _provider As IDataStore
    ''' <summary>
    ''' Canal de comando
    ''' </summary>
    Private _commandChannel As ICommandChannel

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene el proveedor de almacenamiento
    ''' </summary>
    ''' <returns>proveedor de almacenamiento</returns>
    Public ReadOnly Property Provider As IDataStore
        Get
            Return Me._provider
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el canal de comando
    ''' </summary>
    ''' <returns>canal de comando</returns>
    Public ReadOnly Property CommandChannel As ICommandChannel
        Get
            Return Me._commandChannel
        End Get
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la estructura
    ''' </summary>
    ''' <param name="provider">Proveedor de almacenamiento</param>
    ''' <param name="commandChannel">Canal de comando</param>
    Public Sub New(ByVal provider As IDataStore, ByVal commandChannel As ICommandChannel)
        Me._provider = provider
        Me._commandChannel = commandChannel
    End Sub

#End Region

End Structure