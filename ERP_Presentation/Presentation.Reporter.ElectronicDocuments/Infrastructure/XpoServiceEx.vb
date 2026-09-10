'***********************************************************************
' Assembly         : Presentation.Reporter.Net8
' Adapted for .NET 8
'***********************************************************************

Imports DevExpress.Xpo
Imports DevExpress.Xpo.DB
Imports DevExpress.Data.Filtering

Namespace Infrastructure

    ''' <summary>
    ''' Servicio XPO simplificado para acceso a datos
    ''' </summary>
    Public Class XpoServiceEx
        Implements IDisposable

        Private Shared _instances As New Dictionary(Of String, XpoServiceEx)
        Private ReadOnly _session As Session
        Private ReadOnly _billingService As BillingServiceSimple
        Private ReadOnly _securityService As SecurityServiceSimple
        Private ReadOnly _commonService As CommonServiceSimple

        Private Sub New(connectionString As String)
            Dim dataStore = XpoDefault.GetConnectionProvider(connectionString, AutoCreateOption.None)
            Dim dataLayer = New SimpleDataLayer(dataStore)
            _session = New Session(dataLayer)
            _billingService = New BillingServiceSimple(_session)
            _securityService = New SecurityServiceSimple(_session)
            _commonService = New CommonServiceSimple(_session)
        End Sub

        ''' <summary>
        ''' Obtiene una instancia del servicio XPO para el contenedor especificado
        ''' </summary>
        Public Shared Function Instance(containerName As String) As XpoServiceEx
            If Not _instances.ContainsKey(containerName) Then
                Dim connectionString = GetConnectionString(containerName)
                _instances(containerName) = New XpoServiceEx(connectionString)
            End If
            Return _instances(containerName)
        End Function

        ''' <summary>
        ''' Configura la cadena de conexión para un contenedor
        ''' </summary>
        Public Shared Sub SetConnectionString(containerName As String, connectionString As String)
            If _instances.ContainsKey(containerName) Then
                _instances(containerName).Dispose()
                _instances.Remove(containerName)
            End If
            _instances(containerName) = New XpoServiceEx(connectionString)
        End Sub

        Private Shared Function GetConnectionString(containerName As String) As String
            ' Por defecto, construir una cadena de conexión básica
            Return $"XpoProvider=MSSqlServer;Data Source=.;Initial Catalog={containerName};Integrated Security=True"
        End Function

        Public ReadOnly Property BillingService As BillingServiceSimple
            Get
                Return _billingService
            End Get
        End Property

        Public ReadOnly Property SecurityService As SecurityServiceSimple
            Get
                Return _securityService
            End Get
        End Property

        Public ReadOnly Property CommonService As CommonServiceSimple
            Get
                Return _commonService
            End Get
        End Property

#Region "IDisposable Support"
        Private disposedValue As Boolean

        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    _session?.Dispose()
                End If
                disposedValue = True
            End If
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

    ''' <summary>
    ''' Servicio simplificado para operaciones de Billing
    ''' </summary>
    Public Class BillingServiceSimple
        Private ReadOnly _session As Session

        Public Sub New(session As Session)
            _session = session
        End Sub

        Public Function GetCollection(Of T As Class)(sorting As Object, criteria As String) As List(Of T)
            If String.IsNullOrEmpty(criteria) Then
                Return New XPQuery(Of T)(_session).ToList()
            End If
            Dim collection = New XPCollection(Of T)(_session, CriteriaOperator.Parse(criteria))
            Return collection.ToList()
        End Function

        Public Function GetXPOObject(Of T As Class)(criteria As String) As T
            Return _session.FindObject(Of T)(CriteriaOperator.Parse(criteria))
        End Function
    End Class

    ''' <summary>
    ''' Servicio simplificado para operaciones de Security
    ''' </summary>
    Public Class SecurityServiceSimple
        Private ReadOnly _session As Session

        Public Sub New(session As Session)
            _session = session
        End Sub

        Public Function GetCollection(Of T As Class)(sorting As Object, criteria As String) As List(Of T)
            If String.IsNullOrEmpty(criteria) Then
                Return New XPQuery(Of T)(_session).ToList()
            End If
            Dim collection = New XPCollection(Of T)(_session, CriteriaOperator.Parse(criteria))
            Return collection.ToList()
        End Function
    End Class

    ''' <summary>
    ''' Servicio simplificado para operaciones Common
    ''' </summary>
    Public Class CommonServiceSimple
        Private ReadOnly _session As Session

        Public Sub New(session As Session)
            _session = session
        End Sub

        Public Function GetCollection(Of T As Class)(sorting As Object, criteria As String) As List(Of T)
            If String.IsNullOrEmpty(criteria) Then
                Return New XPQuery(Of T)(_session).ToList()
            End If
            Dim collection = New XPCollection(Of T)(_session, CriteriaOperator.Parse(criteria))
            Return collection.ToList()
        End Function

        Public Function ListOperatingUnitById(id As Integer) As Object
            ' TODO: Implementar si es necesario
            Return Nothing
        End Function
    End Class

End Namespace
