Imports System.Threading

Public NotInheritable Class ServerSessionValues

#Region "Singleton"

    ''' <summary>
    ''' Unica instancia de la clase
    ''' </summary>
    Private Shared _currentInstance As ServerSessionValues

    ''' <summary>
    ''' Obtiene la unica instancias de la clase
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property Current As ServerSessionValues
        Get
            If _currentInstance Is Nothing Then
                _currentInstance = New ServerSessionValues()
            End If
            Return _currentInstance
        End Get
    End Property

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna el nombre del actual contenedor a usar
    ''' </summary>
    Private Shared ReadOnly _currentContainer As New AsyncLocal(Of String)

    ''' <summary>
    ''' Obtiene o asigna el nombre del contenedor actual para cada hilo
    ''' </summary>
    ''' <value>Nombre del contenedor actual</value>
    ''' <returns>El nombre del contenedor actual</returns>
    Public Property CurrentContainer As String
        Get
            Return If(_currentContainer.Value, String.Empty)
        End Get
        Set(value As String)
            _currentContainer.Value = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el nombre del actual contenedor HIS a usar
    ''' </summary>
    ''' <value>Nombre del contenedor HIS actual</value>
    ''' <returns>El nombre del contenedor HIS actual</returns>
    Public Property CurrentHISContainer As String

    ''' <summary>
    ''' Obtiene o asigna la versión actual del cliente 
    ''' </summary>
    ''' <value>Nombre de la versión</value>
    ''' <returns>La versión actual del cliente</returns>
    Public Property IndigoVersion As String

    ''' <summary>
    ''' Obtiene o asigna el nombre del actual contenedor HIS a usar
    ''' </summary>
    ''' <value>Nombre del contenedor HIS actual</value>
    ''' <returns>El nombre del contenedor HIS actual</returns>
    Public Property CurrentInteropCostContainer As String

    ''' <summary>
    ''' Obtiene o asigna el nombre del actual contenedor a usar
    ''' </summary>
    Private Shared _blobContainerName As New ThreadLocal(Of String)

    ''' <summary>
    ''' se establece el nombre del contenedor de blob de fact electronica
    ''' </summary>
    ''' <value>Nombre del contenedor actual</value>
    ''' <returns>El nombre del contenedor actual</returns>
    Public Property BlobContainerName As String
        Get
            Return If(_blobContainerName?.Value, String.Empty)
        End Get
        Set(value As String)
            If _blobContainerName Is Nothing Then
                _blobContainerName = New ThreadLocal(Of String)
            End If
            _blobContainerName.Value = value
        End Set
    End Property

    ''' <summary>
    ''' Cadena de conexion blob storage
    ''' </summary>
    Private Shared _currentBlobConnectionString As New ThreadLocal(Of String)

    ''' <summary>
    ''' Cadena de conexion blob storage
    ''' </summary>
    ''' <value>Nombre del contenedor actual</value>
    ''' <returns>El nombre del contenedor actual</returns>
    Public Property CurrentBlobConnectionString As String
        Get
            Return If(_currentBlobConnectionString?.Value, String.Empty)
        End Get
        Set(value As String)
            If _currentBlobConnectionString Is Nothing Then
                _currentBlobConnectionString = New ThreadLocal(Of String)
            End If
            _currentBlobConnectionString.Value = value
        End Set
    End Property
#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Private Sub New()
        Me.CurrentContainer = String.Empty
    End Sub

#End Region

End Class
