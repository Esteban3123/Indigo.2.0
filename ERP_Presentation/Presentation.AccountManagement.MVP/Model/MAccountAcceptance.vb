Imports Domain.AccountManagement.Model
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent

Public Class MAccountAcceptance
    Implements IDisposable

#Region "Fields"
    ''' <summary>
    ''' Instancia a los valores de sesión
    ''' </summary>
    Private _sessionValues As SessionValues

    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    Private _tagForm As String
#End Region

#Region "Constructor"
    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="tag">Tag del funcional</param>
    Public Sub New(ByVal tag As String)
        _tagForm = tag
        Me._sessionValues = SessionValues.Instance
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Obtiene una lista de traslados de folios de acuerdo a un centro de atención y un código de usuario
    ''' </summary>
    ''' <param name="attentionCenter"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Public Async Function ListFolioTransferRequests(attentionCenter As String, managementAreaCode As String, userCode As String) As Task(Of ActionResult(Of List(Of VDashboardProperties)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccountManagement.ListFolioTransferRequestsAsync(attentionCenter, managementAreaCode, userCode)
    End Function

    ''' <summary>
    ''' Procesa la aceptación o rechazo de una solicitud de traslado de folio
    ''' </summary>
    ''' <param name="folioTransfer"></param>
    ''' <param name="newState"></param>
    ''' <param name="operation"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Async Function ProcessTransferRequest(folioTransfer As FolioTransfer, newState As Byte, operation As String, audit As AuditMessage) As Task(Of ActionResult(Of FolioTransfer))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccountManagement.ProcessTransferRequestAsync(folioTransfer, newState, operation, audit)
    End Function

    ''' <summary>
    ''' Obtiene las areas de gestión activadas para el usuario
    ''' </summary>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Public Async Function GetManagementAreasPerUserCode(userCode As String) As Task(Of List(Of ManagementAreas))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccountManagement.ListManagementAreasByUserCodeAsync(userCode)
    End Function
#End Region


#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
